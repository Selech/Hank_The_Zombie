﻿using UnityEngine;
using System.Collections;

public class MouseControls : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		//player.GetComponent<PlayerScript>().setTarget = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){ // if left button pressed...
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				player.GetComponent<PlayerScript>().setTarget(hit.collider.gameObject);
			}
		}



	}
}