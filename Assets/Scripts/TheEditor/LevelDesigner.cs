using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;

public class LevelDesigner : MonoBehaviour 
{
	public static string LastLoaded
	{
		get { return PlayerPrefs.GetString("editor_last_loaded_level"); }
		set { PlayerPrefs.SetString("editor_last_loaded_level", value); }
	}

	public static string LevelsDirectory
	{
		get { return Application.persistentDataPath + "/levels/"; }
		set { Debug.Log("LevelsDirectory Cannot be changed"); }
	}

	public static Level currentLevel;
	private static int horizontalTilesPerMapTile = 11; // Pænest hvis ulige :)
	private static int verticalTilesPerMapTile = 11; 
	public static int xToMid;
	public static int zToMid;
	private static string[,] tileGrid;
	public GameObject displayedNameOfLevel;
	public static GameObject ObjectToBeInserted;

	// Use this for initialization
	void Start () 
	{
		// Initialize Grid
		tileGrid = new string[horizontalTilesPerMapTile, verticalTilesPerMapTile];

		// Setup and create new level
		if (LastLoaded == "")
		{
			Setup();
			create(currentLevel);
		}
		// Otherwise load the last loaded level
		else
		{
			LoadLevel(LastLoaded);
		}
		
		// Move Camera to Midtile
		Camera.main.transform.position = new Vector3 (0,	Camera.main.transform.position.y, -zToMid);
	}

	void Setup()
	{
		// Initialize default Level
		currentLevel = new Level();
		currentLevel.name 		= "<< Name of level >>";
		currentLevel.author 	= "<< Name of author >>";
		currentLevel.description = "<< Description of level >>";
		currentLevel.id 		= 0;
		currentLevel.rating		= 0;
		currentLevel.numRatings	= 0;
		currentLevel.numDownloads = 0;
		currentLevel.requiredPlayerLevel = 0;
		currentLevel.appVersion	= 1.0;
		currentLevel.xpReward	= 0;
		currentLevel.developerCompletionTime = 0;
		
		currentLevel.mapTilesMax = 1;
		currentLevel.mapTilesHorizontally = 1;
		currentLevel.mapTilesVertically = 1;
		
		// Place Tile in middle
		currentLevel.Tiles.Add(new Tile(0, 0, "Tile"));

		// Initialize indexes in 2D array
		for (int x = 0; x < horizontalTilesPerMapTile; x++) 
		{
			for (int y = 0; y < verticalTilesPerMapTile; y++)
			{
				tileGrid[x, y] = "";
			}
		}
	}

	void create(Level lvl)
	{
		// Place Empty Tiles where nothing is placed according to 2D Grid
		xToMid = (int) Mathf.Ceil(horizontalTilesPerMapTile/2);
		zToMid = (int) Mathf.Ceil(verticalTilesPerMapTile/2);

		// Place Tiles Specified in Level (And register them in 2D grid)
		int numTiles = lvl.Tiles.Count;

		for (int i = 0; i < numTiles; i++) 
		{
			// Ved of object and for registering in TileGrid
			Vector3 vec = new Vector3(lvl.Tiles[i].X, -0.5f, lvl.Tiles[i].Y);

			// Asset path to Ressource-folder
			string assetPath = lvl.Tiles[i].ObjectOnTile;

			// Place object if specified
			if (lvl.Tiles[i].ObjectOnTile != "")
			{
				GameObject obj = (GameObject) LoadAssetFromString(assetPath);
				if(obj != null)
				{
					obj.transform.position = vec;
					obj.transform.SetParent(this.transform);
				}
			}

			// Register in TileGrid
			RegisterOnTile(vec, assetPath);
		}

		for (int x = 0; x < horizontalTilesPerMapTile; x++) 
		{
			for (int y = 0; y < verticalTilesPerMapTile; y++)
			{
				if (tileGrid[x, y] == "")
				{
					tileGrid[x, y] = "EmptyTile";
					Vector3 placement2 = new Vector3(x - xToMid, -0.5f, y - zToMid);
					
					GameObject obj2 = (GameObject) LoadAssetFromString("EmptyTile");
					obj2.transform.position = placement2;
					obj2.transform.SetParent(this.transform);
				}
			}
		}
	}

	public static void RegisterOnTile(Vector3 vec, string assetResourcePath)
	{
		SetValueOfTileGridIndex(vec, assetResourcePath);
	}

	public static void UnRegisterOnTile(Vector3 vec)
	{
		SetValueOfTileGridIndex(vec, "EmptyTile");
	}

	/**
	 *	Changes the registered value of the index specified by the provided vector (x & z is used)
	 **/
	private static void SetValueOfTileGridIndex(Vector3 vec, string newResourcePath)
	{
		tileGrid[((int)vec.x + xToMid), (int)(vec.z + zToMid)] = newResourcePath;
	}
	
	/**
	 *	Get the Prefab path of the GameObjects position. Returned string could be "Traps\Beartrap" i.e.
	 **/
	public static string GetValueOfTileGridIndex(Vector3 vec)
	{
		print ("Victoorrrr: "+vec.ToString());
		return tileGrid[((int)vec.x + xToMid), (int)(vec.z + zToMid)];
	}

	/**
	 *	Get the Prefab path of the GameObjects position. Returned string could be "Traps\Beartrap" i.e.
	 **/
	public static bool IsTileClear(Vector3 vec)
	{
		print ("Whats on tile: "+GetValueOfTileGridIndex(vec));
		return (GetValueOfTileGridIndex(vec) == "Tile");
	}

	void LoadLevel(string levelName)
	{
		string filePath = LevelsDirectory + levelName + ".xml";
		Level lvl = currentLevel = Level.Load(filePath);
		currentLevel = lvl;
		create (lvl);
		try{
			displayedNameOfLevel.GetComponent<Text>().text = lvl.name;
		} catch(System.Exception e)
		{
			print (e.StackTrace);
		};
	}

	GameObject LoadAssetFromString(string assetName)
	{
		GameObject instance = null;

		try
		{
			instance = Instantiate(Resources.Load(assetName, typeof(GameObject))) as GameObject;
		}
		catch
		{
			Debug.Log("[LevelDesigner] ** ERROR ** Couldn't instanciate '"+assetName+"'");
		}
		
		return instance;
	}

	/**
	 * This method ONLY exist because "SaveLevel()" cannot be access when static from a dragged into component called function in the GUI of Unity.
	 **/
	public void comeOnSave()
	{
		SaveLevel();
	}

	public static void SaveLevel()
	{
		PrepareForXMLSave();
//		print ("Saving into: "+(Directory.Exists(LevelsDirectory + currentLevel.name + ".xml")));
		string fileName = currentLevel.name + ".xml";
		currentLevel.Save(LevelDesigner.LevelsDirectory, fileName);
	}

	public static bool checkForOverWrite(string levelName)
	{
//		print (LevelsDirectory + levelName + ".xml");
		return Directory.Exists(LevelsDirectory + levelName + ".xml");
	}

	private static void PrepareForXMLSave()
	{
		List<Tile> tiles = new List<Tile>();

		for (int x = 0; x < horizontalTilesPerMapTile; x++) 
		{
			for (int y = 0; y < verticalTilesPerMapTile; y++)
			{
//				print("["+x+","+y+"] " + tileGrid[x,y]);
				if(tileGrid[x,y] != "EmptyTile")
				{
					//print ("x - xToMid, y - zToMid: "+(x - xToMid) +", "+(  y - zToMid));
					tiles.Add(new Tile(x - xToMid, y - zToMid, tileGrid[x, y]));
				}
			}
		}

		currentLevel.Tiles = tiles;
	}
}