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
					print (hit.collider.gameObject.tag);

					if (hit.collider.gameObject.tag == "Clickable") {
						player.GetComponent<PlayerScript> ().SetTarget (hit.collider.gameObject.transform.position);
					}

					if (hit.collider.gameObject.tag == "Attackable") {
						if (Vector3.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, hit.collider.gameObject.transform.position) > 1) {
							GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ().SetTarget (hit.collider.gameObject.transform.position);
						} else {
							hit.collider.gameObject.GetComponent<LabTableWithLaptop>().Clicked();
						}
					}
				}
			}

		}

	}
}
