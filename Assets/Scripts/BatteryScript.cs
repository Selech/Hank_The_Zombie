using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatteryScript : MonoBehaviour {

	public AudioClip powerdown;

	// Use this for initialization
	void Start () {
		Statics.BatteriesLeft++;
		GameObject.Find("Score").GetComponent<Text>().text = "Batteries left: " + Statics.BatteriesLeft;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player") {
			AudioSource.PlayClipAtPoint (powerdown, GameObject.Find("Main Camera").GetComponent<Transform>().position);

			Statics.BatteriesLeft--;
			GameObject.Find("Score").GetComponent<Text>().text = "Batteries left: " + Statics.BatteriesLeft;
			GameObject.Find("BatteryFlash").GetComponent<Text>().text = "Batteries left: " + Statics.BatteriesLeft;
			GameObject.Find("BatteryFlash").GetComponent<Animation>().Play("Flash");

			Destroy (this.gameObject);
		}
	}
}
