using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

[XmlType("ObjectAtCell")]
public class ObjectAtCell
{
	[XmlAttribute("assetName")]
	public string assetName;

	[XmlAttribute("rotation")]
	public float rotation;

	public ObjectAtCell()
	{
	}

	public ObjectAtCell(string theAssetName, float rotation = 0)
	{
		this.assetName = theAssetName;
		this.rotation = rotation;
	}
}