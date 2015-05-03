using UnityEngine;
using System.Collections;

public class BossHitScript : MonoBehaviour {

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Bullet") {
			print ("hit");

			Destroy(other.gameObject);
			this.gameObject.GetComponentInParent<BossScript>().Death();
		}
	}

}
 