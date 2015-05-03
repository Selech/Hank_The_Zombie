using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;


//[XmlRoot("LevelData")]
public class Cell
{
	[XmlArray("ObjectData")]
	[XmlArrayItem("Object")]
	private List<ObjectAtCell> ObjectData = new List<ObjectAtCell>(){new ObjectAtCell()};
	public ObjectAtCell objData
	{
		get { return ObjectData[0]; }
		set { ObjectData[0] = value; }
	}
	[XmlIgnore]
	public GameObject obj;

	[XmlArray("WallData")]
	[XmlArrayItem("Wall")]
	private List<ObjectAtCell> WallData = new List<ObjectAtCell>(){new ObjectAtCell()};
	public ObjectAtCell wallData
	{
		get { return WallData[0]; }
		set { WallData[0] = value; }
	}
	[XmlIgnore]
	public GameObject wall;

	[XmlArray("TileData")]
	[XmlArrayItem("Tile")]
	private List<ObjectAtCell> TileData = new List<ObjectAtCell>(){new ObjectAtCell()};
	public ObjectAtCell tileData
	{
		get { return TileData[0]; }
		set { TileData[0] = value; }
	}
	[XmlIgnore]
	public GameObject tile;

	[XmlAttribute("xPos")]
	public float xPos;
	
	[XmlAttribute("yPos")]
	public float yPos;

	[XmlAttribute("zPos")]
	public float zPos;

	public Cell()
	{
	}

	public Cell(float xPos,
	            float yPos,
	            float zPos,
	            ObjectAtCell objData = null,  
	            ObjectAtCell wallData = null, 
	            ObjectAtCell tileData = null)
	{
		this.xPos = xPos;
		this.yPos = yPos;
		this.zPos = zPos;
		this.objData = objData;
		this.wallData = wallData;
		this.tileData = tileData;
	}

	public override string ToString()
	{
		string objName = (obj == null) ? "" : obj.name.Replace("(Clone)", "");
		string tileName = (tile == null) ? "" : tile.name.Replace("(Clone)", "");
		string wallName = (wall == null) ? "" : wall.name.Replace("(Clone)", "");

		return "[Cell] xPos: "+xPos+", yPos: "+yPos+", zPos: "+zPos+", obj: "+objName+", tile: "+tileName+", "+"wall: "+wallName;
	}
}
