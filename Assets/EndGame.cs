using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	void OnCollisionEnter(){
		GameObject.Find ("Controller").GetComponent<GameController> ().GameEnd ();
	}
}
