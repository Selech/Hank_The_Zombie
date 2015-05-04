using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject){
			Vector3 target = new Vector3 (player.transform.position.x, player.transform.position.y + 3.5f, player.transform.position.z -2f);
			transform.position = Vector3.Lerp(this.transform.position, target, 0.5f);

//			transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + 3f, player.transform.position.z -1.5f);
			//transform.position = new Vector3 (player.transform.position.x - 1.2f, player.transform.position.y + 2.5f, player.transform.position.z - 1.2f);
			transform.LookAt (player.transform.position);
		}
	}
}
