using UnityEngine;
using System.Collections;

public class AmmoCrateScript : MonoBehaviour {

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerScript>().GiveAmmo();
			Destroy (this.gameObject);
		}
	}
}
