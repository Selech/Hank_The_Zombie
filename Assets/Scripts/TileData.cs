using UnityEngine;
using System.Collections;

public class TileData : MonoBehaviour {

	public Vector2 position;
	//public GameObject type;

	// Use this for initialization
	void Start () 
	{
		if(LevelDesigner.isNotTesting && EditorMouse.mode == "InsertTile")
		{
			GetComponent<AudioSource>().Play();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
