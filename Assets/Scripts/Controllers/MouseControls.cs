﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseControls : MonoBehaviour
{
	public GameObject player;
	public GameObject InventoryUI;
	public Button touchArea;
	public Button shootArea;

	// Use this for initialization
	void Start ()
	{
		//player.GetComponent<PlayerScript>().setTarget = null;
	}

	// Update is called once per frame
	void Update ()
	{
		if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject (0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {

			if (Input.GetMouseButtonDown (0)) { // if left button pressed...
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.gameObject.tag == "Clickable") {
						player.GetComponent<PlayerScript> ().SetTarget (hit.collider.gameObject.transform.position);
					}

					if (hit.collider.gameObject.tag == "Attackable") {
						if (Vector3.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, hit.collider.gameObject.transform.position) > 1) {
							GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ().SetTarget (hit.collider.gameObject.transform.position);
						} else {
							hit.collider.gameObject.GetComponent<IAttackable> ().Attack ();
						}
					}

					if (hit.collider.gameObject.tag == "Player") {
						InventoryUI.SetActive (true);
					}
				}
			}
		} else if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject (0) || UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {
			if (Input.GetMouseButton (0)) { // if left button pressed...
				Vector3 mousePos = Input.mousePosition;
				if (isInsideTouch (mousePos, touchArea)) {
					Vector3 target = Input.mousePosition - touchArea.transform.position;

//					print (target);
//
//					//target = Quaternion.Euler(0, 0, -45) * target;
//
//					print (target);

					Vector3 calculatedTarget = new Vector3 (player.transform.position.x + target.x, 0, player.transform.position.z + target.y);

					//print (calculatedTarget);

					player.GetComponent<PlayerScript> ().SetTarget (calculatedTarget);
				}

				if (isInsideTouch (mousePos, shootArea)) {
					player.GetComponent<PlayerScript> ().shoot = true;
				} else {
					player.GetComponent<PlayerScript> ().shoot = false;
				}
			} else {
				player.GetComponent<PlayerScript> ().shoot = false;
				player.GetComponent<PlayerScript> ().SetTarget (new Vector3 (-1, -1, -1));
			}
		} else {
			player.GetComponent<PlayerScript> ().shoot = false;
			player.GetComponent<PlayerScript> ().SetTarget (new Vector3 (-1, -1, -1));
		}
	}

	private bool isInsideTouch(Vector3 pos, Button but){
		if(pos.x - but.transform.position.x < but.GetComponent<RectTransform>().rect.width/2 &&
		   but.transform.position.x - pos.x < but.GetComponent<RectTransform>().rect.width/2 &&
		   pos.y - but.transform.position.y < but.GetComponent<RectTransform>().rect.height/2 &&
		   but.transform.position.y - pos.y < but.GetComponent<RectTransform>().rect.height/2){
			return true;
		}
		else{
			return false;
		}
	}
}
