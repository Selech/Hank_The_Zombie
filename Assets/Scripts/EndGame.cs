using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player") {
			Destroy (other.gameObject);
			GameObject.Find ("Controller").GetComponent<GameController> ().GameEnd ();
			GameObject.Find ("Controller").GetComponent<TimeController> ().started = false;
		}
	}
}
