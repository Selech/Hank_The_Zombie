using UnityEngine;
using System.Collections;

public class LoadingLevelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LevelButtonClicked(string levelToLoad){
		Application.LoadLevel (levelToLoad);
	}
}
