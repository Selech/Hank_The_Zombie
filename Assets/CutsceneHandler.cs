using UnityEngine;
using System.Collections;

public class CutsceneHandler : MonoBehaviour {

	public string nextLevel;

	public void TurnDownMusic(){
		StartCoroutine (TurnDownAndLoad());
	}

	public void LoadNextLevel(){
		Application.LoadLevel (nextLevel);
	}

	private IEnumerator TurnDownAndLoad(){
		yield return new WaitForSeconds(0.1f);

		for(int i = 0; i <= 5; i++){
			this.GetComponent<AudioSource>().volume -= 0.2f;
			yield return new WaitForSeconds(0.5f);
		}
		yield return new WaitForSeconds(0.5f);

		LoadNextLevel();
	}
}
