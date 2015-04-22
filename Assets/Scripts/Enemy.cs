using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private MouseControls GameController;
	private bool seen;
	private float moveSpeed = 0.02f;
	private float moveSpeedAway = -0.02f;
	private bool hit = false;
	public GameObject hat;
	public GameObject leftArm;
	public GameObject rightArm;
	private Vector3 startPoint;
	public GameObject AmmoCratePrefab;

	private Material enemyColor;
	private float colorRate = 5;

	// Use this for initialization
	void Start () {
		seen = false;
		this.GameController = GameObject.Find ("Controller").GetComponent<MouseControls>();
		startPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Enemy er i live, og bevæger sig
		if (this.GameController.player != null && !hit) {
			Vector3 TargetPlayer = new Vector3 (this.GameController.player.transform.position.x, transform.position.y, this.GameController.player.transform.position.z);

			if (this.GameController.player.GetComponent<PlayerScript> ().powerActive == true) {
				moveSpeed = moveSpeedAway;
			}

			if (Vector3.Distance (TargetPlayer, transform.position) < 3.5f || seen) {

				seen = true;

				if (!hit) {
					transform.position = Vector3.MoveTowards (transform.position, TargetPlayer, moveSpeed);
					transform.LookAt (TargetPlayer);
				}
			} 
		} 

		//Enemy er blevet skudt, eller player er død.
		else {
			if(colorRate > 0.1f){
				//print (colorRate);
				enemyColor = this.GetComponent<MeshRenderer>().material;
				//print (enemyColor.color.a);
				if(colorRate <= 1.0f){
					colorRate = Mathf.MoveTowards(colorRate, 0.1f, 0.01f);
				}
				else{
					colorRate = Mathf.MoveTowards(colorRate, 1.0f, 0.5f);
				}
				print (colorRate);
				enemyColor.color = new Color(enemyColor.color.r,enemyColor.color.g,enemyColor.color.b, colorRate);
				leftArm.GetComponent<MeshRenderer>().material = enemyColor;
				rightArm.GetComponent<MeshRenderer>().material = enemyColor;

				if(colorRate == 0.1f){
					this.GetComponent<Rigidbody>().isKinematic = true;
					this.GetComponent<BoxCollider>().enabled = false;
				}
			}
		}
	}

	public void SetStartPoint(Vector3 startPoint){
		this.startPoint = startPoint;
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player" && !hit) {
			if(this.GameController.player.GetComponent<PlayerScript>().powerActive == true){
				if(Random.Range(0,3) == 0){
					GameObject ammocrate = Instantiate(AmmoCratePrefab);
					ammocrate.transform.position = new Vector3(this.transform.position.x, 0.1f, this.transform.position.z);
				}
				Destroy (this.gameObject);
			}
			else{
				other.gameObject.GetComponent<PlayerScript>().enabled = false;
				other.gameObject.GetComponent<Rigidbody>().AddForce((other.gameObject.transform.position - transform.position)*500);
				this.GameController.player = null;
			}
		}

		if (other.gameObject.tag == "Bullet" && !hit) {
			Destroy(other.gameObject);

			if(Random.Range(0,3) == 0){
				GameObject ammocrate = Instantiate(AmmoCratePrefab);
				ammocrate.transform.position = this.transform.position;
			}
			hit = true;

//			hat.transform.SetParent(null);
//			hat.GetComponent<Rigidbody>().isKinematic = false;
//			hat.GetComponent<Rigidbody>().AddForce(new Vector3(0,100f,0));
		}

		
		if (other.gameObject.tag == "Enemy") {
			startPoint = this.transform.position;
		}
	}

}
 