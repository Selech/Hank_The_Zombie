using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject winScreen;
	public Camera overview;
	public Camera gameplay;
	public GameObject door;

	// Use this for initialization
	void Start () {
		gameplay.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Statics.BatteriesLeft == 0) {
			//winScreen.SetActive(true);
			door.SetActive(false);
		}
	}

	public void GameStart() {
		//this.GetComponent<TimeController> ().enabled = true;

//		overview.enabled = !overview.enabled;
//		gameplay.enabled = !gameplay.enabled;
	}

	public void GameEnd() {
		winScreen.SetActive(true);		
	}

	public void LoadLevel(string levelName){
		Application.LoadLevel (levelName);
	}
}
