using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {

	public GameObject canvas;

	void OnTriggerEnter(){
		canvas.SetActive (true);
		Destroy (this.gameObject);
	}

}
