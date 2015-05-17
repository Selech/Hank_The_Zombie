using UnityEngine;
using System.Collections;

public class AmmoCrateScript : MonoBehaviour {

	public int ammoAmount;
	public AudioClip pickup;

	void Start(){
		if(ammoAmount == 0){
			ammoAmount = Random.Range(5,25);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			//AudioSource.PlayClipAtPoint (pickup, GameObject.Find("Main Camera").GetComponent<Transform>().position);

			other.gameObject.GetComponent<PlayerScript>().GiveAmmo(ammoAmount);
			Destroy (this.gameObject);
		}
	}
}
