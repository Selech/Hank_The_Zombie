using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour {

	public Material trans;
	private GameController GameController;
	private bool seen;
	private float moveSpeed = 0.01f;
	public bool hit = false;
	public GameObject leftArm;
	public GameObject rightArm;
	private Vector3 startPoint;

	private Material enemyColor;
	private float colorRate = 5;

	// Use this for initialization
	void Start () {
		seen = false;
		this.GameController = GameObject.Find ("Controller").GetComponent<GameController>();
		startPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Enemy er i live, og bevæger sig
		if (this.GameController.player != null && !hit) {
			Vector3 TargetPlayer = new Vector3 (this.GameController.player.transform.position.x, transform.position.y, this.GameController.player.transform.position.z);

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
				this.GetComponent<MeshRenderer>().material = trans;


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
			//other.gameObject.GetComponent<PlayerScript> ().enabled = false;
			other.gameObject.GetComponent<Rigidbody> ().AddForce ((other.gameObject.transform.position - transform.position));
			//this.GameController.player = null;


			this.GameController.Infected();
		}

		if (other.gameObject.tag == "Bullet" && !hit) {
			Destroy(other.gameObject);
			//hit = true;

		}
	}

}
 