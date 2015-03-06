using UnityEngine;
using System.Collections;

public class CollectableScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player")
		{
			print ("Picked up one bottle of experimental cure");
			Destroy(this.gameObject);
		}
	}
}
