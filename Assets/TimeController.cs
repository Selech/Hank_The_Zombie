using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeController : MonoBehaviour {

	public float time;
	public Text timeField;

	public bool started = false;
	private float startTime;

	// Use this for initialization
	void OnEnable () {
		startTime = Time.time;

		started = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (started) {
			float timeelapsed = Time.time - startTime; 
			
			if ((time - timeelapsed) < 0) {
				print ("Lost");
			}
			
			timeField.text = "Time left: " + (int)(time - timeelapsed) + "s";
		}
	}
}
