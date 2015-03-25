using UnityEngine;
using System.Collections;

public class WallSwitchScript : MonoBehaviour
{

	public GameObject switchObject;
	private float nonActiveZ;
	public float activeZ;
	public int isSet = 0;
	void Start ()
	{
		nonActiveZ = this.transform.localScale.z;
	}

	void OnCollisionExit ()
	{
		isSet--;
		if (isSet == 0) {
			switchObject.SetActive (true);
		}

	}


	void OnCollisionEnter ()
	{
		isSet++;
		Vector3 scale = new Vector3 (this.transform.localScale.x, this.transform.localScale.y, activeZ);
		this.transform.localScale = scale;
		switchObject.SetActive (false);
	}
}
