using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DesigningLevel : MonoBehaviour 
{
	public List<Tile> tiles;
	public string caseSwitch;
	// Use this for initialization
	void Start () 
	{
		caseSwitch = "New Level";
		//caseSwitch = "From XML File";
		DesignLevel (caseSwitch);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void DesignLevel (string caseSwitch)
	{
		switch (caseSwitch) 
		{
		case "New Level":
			NewLevel();
			break;
		case "From XML File":
			LoadLevel();
			break;
		}
	}

	void NewLevel()
	{
		int xMax = 5;
		int yMax = 5;

		TileContainer tileContainer = new TileContainer ();
		Tile tile; 

		for (int x = 0; x < xMax; x++) {
			for(int y = 0; y < xMax; y++){
				tile = new Tile(x,y,"");
				tileContainer.Tiles.Add (tile);
			}
		}

		tiles = tileContainer.Tiles;

		DrawNewLevel ();
	}

	void LoadLevel()
	{
		XMLReader.Save ();
		
		TileContainer tc = XMLReader.Load ();
		
		tiles = tc.Tiles;
		DrawLevel ();
	}

	void DrawNewLevel()
	{
		for (int i = 0; i < tiles.Count; i++) 
		{
			Vector3 placement = new Vector3(tiles[i].X,-0.5f,tiles[i].Y);
			GameObject tile;
			GameObject objectOnTile;

			if(tiles[i].X == 0 && tiles[i].Y == 0)
			{
				tile = (GameObject) LoadAssetFromString("Tile");
				tile.transform.position = placement;
				tile.transform.SetParent(this.transform);
			}
			else
			{
				tile = (GameObject) LoadAssetFromString("EmptyTile");
				tile.transform.position = placement;
				tile.transform.SetParent(this.transform);
			}
		}
	}
	void DrawLevel()
	{
		for (int i = 0; i < tiles.Count; i++) 
		{
			Vector3 placement = new Vector3(tiles[i].X,-0.5f,tiles[i].Y);
			GameObject tile;
			GameObject objectOnTile;
			
			tile = (GameObject) LoadAssetFromString("Tile");
			tile.transform.position = placement;
			tile.transform.SetParent(this.transform);
			
			if(tiles[i].ObjectOnTile != "")
			{
				objectOnTile = (GameObject) LoadAssetFromString(tiles[i].ObjectOnTile);
				objectOnTile.transform.position = placement;
				objectOnTile.transform.SetParent(tile.transform);
			}
		}
	}
	
	GameObject LoadAssetFromString(string assetName)
	{
		GameObject instance = Instantiate(Resources.Load(assetName, typeof(GameObject))) as GameObject;
		
		return instance;
	}
}
