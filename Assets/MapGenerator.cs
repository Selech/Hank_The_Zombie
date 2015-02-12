﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

	public GameObject roadTile;
	public GameObject trapTile;
	public GameObject goalTile;
	
	public int width;
	public int height;
	public Vector2 goal;
	public List<Vector2> traps;

	// Use this for initialization
	void Start () {
		StartCoroutine ("DrawMap");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator DrawMap() {
		for (int i = 0; i <= width; i++) {
			for (int l = 0; l <= height; l++) {
				Vector3 placement = new Vector3(i,-0.5f,l);
				GameObject tile;

				if(i == goal.x && l == goal.y ){
					tile = (GameObject) Instantiate(goalTile,placement,new Quaternion());
				}

				else if (traps.Contains(new Vector2(i,l))){
					tile = (GameObject) Instantiate(trapTile,placement,new Quaternion());
				}

				else{
					tile = (GameObject) Instantiate(roadTile,placement,new Quaternion());
				}
				tile.transform.SetParent(this.transform);

				yield return new WaitForSeconds(0f);;
				//yield return new WaitForSeconds(0.1f / width);
			}
		}
	}
}
