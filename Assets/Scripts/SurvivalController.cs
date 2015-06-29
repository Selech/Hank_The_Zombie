using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SurvivalController : MonoBehaviour {

	public GameObject loseScreen;
	public GameObject player;
	public Text scoreText;

	public SpawnPointScript spawnScript;

	public Text diedText;

	// Use this for initialization
	void Start () {
		Statics.SurvivalScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(!loseScreen.activeSelf && !player){
			loseScreen.SetActive(true);
		}

		spawnScript.rate = 100 - (Statics.SurvivalScore/10);
		scoreText.text = "SurvivalScore: " + Statics.SurvivalScore;

		diedText.text = "Score: " + Statics.SurvivalScore;
	}

	
}
