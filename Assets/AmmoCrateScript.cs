using UnityEngine;
using System.Collections;

public class AmmoCrateScript : MonoBehaviour {

	public int ammoAmount;

	void Start(){
		if(ammoAmount == 0){
			ammoAmount = Random.Range(5,25);
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerScript>().GiveAmmo(ammoAmount);
			Destroy (this.gameObject);
		}
	}
}
