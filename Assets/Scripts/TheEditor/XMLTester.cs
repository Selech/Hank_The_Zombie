using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class XMLTester : MonoBehaviour {

	public Text txt;

	// Use this for initialization
	void Start () {
		//Debug.Log("XMLTester: Creating XML...");
		createLevel1();
		createLevel2();
	}

	void createLevel1()
	{
		Level lvl 		= new Level();
		lvl.name 		= "The grand Escape";
		lvl.description = "Only the most cunning players will be able to avoid being caught in this level!";
		lvl.author 		= "flotfyr007";
		lvl.id 			= 152;
		lvl.rating		= 8.7;
		lvl.numRatings	= 34;
		lvl.numDownloads = 423;
		lvl.requiredPlayerLevel = 1;
		lvl.appVersion	= 1.0;
		lvl.xpReward	= 10;
		lvl.developerCompletionTime = 76.5;
		
		lvl.mapTilesMax = 1;
		lvl.mapTilesHorizontally = 1;
		lvl.mapTilesVertically = 1;
		
		lvl.SetWinCondition(Level.WinConditionEnum.Escape);
		lvl.SetLoseCondition(Level.LoseConditionEnum.Caught);
		
		lvl.Tiles.Add(new Tile(1, 2, "Terrain/Wall"));
		lvl.Tiles.Add(new Tile(1, 1, "Tile"));
		lvl.Tiles.Add(new Tile(1, 1, "Terrain/Chair"));
		lvl.Tiles.Add(new Tile(0, 0, "Tile"));
		lvl.Tiles.Add(new Tile(0, 2, "Terrain/Wall"));
		lvl.Tiles.Add(new Tile(0, 1, "Terrain/Wall"));
		lvl.Tiles.Add(new Tile(0, 0, "Traps/BearTrap"));
		lvl.Tiles.Add(new Tile(2, 0, "Traps/LaserWall"));
		lvl.Tiles.Add(new Tile(4, 0, "Terrain/Wall"));
		lvl.Tiles.Add(new Tile(2, 0, "Tile"));
		lvl.Tiles.Add(new Tile(2, 2, "Terrain/Wall"));
		lvl.Tiles.Add(new Tile(3, 1, "Tile"));
		lvl.Tiles.Add(new Tile(1, 3, "Tile"));

		// Save XML to file

		txt.text = Path.Combine (Application.persistentDataPath, "levels/"+lvl.name+".xml") + "\n" + txt.text;
		lvl.Save(LevelDesigner.LevelsDirectory, lvl.name+".xml");
			
		//lvl.Save(Path.Combine (Application.persistentDataPath, "levels/"+lvl.name+".xml"));
		//lvl.Save(lvl.name+".xml");
	}
	
	void createLevel2()
	{
		Level lvl = new Level();
		lvl.name 		= "Infection makes Perfection!";
		lvl.description = "As any other zombie loving freak would think a crazy infectioning breakup level should be available - Enjoy.";
		lvl.author 		= "Zombinator";
		lvl.id 			= 153;
		lvl.rating		= 6.1;
		lvl.numRatings	= 7;
		lvl.numDownloads = 103;
		lvl.requiredPlayerLevel = 2;
		lvl.appVersion	= 1.3;
		lvl.xpReward	= 20;
		lvl.developerCompletionTime = 88.6;
		
		lvl.mapTilesMax = 1;
		lvl.mapTilesHorizontally = 1;
		lvl.mapTilesVertically = 1;
		
		lvl.SetWinCondition(Level.WinConditionEnum.Infect);
		lvl.SetLoseCondition(Level.LoseConditionEnum.Killed);
		
		lvl.Tiles.Add(new Tile(1,2, "Terrain/Wall"));
		lvl.Tiles.Add(new Tile(2,2, "Traps/BearTrap"));
		lvl.Tiles.Add(new Tile(1,1, "Terrain/Chair"));
		lvl.Tiles.Add(new Tile(1,0, "Tile"));
		lvl.Tiles.Add(new Tile(2,0, "Traps/LaserWall"));
		lvl.Tiles.Add(new Tile(3,0, "Traps/BearTrap"));
		lvl.Tiles.Add(new Tile(4,0, "Tile"));
		lvl.Tiles.Add(new Tile(5,0, "Terrain/Wall"));
		lvl.Tiles.Add(new Tile(5,1, "Terrain/Wall"));
		lvl.Tiles.Add(new Tile(5,2, "Terrain/Wall"));
		lvl.Tiles.Add(new Tile(4,3, "Terrain/Wall"));
		lvl.Tiles.Add(new Tile(2,1, "Tile"));
		lvl.Tiles.Add(new Tile(2,3, "Terrain/Wall"));
		
		// Save XML to file
		lvl.Save(LevelDesigner.LevelsDirectory, lvl.name+".xml");
		//lvl.Save(lvl.name+".xml");
	}
}
