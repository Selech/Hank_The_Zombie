using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	// Screens
	public GameObject winScreen;
	public GameObject loseScreen;

	// Player
	public GameObject player;

	// Door, if placed
	public GameObject door;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("Ammo") == 0 || PlayerPrefs.GetInt ("Ammo") == 0) {
			PlayerPrefs.SetInt ("Ammo",0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Statics.BatteriesLeft == 0 && door != null) {
			door.SetActive(false);
		}

		if (loseScreen != null) {
			if(winScreen != null){
				if(!loseScreen.activeSelf && !player && !winScreen.activeSelf){
					loseScreen.SetActive(true);
				}
			}else{
				if(!loseScreen.activeSelf && !player){
					loseScreen.SetActive(true);
				}
			}
		}
	}

	public void GameStart() {
	}

	public void GameEnd() {
		PlayerPrefs.SetInt ("Ammo",player.GetComponent<PlayerScript>().ammoAmount);

		winScreen.SetActive(true);		
		Destroy (player);
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
