using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

	public bool spawned = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.Find("Player");

//		if (!spawned && Vector3.Distance (player.transform.position, this.transform.position) < 2f && !Statics.LevelWon) {
//			if(gameObject.name == "Road(Clone)")
//				animation.Play("DrawAnimation");
//
//			if(gameObject.name == "Goal(Clone)" || gameObject.name == "Trap(Clone)")
//				animation.Play("DrawTrapAnimation");
//
//			spawned = true;
//		}
//
//		if (spawned && Vector3.Distance (player.transform.position, this.transform.position) > 2f && !Statics.LevelWon) {
//			if (gameObject.name == "Road(Clone)")
//					animation.Play ("RemoveAnimation");
//
//			if (gameObject.name == "Goal(Clone)" || gameObject.name == "Trap(Clone)")
//					animation.Play ("RemoveTrapAnimation");
//
//			spawned = false;
//		}
	}
}
