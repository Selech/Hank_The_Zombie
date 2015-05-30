using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player") {
			GameObject.Find ("Controller").GetComponent<GameController> ().GameEnd ();
		}
	}
}
