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

	private static float floorDefaultPositionY	= 0.0f;
//	private static float floorIntervalDist 		= 2.0;

	public static Level currentLevel;
	private static int horizontalTilesPerMapTile = 11; // Pænest hvis ulige :)
	private static int verticalTilesPerMapTile = 11; 
	public static int xToMid;
	public static int zToMid;
	private static Cell[,] cellGrid;
	//public GameObject displayedNameOfLevel;
	public static GameObject ObjectToBeInserted;

	public static bool HankPlaced { get{ return (GameObject.Find("CubeHank(Clone)") != null || GameObject.Find("CubeHank-NoGun(Clone)") != null); } }
	public static bool ExitPlaced { get{ return (GameObject.Find("EndDoor(Clone)") != null);} }
	public static bool isNotTesting { get; set; }
	public static bool isUsingEditor { get; set; }

	public GameObject txtLevelName;
	public GameObject popupTestComletedSuccesFully;

	public enum TileResource
	{
		Tile,
		EmptyTile
	}

	public enum WallResource
	{
		Wall
	}
	
	public enum ObjectResource
	{
		Hank,
		Rabbit
	}
		
	// Use this for initialization
	void Start () 
	{
		// Initialize Grid
		initializeGrid(); 

		// Setup and create new level
		if (LastLoaded == "")
		{
			LoadDefaultLevel();
		}
		// Otherwise load the last loaded level
		else
		{
			LoadLevel(LastLoaded);
		}
		
		// Move Camera to Midtile
		Camera.main.transform.position = new Vector3 (0,	Camera.main.transform.position.y, -zToMid);
	}

	private static void initializeGrid()
	{
		// Define grid based on size
		cellGrid = new Cell[horizontalTilesPerMapTile, verticalTilesPerMapTile];

		// Set initial values to null
		for (int x = 0; x < horizontalTilesPerMapTile; x++) 
		{
			for (int y = 0; y < verticalTilesPerMapTile; y++)
			{
				cellGrid[x, y] = null;
			}
		}

		// Calculate dist to mid based on map size
		xToMid = (int) Mathf.Ceil(horizontalTilesPerMapTile/2);
		zToMid = (int) Mathf.Ceil(verticalTilesPerMapTile/2);
	}

	void LoadDefaultLevel()
	{
		// Initialize default Level
		currentLevel 			= new Level();
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
		RegisterTile(new Vector3(0, floorDefaultPositionY, 0), LevelDesigner.TileResource.Tile);

		// Instantiate Level
		InstantiateLevel(currentLevel);
	}

	void InstantiateLevel(Level lvl)
	{
		// Place Tiles Specified in Level (And register them in 2D grid)
		int numCells = lvl.Cells.Count;

		for (int i = 0; i < numCells; i++) 
		{ 
			// Position of object and for registering in TileGrid
			Vector3 position = new Vector3(lvl.Cells[i].xPos, floorDefaultPositionY, lvl.Cells[i].zPos);

			// Place Tile
			if(lvl.Cells[i].tileData != null) lvl.Cells[i].tile = InsertIntoSceneUsingString(position, lvl.Cells[i].tileData.assetName, lvl.Cells[i].tileData.rotation);

			// Place Object
			if(lvl.Cells[i].objData != null) lvl.Cells[i].obj = InsertIntoSceneUsingString(position, lvl.Cells[i].objData.assetName, lvl.Cells[i].objData.rotation);

			// Place Wall
			if(lvl.Cells[i].wallData != null) lvl.Cells[i].wall = InsertIntoSceneUsingString(position, lvl.Cells[i].wallData.assetName,lvl.Cells[i].wallData.rotation);

			// Register in CellGrid
			RegisterCell(position, lvl.Cells[i]);
		}

		// Place Empty Tiles on the rest
		insertEmptyTiles();
	}

	/**
	 * Inserts empty tiles where values are null in the cell grid.
	 **/
	public void insertEmptyTiles()
	{
		for (int x = 0; x < horizontalTilesPerMapTile; x++) 
		{
			for (int y = 0; y < verticalTilesPerMapTile; y++)
			{
				if (cellGrid[x, y] == null)
				{
					Vector3 position = new Vector3(x - xToMid, floorDefaultPositionY, y - zToMid);
					InsertIntoSceneUsingString(position, TileResource.EmptyTile.ToString(), 0);
				}
			}
		}
	}

	/**
	 * Inserts a GameObject at the specified position based on a specified assetname.
	 **/
	private static GameObject InsertIntoSceneUsingString(Vector3 position, string assetName, float rotation)
	{
		GameObject gameObject = LoadAssetFromString(assetName);

		if (gameObject != null)
		{
			// Set rotation
			gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, rotation, gameObject.transform.eulerAngles.z);

			// Place on scene
			InsertIntoScene(position, gameObject);
		}

		return gameObject;
	}

	private static GameObject InsertIntoScene(Vector3 position, GameObject gameObject)
	{
		gameObject.transform.position = position;
		gameObject.transform.SetParent(gameObject.transform.root);

		return gameObject;
	}

	/**
	 * Registeres a Tile in a Cell of the specified position.
	 **/
	public static void RegisterTile(Vector3 position, TileResource tile)
	{
		// Get cell from grid based on the specified position
		Cell cell = getCell(position);

		// If cell is null; register a new cell with the specified Tile
		if (cell == null)
		{
			cell = new Cell(position.x, position.y, position.z, null, null, null);
			cell.tile = InsertIntoSceneUsingString(position, tile.ToString(), 0);
			setCell(position, cell);
		}
	}

	/**
	 * Registeres an Object in a Cell of the specified position.
	 **/
	public static void RegisterObject(Vector3 position, ObjectResource obj)
	{
		// Get cell from grid based on the specified position
		Cell cell = getCell(position);
		
		// If cell is null; register a new cell with the specified Object
		if (cell == null)
		{
			cell = new Cell(position.x, position.y, position.z, null, null, null);
			cell.obj = InsertIntoSceneUsingString(position, obj.ToString(), 0);
		}
	}

	/**
	 * Registeres an Object in a Cell of the specified position.
	 **/
	public static void RegisterObjectUsingGameObject(Vector3 position, GameObject obj)
	{
		// Get cell from grid based on the specified position
		Cell cell = getCell(position);

		if (cell != null && cell.obj == null)
		{
			// Insert object in the Cell
			cell.obj = InsertIntoSceneUsingString(position, obj.name, 0);
			
			// Save changes to cell
			setCell(position, cell);
		}
	}
	
	/**
	 * Registeres a Wall in a Cell of the specified position.
	 **/
	public static void RegisterWall(Vector3 position, WallResource wall)
	{
		// Get cell from grid based on the specified position
		Cell cell = getCell(position);
		
		// If cell is null; register a new cell with the specified Wall
		if (cell == null)
		{
			cell = new Cell(position.x, position.y, position.z, null, null, null);
			cell.wall = InsertIntoSceneUsingString(position, wall.ToString(), 0);
		}
	}

	/**
	 * Registeres a Cell in the specified position.
	 **/
	public static void RegisterCell(Vector3 position, Cell cell)
	{
		setCell(position, cell);
	}

	/**
	 * Places empty Tile
	 **/
	public static void RemoveCell(Vector3 position)
	{
		// Find Cell in Grid
		Cell cell = getCell(position);

		// Remove Object, Wall and Tile
		if (cell.obj != null)
		{
			Destroy (cell.obj);
		}
		if (cell.wall != null)
		{
			Destroy (cell.wall);
		}
		if (cell.tile != null)
		{
			Destroy (cell.tile);
		}

		// Set ready for Garbage Collection
		setCell(position, null);

		// Place Empty Tile
		//position.y += 0.05f; // small adjustment to match floor level
		InsertIntoSceneUsingString(position, TileResource.EmptyTile.ToString(), 0);
	}

	/**
	 *	Changes the registered value of the index specified by the provided vector (x & z is used)
	 **/
	private static void setCell(Vector3 position, Cell gObj)
	{
		// Calculate Positions
		int xPos = (int) position.x + xToMid;
		int yPos = (int) position.z + zToMid;

		cellGrid[xPos, yPos] = gObj;
	}
	
	/**
	 *	Returns a Cell, from where the Object, Tile and Wall can be accessed.
	 **/
	public static Cell getCell(Vector3 position)
	{
		print ("z("+position.z+") becomes: "+((int)(position.z + zToMid)));
		print ("x("+position.x+") becomes: "+((int)(position.x + xToMid)));
		return cellGrid[((int)position.x + xToMid), (int)(position.z + zToMid)];
	}

	/**
	 *	Returns bool value based on whether the tile of the cell doesn't have a wall and an object on it.
	 **/
	public static bool IsTileClear(Vector3 position)
	{
		return (getCell(position).objData == null && getCell(position).wallData == null);
	}

	void LoadLevel(string levelName)
	{
		string filePath = LevelsDirectory + levelName + ".xml";
		Level lvl = currentLevel = Level.Load(filePath);
		currentLevel = lvl;
		InstantiateLevel (lvl);
		txtLevelName.GetComponent<Text>().text = lvl.name;
		//displayedNameOfLevel.GetComponent<Text>().text = lvl.name;
	}

	private static GameObject LoadAssetFromString(string assetName)
	{
		// If nothing is found look in other specific folders in Resources
		string[] assetDirectories = new string[5]{ "Tiles", "Traps", "Terrain", "Hostiles", "Friendly"};
		int numDirectories = assetDirectories.Length;
		for(int i=0; i<numDirectories; i++)
		{
			object[] objs = Resources.LoadAll(assetDirectories[i]+"/"+assetName);
			int numObjs = objs.Length;

			for (int u=0; u<numObjs; u++)
			{
				// Return object if assetName and name of object is similar
				if (assetName == (objs[u] as GameObject).name)
				{
					return Instantiate(objs[u] as GameObject);
				}
			}
		} 

		return null;
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
		string fileName = currentLevel.name + ".xml";
		currentLevel.Save(LevelDesigner.LevelsDirectory, fileName);
	}

	public static bool checkForOverWrite(string levelName)
	{
		return Directory.Exists(LevelsDirectory + levelName + ".xml");
	}

	private static void PrepareForXMLSave()
	{
		List<Cell> cells = new List<Cell>();

		for (int x = 0; x < horizontalTilesPerMapTile; x++) 
		{
			for (int y = 0; y < verticalTilesPerMapTile; y++)
			{
				if(cellGrid[x,y] != null)
				{
					Cell cell = cellGrid[x,y];

					// Save data about the object at the cell
					if(cell.obj != null) cell.objData = new ObjectAtCell(cell.obj.name.Replace("(Clone)", ""), cell.obj.transform.eulerAngles.y);

					// Save data about the tile at the cell
					if(cell.tile != null) cell.tileData = new ObjectAtCell(cell.tile.name.Replace("(Clone)", ""), cell.tile.transform.eulerAngles.y);

					// Save data about the wall at the cell
					if(cell.wall != null) cell.wallData = new ObjectAtCell(cell.wall.name.Replace("(Clone)", ""), cell.wall.transform.eulerAngles.y);

					cells.Add(cell);
				}
			}
		}

		currentLevel.Cells = cells;
	}
}