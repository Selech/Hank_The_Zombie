using UnityEngine;
using System.Collections;

public class BossHitScript : MonoBehaviour {

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Bullet") {
			Destroy(other.gameObject);
			this.gameObject.GetComponentInParent<BossScript>().Death();
		}
	}

}
 