using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {

	public static bool PopupOpen = false; 
	
	void OnEnable () 
	{
		PopupOpen = true;
	}

	void OnDisable () 
	{
		PopupOpen = false;
	}
}
