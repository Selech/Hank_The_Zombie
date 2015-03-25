using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Tile {
	
	[XmlAttribute("objectOnTile")]
	public string ObjectOnTile;
	
	[XmlAttribute("x")]
	public int X;
	
	[XmlAttribute("y")]
	public int Y;

	private Tile(){}
	public Tile(int x, int y, string objectOnTile)
	{
		this.ObjectOnTile = objectOnTile;
		this.X = x;
		this.Y = y;
	}
}
