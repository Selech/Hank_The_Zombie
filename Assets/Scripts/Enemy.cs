using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private MouseControls GameController;

	// Use this for initialization
	void Start () {
		this.GameController = GameObject.Find ("Controller").GetComponent<MouseControls>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 TargetPlayer = new Vector3(this.GameController.player.transform.position.x, transform.position.y, this.GameController.player.transform.position.z);
		transform.position = Vector3.MoveTowards (transform.position, TargetPlayer, 0.008f);
		transform.LookAt (TargetPlayer);
	}
}
 