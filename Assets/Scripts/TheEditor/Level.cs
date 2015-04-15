using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.ComponentModel;
using System.Reflection;
using System.IO;


[XmlRoot("LevelData")]
public class Level {

	[XmlArrayItem("name")]
	public string name;
	
	[XmlArrayItem("description")]
	public string description;

	[XmlArrayItem("author")]
	public string author;

	[XmlArrayItem("id")]
	public int id;
	
	[XmlArrayItem("rating")]
	public double rating;
	
	[XmlArrayItem("numRatings")]
	public int numRatings;
	
	[XmlArrayItem("numDownloads")]
	public int numDownloads;
	
	[XmlArrayItem("requiredPlayerLevel")]
	public int requiredPlayerLevel;
	
	[XmlArrayItem("appVersion")]
	public double appVersion;

	[XmlArrayItem("xpReward")]
	public int xpReward;
	
	[XmlArrayItem("developerCompletionTime")]
	public double developerCompletionTime;


	[XmlArrayItem("mapTilesMax")]
	public int mapTilesMax = 1;
	
	[XmlArrayItem("mapTilesHorizontally")]
	public int mapTilesHorizontally = 1;
	
	[XmlArrayItem("mapTilesVertically")]
	public int mapTilesVertically = 1;

	[XmlArrayItem("winCondition")]
	private string winCondition;
	public void SetWinCondition(WinConditionEnum enumse){winCondition = enumse.ToName();}
	public enum WinConditionEnum {
		[Description("Destroy Lab Equipment")] Equipment, 
		[Description("Infect Everything")] Infect, 
		[Description("Escape uncaught")] Escape,
		[Description("Solve the Puzzle")] Puzzle}

	[XmlArrayItem("loseCondition")]
	private string loseCondition;
	public void SetLoseCondition(LoseConditionEnum enumse){loseCondition = enumse.ToName();}
	public enum LoseConditionEnum {
		[Description("Killed")] Killed, 
		[Description("Caught")] Caught}

	[XmlArray("Tiles")]
	[XmlArrayItem("Tile")]
	public List<Tile> Tiles = new List<Tile>();

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(Level));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static Level Load(string path)
	{
		var serializer = new XmlSerializer(typeof(Level));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as Level;
		}
	}
}