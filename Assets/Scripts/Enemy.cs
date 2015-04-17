﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private MouseControls GameController;
	private bool seen;
	private float moveSpeed = 0.008f;
	private bool hit = false;
	public GameObject hat;
	private Vector3 startPoint;
	public GameObject AmmoCratePrefab;

	// Use this for initialization
	void Start () {
		seen = false;
		this.GameController = GameObject.Find ("Controller").GetComponent<MouseControls>();
		startPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GameController.player != null && !hit) {
			Vector3 TargetPlayer = new Vector3(this.GameController.player.transform.position.x, transform.position.y, this.GameController.player.transform.position.z);

			if(this.GameController.player.GetComponent<PlayerScript>().powerActive == true){
				moveSpeed = -0.008f;
			}

			if(Vector3.Distance(TargetPlayer, transform.position) < 3.5f || seen){

				seen = true;

				if(!hit){
					transform.position = Vector3.MoveTowards (transform.position, TargetPlayer, moveSpeed);
					transform.LookAt (TargetPlayer);
				}
			}
			else {
				if(!(Vector3.Distance(startPoint, transform.position) < 1f)){
					transform.position = Vector3.MoveTowards (transform.position, startPoint, moveSpeed);
					transform.LookAt (startPoint);
				}
			}
		}
	}

	public void SetStartPoint(Vector3 startPoint){
		this.startPoint = startPoint;
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player" && !hit) {
			if(this.GameController.player.GetComponent<PlayerScript>().powerActive == true){
				if(Random.Range(0,3) == 0){
					GameObject ammocrate = Instantiate(AmmoCratePrefab);
					ammocrate.transform.position = this.transform.position;
				}

				Destroy (this.gameObject);
			}
			else{
				other.gameObject.GetComponent<PlayerScript>().enabled = false;
				other.gameObject.GetComponent<Rigidbody>().AddForce((other.gameObject.transform.position - transform.position)*500);
				this.GameController.player = null;
			}
		}

		if (other.gameObject.tag == "Bullet" && !hit) {
			Destroy(other.gameObject);

			if(Random.Range(0,3) == 0){
				GameObject ammocrate = Instantiate(AmmoCratePrefab);
				ammocrate.transform.position = this.transform.position;
			}
			hit = true;
			hat.transform.SetParent(null);
			hat.GetComponent<Rigidbody>().isKinematic = false;
			hat.GetComponent<Rigidbody>().AddForce(new Vector3(0,100f,0));
		}

		
		if (other.gameObject.tag == "Enemy") {
			startPoint = this.transform.position;
		}
	}

}
 