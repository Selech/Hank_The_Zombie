using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

	public GameObject target;
	public Canvas won;

	// Use this for initialization
	void Start () {
		Statics.LevelWon = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (target != null) {
			transform.position = new Vector3 (transform.position.x, 0.05f, transform.position.z);
				Vector3 targetposition = new Vector3 (target.transform.position.x, 0.05f, target.transform.position.z);
				transform.position = Vector3.MoveTowards (transform.position, targetposition, 0.05f);
		}
	}	


	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name == "Trap(Clone)") {
			//Destroy(this.gameObject);
			Application.LoadLevel(Application.loadedLevel);
		}

		if (collision.gameObject.name == "Goal(Clone)") {
			won.gameObject.SetActive(true);

			Statics.LevelWon = true;

			collision.gameObject.animation.Play("RemoveTrapAnimation");
			collision.gameObject.SetActive(false);
			//Destroy(this.gameObject);
			StartCoroutine("RemoveMap");
		}
	
	}

	IEnumerator RemoveMap() {

		Transform[] tiles = GameObject.Find ("Map").GetComponentsInChildren<Transform> ();

		for (int i = tiles.Length -1; i >= 0; i--) {
			TileScript ts = tiles[i].GetComponent<TileScript>();

			if (tiles[i].gameObject.name == "Road(Clone)" && ts.spawned) {
				tiles[i].animation.Play("RemoveAnimation");
			}

			else if (tiles[i].gameObject.name == "Trap(Clone)" && ts.spawned) {
				tiles[i].animation.Play("RemoveTrapAnimation");
			}

			else if (tiles[i].gameObject.name == "Goal(Clone)" && ts.spawned) {
				tiles[i].animation.Play("RemoveTrapAnimation");
			}
				
			//yield return new WaitForSeconds(0.001f);
		}
		yield return new WaitForSeconds(1.0f);

		//won.gameObject.SetActive(false);
		Application.LoadLevel(Application.loadedLevel+1);
	}


}	