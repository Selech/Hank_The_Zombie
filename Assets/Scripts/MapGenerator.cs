using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

	public List<Tile> tiles;

	// Use this for initialization
	public void LoadLevel (string path) {
//		XMLReader.Save ();
//
//		TileContainer tc = XMLReader.Load ();
//
//		tiles = tc.Tiles;
//		DrawTiles ();
	}

	void DrawTiles(){
		for (int i = 0; i < tiles.Count; i++) {
			Vector3 placement = new Vector3(tiles[i].X,-0.5f,tiles[i].Y);
			GameObject tile;
			GameObject objectOnTile;

			tile = (GameObject) LoadAssetFromString("Tile");
			tile.transform.position = placement;
			tile.transform.SetParent(this.transform);

			if(tiles[i].ObjectOnTile != ""){
				objectOnTile = (GameObject) LoadAssetFromString(tiles[i].ObjectOnTile);
				objectOnTile.transform.position = placement;
				objectOnTile.transform.SetParent(tile.transform);
			}
		}
	}

	GameObject LoadAssetFromString(string assetName){
		GameObject instance = Instantiate(Resources.Load(assetName, typeof(GameObject))) as GameObject;

		return instance;
	}
}
