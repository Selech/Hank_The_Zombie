using UnityEngine;
using System.Collections;

public class BearTrapScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player")
		{
			gameObject.GetComponent<Animator> ().SetBool ("Hit",true);
			other.gameObject.GetComponent<PlayerScript>().setTarget(other.gameObject);
			other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(10,4,0),ForceMode.VelocityChange);
		}
	}
}
