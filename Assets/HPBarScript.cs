using UnityEngine;
using System.Collections;

public class HPBarScript : MonoBehaviour {

	public float fullHP;
	public float currentHP;
	public GameObject HP;
	public GameObject HPEmpty;

	// Use this for initialization
	void Start () {
		currentHP = fullHP;
	}
	
	// Update is called once per frame
	void Update () {
		HP.transform.localScale = new Vector3 (0.35f * (float)(currentHP/fullHP) ,0.05f, 0.05f);
		//HP.transform.position = HPEmpty.transform.position - new Vector3(0.35f / (float)(currentHP/fullHP),0,0);
	}
}
