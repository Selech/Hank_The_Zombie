using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {

	public GameObject activate;

	void OnTriggerEnter(){
		activate.SetActive (true);
		Destroy (this.gameObject);
	}

}
