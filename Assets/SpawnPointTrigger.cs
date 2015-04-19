using UnityEngine;
using System.Collections;

public class SpawnPointTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Moveable") {
			GetComponentInParent<SpawnPointScript>().running = false;
			other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		}
	}
}
