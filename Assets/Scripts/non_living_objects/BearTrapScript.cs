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

			//gameObject.GetComponent<Animator> ().SetBool ("Hit",true);
			//other.gameObject.GetComponent<PlayerScript>().SetTarget(other.gameObject);
			other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,4,0),ForceMode.VelocityChange);


		}
	}
}
