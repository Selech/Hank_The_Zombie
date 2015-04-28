using UnityEngine;
using System.Collections;

public class ActivatorScript : MonoBehaviour {

	public GameObject[] activate;
	public bool activatedOnce;

	public void ActivateGameobjects(){
		foreach (GameObject go in activate) {
			if(go != null){
				go.SetActive(true);
			}
		}
	}

	public void ActivateGameobjectsOnce(){
		if(!activatedOnce){
			activatedOnce = true;
			foreach (GameObject go in activate) {
				if(go != null){
					go.SetActive(true);
				}
			}
		}
	}
}
