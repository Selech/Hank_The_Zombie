using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {

	public GameObject[] activate;
	public AudioClip sound;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			if (sound != null) {
				AudioSource.PlayClipAtPoint (sound, GameObject.Find("Main Camera").GetComponent<Transform>().position);
			}

			foreach (GameObject go in activate) {
				if(go != null){
					go.SetActive(true);
				}
			}
			Destroy (this.gameObject);
		}
	}

}
