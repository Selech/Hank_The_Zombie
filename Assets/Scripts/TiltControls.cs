using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TiltControls : MonoBehaviour {

	public Text test;
	public GameObject hank;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 accel = new Vector3 (Input.acceleration.x/10, Input.acceleration.y/10, 0);

		Vector3 calculated = new Vector3 (Mathf.Sqrt((accel.x*accel.x) + (accel.y*accel.y)),0,Mathf.Sqrt((accel.x*accel.x) + (accel.y*accel.y)));

		//test.text = accel*100 + "";

		hank.transform.Translate(calculated);
	}
}
