using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public Vector3 direction;
	private int lifetime = 100;

	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody>().AddForce(direction*500);

	}
	
	// Update is called once per frame
	void Update () {
		lifetime--;

		if (lifetime <= 0) {
			Destroy (this.gameObject);
		}
	}
}
