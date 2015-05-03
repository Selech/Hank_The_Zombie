using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class XMLTester : MonoBehaviour {

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
		
		lvl.Cells.Add(new Cell(1, 0, 2, null, new ObjectAtCell("Wall", 0)));
		lvl.Cells.Add(new Cell(1, 0, 1, new ObjectAtCell("Chair", 90), null, new ObjectAtCell("Tile", 0)));
		lvl.Cells.Add(new Cell(0, 0, 0, new ObjectAtCell("BearTrap",0), null, new ObjectAtCell("Tile",0)));
		lvl.Cells.Add(new Cell(0, 0, 2, null, new ObjectAtCell("Wall",0)));
		lvl.Cells.Add(new Cell(0, 0, 1, null, new ObjectAtCell("Wall",0)));
		lvl.Cells.Add(new Cell(2, 0, 1, new ObjectAtCell("LaserWall",0)));
		lvl.Cells.Add(new Cell(4, 0, 0, null, new ObjectAtCell("Wall",0)));
		lvl.Cells.Add(new Cell(2, 0, 0, null, null, new ObjectAtCell("Tile",0)));
		lvl.Cells.Add(new Cell(2, 0, 2, null, new ObjectAtCell("Wall",0)));
		lvl.Cells.Add(new Cell(3, 0, 1, null, null, new ObjectAtCell("Tile",0)));
		lvl.Cells.Add(new Cell(1, 0, 3, null, null, new ObjectAtCell("Tile",0)));

		// Save XML to file
		lvl.Save(LevelDesigner.LevelsDirectory, lvl.name+".xml");
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
		
		lvl.Cells.Add(new Cell(1, 0, 2, null, new ObjectAtCell("Wall",0)));
		lvl.Cells.Add(new Cell(2, 0, 2, new ObjectAtCell("BearTrap",0)));
		lvl.Cells.Add(new Cell(1, 0, 1, new ObjectAtCell("Chair", 180)));
		lvl.Cells.Add(new Cell(2, 0, 2, new ObjectAtCell("BearTrap",270)));
		lvl.Cells.Add(new Cell(1, 0, 0, null, null, new ObjectAtCell("Tile",0)));
		lvl.Cells.Add(new Cell(2, 0, 0, new ObjectAtCell("LaserWall",0)));
		lvl.Cells.Add(new Cell(3, 0, 0, new ObjectAtCell("BearTrap",0)));
		lvl.Cells.Add(new Cell(4, 0, 0, null, null, new ObjectAtCell("Tile",0)));
		lvl.Cells.Add(new Cell(5, 0, 0, null, new ObjectAtCell("Wall",0)));
		lvl.Cells.Add(new Cell(5, 0, 1, null, new ObjectAtCell("Wall",0)));
		lvl.Cells.Add(new Cell(5, 0, 2, null, new ObjectAtCell("Wall",0)));
		lvl.Cells.Add(new Cell(4, 0, 3, null, new ObjectAtCell("Wall",0)));
		lvl.Cells.Add(new Cell(2, 0, 1, null, null, new ObjectAtCell("Tile",0)));
		lvl.Cells.Add(new Cell(2, 0, 3, null, new ObjectAtCell("Wall",0)));
		
		// Save XML to file
		lvl.Save(LevelDesigner.LevelsDirectory, lvl.name+".xml");
	}
}
