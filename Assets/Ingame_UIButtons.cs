using UnityEngine;
using System.Collections;

public class Ingame_UIButtons : MonoBehaviour {

	public void goToLevelSelection(){
		Application.LoadLevel ("LevelSelection");
	}

	public void restartLevel(){
		Application.LoadLevel (Application.loadedLevel);
	}

}
