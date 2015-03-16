using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject winScreen;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Statics.BottlesLeft == 0) {
			winScreen.SetActive(true);		
		}


	}
}
