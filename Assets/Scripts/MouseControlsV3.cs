using UnityEngine;
using System.Collections;

public class MouseControlsV3 : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		//player.GetComponent<PlayerScript>().setTarget = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject (0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {
			if (Input.GetMouseButtonDown (0)) { // if left button pressed...
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				//print ();
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.gameObject.tag == "Player") {
						player.GetComponent<PlayerScript>().ActivatePush();
					}
				}
			}


		}
	}
}
