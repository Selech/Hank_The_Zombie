using UnityEngine;
using System.Collections;

public class BearTrapScript : MonoBehaviour {

	Animator anim;
	int triggerHash = Animator.StringToHash("Hit");

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player")
		{
			anim.SetTrigger(triggerHash);
			other.gameObject.GetComponent<PlayerScript>().enabled = false;
			//gameObject.GetComponent<Animator> ().SetBool ("Hit",true);
			//other.gameObject.GetComponent<PlayerScript>().SetTarget(other.gameObject);
			Vector3 force = other.transform.position - this.transform.position;
			other.gameObject.GetComponent<Rigidbody>().AddForce(force*2 + new Vector3(0,3,0),ForceMode.VelocityChange);
			other.gameObject.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.right * 5);

		}
	}
}
