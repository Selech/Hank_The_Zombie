﻿using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public Vector3 direction;
	private int lifetime = 200;
	public AudioClip bullet;

	// Use this for initialization
	void Start () {
		AudioSource.PlayClipAtPoint (bullet, GameObject.Find("Main Camera").GetComponent<Transform>().position);

		this.GetComponent<Rigidbody>().AddForce(direction*500);
	}
	
	// Update is called once per frame
	void Update () {
		lifetime--;

		if (lifetime <= 0) {
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter(Collision other ){
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerScript>().GiveAmmo(1);
			Destroy(this.gameObject);
		}
	}
}