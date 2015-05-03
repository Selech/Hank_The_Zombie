using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject winScreen;
	public GameObject loseScreen;

	public GameObject player;

	public Camera overview;
	public Camera gameplay;
	public GameObject door;

	// Use this for initialization
	void Start () {
		gameplay.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Statics.BatteriesLeft == 0 && door != null) {
			//winScreen.SetActive(true);
			door.SetActive(false);
		}

		if (loseScreen != null) {
			if(!loseScreen.activeSelf && !player && !winScreen.activeSelf){
				loseScreen.SetActive(true);
			}
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
		Statics.BatteriesLeft = 0;
		Application.LoadLevel (levelName);
	}

	//Used in tutorial 3!
	public void Infected(){
		this.GetComponent<ActivatorScript> ().ActivateGameobjectsOnce ();
		player.GetComponent<PlayerScript> ().BecomeInfected ();
	}
}
