using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public Vector3 direction;

	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody>().AddForce(direction*500);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
