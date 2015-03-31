using UnityEngine;
using System.Collections;

public class GraplingArmScript : MonoBehaviour, IThrowable {

	bool hit = false;
	private Vector3 target;

	// Use this for initialization
	void Start () {
		target = new Vector3(-1,-1,-1);
		GetComponent<Rigidbody> ().isKinematic = true;
		GetComponent<Rigidbody>().useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != new Vector3(-1,-1,-1)) {
			if(!(Vector3.Distance(transform.position, target) < 0.5f)){
				transform.position = Vector3.MoveTowards(transform.position, target, 0.1f);
				transform.LookAt(target);
			}
			else{
				GetComponent<Rigidbody> ().isKinematic = false;
				GetComponent<Rigidbody>().useGravity = true;
			}
		}
//		else{
//			GetComponent<Rigidbody>().useGravity = true;
//		}

	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag != "Player") {
			hit = true;
		}

		if (other.gameObject.tag == "Player" && hit) {
			Destroy(this.gameObject);
		}
	}

	public void SetTarget(Vector3 target){
//		GetComponent<Rigidbody> ().isKinematic = false;
		this.target = target;
		
	} 
}
