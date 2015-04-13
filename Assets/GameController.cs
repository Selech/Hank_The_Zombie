using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject winScreen;
	public Camera overview;
	public Camera gameplay;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Statics.BottlesLeft == 0) {
			//winScreen.SetActive(true);
		}
	}

	public void GameStart() {
		this.GetComponent<TimeController> ().enabled = true;

		overview.enabled = !overview.enabled;
		gameplay.enabled = !gameplay.enabled;
	}

	public void GameEnd() {
		winScreen.SetActive(true);		
	}
}
