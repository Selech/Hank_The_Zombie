using UnityEngine;
using System.Collections;

public class WallSwitchScript : MonoBehaviour {

	public GameObject switchObject;
	private float nonActiveZ;
	public float activeZ;

	void Start(){
		nonActiveZ = this.transform.localScale.z;
	}

	void OnCollisionEnter(){
		Vector3 scale = new Vector3 (this.transform.localScale.x, this.transform.localScale.y, activeZ);
		this.transform.localScale = scale;
		
		switchObject.SetActive(!switchObject.activeSelf);
	}
}
