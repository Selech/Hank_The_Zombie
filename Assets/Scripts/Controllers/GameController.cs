using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject winScreen;
	public GameObject loseScreen;

	//public Camera overview;
	public Camera gameplay;

	public GameObject player;
	public GameObject door;

	public void Initialize()
	{
		// Find and assign player
		player = GameObject.Find ("CubeHank(Clone)");
		if (player == null)
			player = GameObject.Find ("CubeHank-NoGun(Clone)");

		player.GetComponent<PlayerScript> ().Initialize ();
		player.GetComponent<Controller3DPort> ().Initialize ();

		// Find and assign Exit
		door = GameObject.Find ("EndDoor(Clone)");

		gameplay.enabled = true;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Statics.BatteriesLeft == 0 && door != null) 
		{
			if(LevelDesigner.isUsingEditor == false)
			{
				//winScreen.SetActive(true);
				door.SetActive(false);
			}
		}

		if (loseScreen != null) 
		{
			if(winScreen != null)
			{
				if(!loseScreen.activeSelf && !player && !winScreen.activeSelf)
				{
					loseScreen.SetActive(true);
				}
			}
			else
			{
				if(!loseScreen.activeSelf && !player)
				{
					loseScreen.SetActive(true);
				}
			}
		}
	}

	public void GameStart() {
		//this.GetComponent<TimeController> ().enabled = true;

//		overview.enabled = !overview.enabled;
//		gameplay.enabled = !gameplay.enabled;
	}

	public void GameEnd() 
	{
		winScreen.SetActive(true);		
	}

	public void LoadLevel(string levelName){
		Statics.BatteriesLeft = 0;
		Application.LoadLevel (levelName);
	}

	//Used in tutorial 3!
	public void Infected()
	{
		this.GetComponent<ActivatorScript> ().ActivateGameobjectsOnce ();
		player.GetComponent<PlayerScript> ().BecomeInfected ();
	}
}
