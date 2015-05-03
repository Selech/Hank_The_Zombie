using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!this.GetComponent<ParticleSystem> ().IsAlive ()) {
			Destroy(this.gameObject);
		}
	}
}
