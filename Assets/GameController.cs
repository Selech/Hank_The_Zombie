using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject winScreen;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Statics.BottlesLeft == 0) {
		}
	}

	public void GameEnd() {
		winScreen.SetActive(true);		
	}
}
