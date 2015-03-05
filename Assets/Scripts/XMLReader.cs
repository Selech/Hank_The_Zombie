using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Tile {

	public int X;
	public int Y;
	
	public string ObjectOnTile;
}

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

		Tile tile = new Tile ();
		tile.X = 1;
		tile.Y = 2;
		tile.ObjectOnTile = "Wall";
		tileContainer.Tiles.Add (tile);

		tile = new Tile ();
		tile.X = 1;
		tile.Y = 0;
		tile.ObjectOnTile = "";
		tileContainer.Tiles.Add (tile);

		tile = new Tile ();
		tile.X = 1;
		tile.Y = 1;
		tile.ObjectOnTile = "";
		tileContainer.Tiles.Add (tile);

		tile = new Tile ();
		tile.X = 0;
		tile.Y = 0;
		tile.ObjectOnTile = "";
		tileContainer.Tiles.Add (tile);

		tile = new Tile ();
		tile.X = 0;
		tile.Y = 2;
		tile.ObjectOnTile = "Wall";
		tileContainer.Tiles.Add (tile);

		tile = new Tile ();
		tile.X = 0;
		tile.Y = 1;
		tile.ObjectOnTile = "Wall";
		tileContainer.Tiles.Add (tile);

		tile = new Tile ();
		tile.X = 3;
		tile.Y = 0;
		tile.ObjectOnTile = "Traps/BearTrap";
		tileContainer.Tiles.Add (tile);

		tile = new Tile ();
		tile.X = 2;
		tile.Y = 0;
		tile.ObjectOnTile = "Traps/LaserWall";
		tileContainer.Tiles.Add (tile);

		tile = new Tile ();
		tile.X = -1;
		tile.Y = 0;
		tile.ObjectOnTile = "Wall";
		tileContainer.Tiles.Add (tile);

		tileContainer.Save(Path.Combine (Application.persistentDataPath, "monsters.xml"));
		print (Application.persistentDataPath);
	}

	public static TileContainer Load(){
		tileCollection = TileContainer.Load(Path.Combine(Application.persistentDataPath, "monsters.xml"));
		return tileCollection;
	}

}
