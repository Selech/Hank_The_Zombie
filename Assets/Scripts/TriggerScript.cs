using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {

	public GameObject[] activate;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			foreach (GameObject go in activate) {
				if(go != null){
					go.SetActive(true);
				}
			}
			Destroy (this.gameObject);
		}
	}

}
