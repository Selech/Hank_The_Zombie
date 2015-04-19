using UnityEngine;
using System.Collections;

public class MoveableBoxScript : MonoBehaviour {

	public bool moveableX;
	public bool moveableZ;

	public int maxX;
	public int minX;
	public int maxZ;
	public int minZ;


	// Use this for initialization
	void Start () {
		if (moveableX) {
			GetComponent<Rigidbody>().constraints -= RigidbodyConstraints.FreezePositionX;
		}

		if (moveableZ) {
			print (moveableZ);
			GetComponent<Rigidbody>().constraints -= RigidbodyConstraints.FreezePositionZ;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
