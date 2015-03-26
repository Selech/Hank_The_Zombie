using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("TilesCollection")]
public class TileContainer
{
	[XmlArray("Tiles"),XmlArrayItem("Tile")]
	public List<Tile> Tiles = new List<Tile>();

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(TileContainer));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static TileContainer Load(string path)
	{
		var serializer = new XmlSerializer(typeof(TileContainer));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as TileContainer;
		}
	}
}

public class XMLReader : MonoBehaviour {

	public static TileContainer tileCollection;

	public static void Save(){
		TileContainer tileContainer = new TileContainer ();

		tileContainer.Tiles.Add(new Tile(1,2, "Wall"));
		tileContainer.Tiles.Add(new Tile(2,2, "Objects/BearTrap"));
		tileContainer.Tiles.Add(new Tile(1,1, "Objects/Chair"));
		tileContainer.Tiles.Add(new Tile(1,0, ""));
		tileContainer.Tiles.Add(new Tile(2,0, "Traps/LaserWall"));
		tileContainer.Tiles.Add(new Tile(3,0, "Traps/BearTrap"));
		tileContainer.Tiles.Add(new Tile(4,0, ""));
		tileContainer.Tiles.Add(new Tile(5,0, "Wall"));
		tileContainer.Tiles.Add(new Tile(5,1, "Wall"));
		tileContainer.Tiles.Add(new Tile(5,-1, "Wall"));
		tileContainer.Tiles.Add(new Tile(-1,0, "Wall"));
		tileContainer.Tiles.Add(new Tile(2,-1, ""));
		tileContainer.Tiles.Add(new Tile(2,-2, "Wall"));

		tileContainer.Save(Path.Combine (Application.persistentDataPath, "john.xml"));
		print (Application.persistentDataPath);
	}

	public static TileContainer Load(){
		tileCollection = TileContainer.Load(Path.Combine(Application.persistentDataPath, "john.xml"));
		return tileCollection;
	}
}
