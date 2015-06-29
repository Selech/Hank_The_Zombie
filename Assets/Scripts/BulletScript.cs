using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public Vector3 direction;
	private int lifetime = 500;
	public AudioClip bullet;
	public AudioClip pickup;

	// Use this for initialization
	void Start () {
		Debug.Log ("Shots fired!");
		AudioSource.PlayClipAtPoint (bullet, GameObject.Find("Main Camera").GetComponent<Transform>().position);
		this.GetComponent<Rigidbody>().AddForce(direction*6,ForceMode.Impulse);
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
			AudioSource.PlayClipAtPoint (pickup, GameObject.Find("Main Camera").GetComponent<Transform>().position);
			other.gameObject.GetComponent<PlayerScript>().GiveAmmo(1);
			Destroy(this.gameObject);
		}
	}
}
