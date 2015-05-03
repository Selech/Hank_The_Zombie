using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectableScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player")
		{
//			Statics.BottlesLeft--;
//			print ("Picked up one bottle of experimental cure");
//			GameObject.Find("Score").GetComponent<Text>().text = "Bottles left: " + Statics.BottlesLeft;
			other.gameObject.GetComponent<PlayerScript>().BoostStamina();
			Destroy(this.gameObject);
		}
	}
}
