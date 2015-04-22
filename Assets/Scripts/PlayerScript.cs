using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	private int boosters;
	private int boostersMax = 2;
	public int cooldownAmount;
	private int cooldown;
	private float movespeed = 0.022f;
	public GameObject direction;
	public GameObject gun;
	public GameObject bulletPrefab;
	private Vector3 target;
	private Vector3 targetVector;
	public Canvas won;
	public RectTransform ActionUI;
	private GameObject selectedObject;
	public GameObject EquippedThrowable;
	private bool Sliding;
	public bool shoot;
	public int ammoAmount;
	public int ammoCapacity;
	public Slider ammoSlider;

	public int pushCooldown;
	public int pushCooldownAmount;
	public Slider pushSlider;


	public bool powerActive = false;

	// Use this for initialization
	void Start ()
	{
		cooldown = cooldownAmount;
		pushCooldown = pushCooldownAmount;

		target = this.transform.position;

		UpdateSliders ();
	}

	public void EquipThrowable (GameObject throwable)
	{
		EquippedThrowable = (GameObject)Instantiate (throwable, transform.position, new Quaternion ());
	}

	// Update is called once per frame
	void Update ()
	{
		PcControls ();
		//transform.position = new Vector3 (this.transform.position.x, 1f, this.transform.position.z);
		pushCooldown++;
		cooldown--;



		if ((Input.GetKey (KeyCode.Space) || shoot) && cooldown <= 0) {
			if(ammoAmount > 0){
				ammoAmount--;

				GameObject bullet = (GameObject) Instantiate(bulletPrefab);
				bullet.transform.position = direction.transform.position;
				bullet.GetComponent<BulletScript>().direction = (direction.transform.position - gun.transform.position).normalized;
				cooldown = cooldownAmount;

			}
		}

		if (target != new Vector3 (-1, -1, -1)) {
			//transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			
			Vector3 targetposition = new Vector3 (target.x, transform.position.y, target.z);

			if(shoot){
				transform.position = Vector3.MoveTowards (transform.position, targetposition, movespeed*0.4f); //speed 0.015
			}
			else{
				transform.position = Vector3.MoveTowards (transform.position, targetposition, movespeed); //speed 0.015
			}

			if (!Input.GetKey (KeyCode.Space) && !shoot) {
				transform.LookAt (targetposition);
			}
			
			if (transform.position == targetposition) {
				target = new Vector3 (-1, -1, -1);
			}
		} else {

		}
		UpdateSliders ();
	}

	private void PcControls(){
		Vector3 tempVector = new Vector3 ();
		if (Input.GetKey (KeyCode.W)) {
			tempVector += new Vector3(0,0,1f);
		}
		if (Input.GetKey (KeyCode.S)) {
			tempVector += new Vector3(0,0,-1f);
		}
		if (Input.GetKey (KeyCode.A)) {
			tempVector += new Vector3(-1f,0,0);
		}
		if (Input.GetKey (KeyCode.D)) {
			tempVector += new Vector3(1f,0,0);
		}


		if(tempVector != new Vector3())
			SetTarget (this.transform.position + tempVector);
	}

	public void Shoot(){
		shoot = true;

		if (cooldown <= 0) {
			if (ammoAmount > 0) {
				ammoAmount--;

				GameObject bullet = (GameObject)Instantiate (bulletPrefab);
				bullet.transform.position = direction.transform.position;
				bullet.GetComponent<BulletScript> ().direction = (direction.transform.position - gun.transform.position).normalized;
				cooldown = cooldownAmount;
			}
		}
	}

	public void NotShoot(){
		shoot = false;
	}

	public void SetTarget (Vector3 gameobj)
	{
		target = gameobj;
//
//		if (EquippedThrowable != null) {
//			EquippedThrowable.GetComponent<IThrowable> ().SetTarget (gameobj);
//			EquippedThrowable = null;
//		} else {
//			if (!Sliding) {
//			}
//		}
	}

	public void BoostStamina(){
		boosters++;

		if (boosters == boostersMax) {
			movespeed = 0.04f;
			powerActive = true;
		}
	}

	public void GiveAmmo(){
		ammoAmount += 10;
	}

	private void UpdateSliders(){
		ammoSlider.value = (float)ammoAmount / (float)ammoCapacity;
		pushSlider.value = (float)pushCooldown / (float)pushCooldownAmount;
	}


	public void ActivatePush(){
		if(pushCooldown >= pushCooldownAmount){
			pushCooldown = 0;
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			
			foreach(GameObject en in enemies){
				if(Vector3.Distance(en.transform.position, this.transform.position) < 2f){
					en.gameObject.GetComponent<Rigidbody>().AddForce((en.gameObject.transform.position - transform.position)*200);			
				}
			}
		}
	}

}	