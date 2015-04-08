using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

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
	int horizontalTilesPerMapTile = 5; // Pænest hvis ulige :)
	int verticalTilesPerMapTile = 5; 
	string[,] tileGrid;

	// Use this for initialization
	void Start () 
	{
		// Initialize Grid
		tileGrid = new string[horizontalTilesPerMapTile, verticalTilesPerMapTile];

		// Setup and create new level
		if (LastLoaded == "")
		{
			setup();
			create(currentLevel);
		}
		// Otherwise load the last loaded level
		else
		{
			LoadLevel(LastLoaded);
		}
	}

	void setup()
	{
		// Initialize default Level
		currentLevel = new Level();
		currentLevel.name 		= "<< Name of level >>";
		currentLevel.author 		= "<< Name of author >>";
		currentLevel.description = "<< Description of level >>";
		currentLevel.id 			= 0;
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
		
		currentLevel.SetWinCondition(Level.WinConditionEnum.Infect);
		currentLevel.SetLoseCondition(Level.LoseConditionEnum.Killed);

		// Place Tile in middle
		currentLevel.Tiles.Add(new Tile(0, 0, ""));
	}

	void create(Level lvl)
	{
		// Place Tiles Specified in Level (And register them in 2D grid)
		int numTiles = lvl.Tiles.Count;

		for (int i = 0; i < numTiles; i++) 
		{
			Vector3 placement = new Vector3(lvl.Tiles[i].X, -0.5f, lvl.Tiles[i].Y);

			// Place Tile no matter what (lvl only entails info of those placed)
			GameObject tile = (GameObject) LoadAssetFromString("Tile");
			tile.transform.position = placement;
			tile.transform.SetParent(this.transform);

			// Place object if specified
			if (lvl.Tiles[i].ObjectOnTile != "")
			{
				GameObject obj = (GameObject) LoadAssetFromString(lvl.Tiles[i].ObjectOnTile);
				if(obj != null)
				{
					obj.transform.position = placement;
					obj.transform.SetParent(this.transform);
				}
			}
		}

		// Place Empty Tiles where nothing is placed according to 2D Grid
		int xToMid = (int) Mathf.Ceil(horizontalTilesPerMapTile/2);
		int yToMid = (int) Mathf.Ceil(verticalTilesPerMapTile/2);
		for (int x = 0; x < horizontalTilesPerMapTile; x++) 
		{
			for (int y = 0; y < verticalTilesPerMapTile; y++)
			{
				//Debug.Log("["+x+", "+y+"] " + tileGrid[x, y]);

				if (tileGrid[x, y] != "")
				{
					tileGrid[x, y] = "EmptyTile";
					Vector3 placement2 = new Vector3(x - xToMid, -0.5f, y - yToMid);
					GameObject obj2 = (GameObject) LoadAssetFromString("EmptyTile");
					obj2.transform.position = placement2;
					obj2.transform.SetParent(this.transform);
				}
			}
		}
	}
	
	void LoadLevel(string levelName)
	{
		string filePath = LevelsDirectory + levelName + ".xml";
		Level lvl = Level.Load(filePath);
		create (lvl);
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
}