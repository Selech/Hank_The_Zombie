using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject) {
			transform.position = new Vector3 (player.transform.position.x -	2, player.transform.position.y + 4, player.transform.position.z - 2);
			transform.LookAt (player.transform.position);
		}
	}
}
