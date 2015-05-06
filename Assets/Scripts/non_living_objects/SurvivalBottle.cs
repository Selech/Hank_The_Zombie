using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SurvivalBottle : MonoBehaviour {

	public int score;

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
			Statics.SurvivalScore += score;

			GameObject newBottle = (GameObject) Instantiate(this.gameObject);
			Vector3 newPosition = new Vector3();

			newPosition.x = Random.Range(0,20);
			newPosition.z = Random.Range(0,15);

			newBottle.transform.position = newPosition;

			Destroy(this.gameObject);
		}
	}
}
