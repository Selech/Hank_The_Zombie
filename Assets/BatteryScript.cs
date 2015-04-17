using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatteryScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Statics.BatteriesLeft++;
		GameObject.Find("Score").GetComponent<Text>().text = "Batteries left: " + Statics.BatteriesLeft;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other){
		print (other.gameObject.tag);
		if (other.gameObject.tag == "Player") {
			Statics.BatteriesLeft--;
			GameObject.Find("Score").GetComponent<Text>().text = "Batteries left: " + Statics.BatteriesLeft;

			Destroy (this.gameObject);
		}
	}
}
