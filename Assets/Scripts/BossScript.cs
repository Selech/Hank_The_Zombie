using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour {

	public Material trans;
	private GameController GameController;
	private bool seen;
	public float moveSpeed = 0.02f;
	private float moveSpeedAway = -0.02f;
	private bool hit = false;
	public GameObject leftArm;
	public GameObject rightArm;
	public GameObject body;
	public GameObject shield;

//	private Vector3 startPoint;
	public GameObject AmmoCratePrefab;

	private Material enemyColor;
	private float colorRate = 5;

	public AudioClip enemyDeath;

	// Use this for initialization
	void Start () {
		seen = false;
		this.GameController = GameObject.Find ("Controller").GetComponent<GameController>();
//		startPoint = transform.position;
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
				enemyColor = body.GetComponent<MeshRenderer>().material;
				//print (enemyColor.color.a);

				if(colorRate <= 1.0f){
					colorRate = Mathf.MoveTowards(colorRate, 0.1f, 0.01f);
				}
				else{
					colorRate = Mathf.MoveTowards(colorRate, 1.0f, 0.5f);
				}

				enemyColor.color = new Color(enemyColor.color.r,enemyColor.color.g,enemyColor.color.b, colorRate);
				leftArm.GetComponent<MeshRenderer>().material = enemyColor;
				rightArm.GetComponent<MeshRenderer>().material = enemyColor;

				if(colorRate == 0.1f){
					this.GetComponent<Rigidbody>().isKinematic = true;
					body.GetComponent<BoxCollider>().enabled = false;

					shield.GetComponent<BoxCollider>().enabled = false;
				}
			}
		}
	}

//	public void SetStartPoint(Vector3 startPoint){
//		this.startPoint = startPoint;
//	}

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
		
//		if (other.gameObject.tag == "Enemy") {
//			startPoint = this.transform.position;
//		}
	}

	public void Death(){
		AudioSource.PlayClipAtPoint (enemyDeath, GameObject.Find("Main Camera").GetComponent<Transform>().position);
		
		if(Random.Range(0,3) == 0){
			GameObject ammocrate = Instantiate(AmmoCratePrefab);
			ammocrate.transform.position = new Vector3(this.transform.position.x, 0.1f, this.transform.position.z);
		}
		
		hit = true;
		
		body.GetComponent<MeshRenderer>().material = trans;
	}
}
 