﻿using UnityEngine;
using System.Collections;

public class ChairScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerScript>().SelectedObject(this.gameObject);
		}
	}
}
