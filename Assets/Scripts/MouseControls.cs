using UnityEngine;
using System.Collections;

public class MouseControls : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		//player.GetComponent<PlayerScript>().setTarget = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {

			if (Input.GetMouseButtonDown (0)) { // if left button pressed...
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.gameObject.tag == "Clickable") {
						player.GetComponent<PlayerScript> ().SetTarget (hit.collider.gameObject.transform.position);
					}
				}
			}

		}

	}
}
