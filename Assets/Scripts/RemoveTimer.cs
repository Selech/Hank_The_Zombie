using UnityEngine;
using System.Collections;

public class RemoveTimer : MonoBehaviour {

	private bool started;
	int countdown;

	// Update is called once per frame
	void Update () {
		if (started) {
			countdown--;

			if(countdown <= 0){
				Destroy(this.gameObject);
			}
		}
	}

	public void StartTimer(int time){
		started = true;

		countdown = time;
	}
}
