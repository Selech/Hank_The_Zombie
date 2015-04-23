using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	public Material zombieSkin;

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
		if (pushCooldown < pushCooldownAmount) {
			pushCooldown++;
		}
		if(cooldown > 0){
			cooldown--;
		}
			

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
	}

	public void BoostStamina(){
		boosters++;

		if (boosters == boostersMax) {
			movespeed = 0.04f;
			powerActive = true;
		}
	}

	public void GiveAmmo(int ammo){
		ammoAmount += ammo;
		if (ammoAmount > ammoCapacity) {
			ammoAmount = ammoCapacity;
		}
	}

	private void UpdateSliders(){
		ammoSlider.value = (float)ammoAmount / (float)ammoCapacity;
		pushSlider.value = (float)pushCooldown / (float) pushCooldownAmount;
	}


	public void ActivatePush(){
		float power = (float)pushCooldown / (float) pushCooldownAmount;
		print (power);

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");

		foreach(GameObject en in enemies){
			if(Vector3.Distance(en.transform.position, this.transform.position) < 2f){
				en.gameObject.GetComponent<Rigidbody>().AddForce((en.gameObject.transform.position - transform.position) * (5f * power),ForceMode.Impulse);			
			}
		}

		foreach(GameObject zom in zombies){
			if(Vector3.Distance(zom.transform.position, this.transform.position) < 2f){
				zom.gameObject.GetComponent<Rigidbody>().AddForce((zom.gameObject.transform.position - transform.position) * (3f * power),ForceMode.Impulse);
				zom.gameObject.GetComponent<ZombieScript>().hit = true;

			}
		}

		pushCooldown = 0;
	}

	public void BecomeInfected(){
		MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer> ();

		foreach(MeshRenderer mesh in meshes){
			if(mesh.gameObject.name == "Head" || mesh.gameObject.name == "LeftArm" || mesh.gameObject.name == "RightArm"){
				mesh.material  = zombieSkin;
			}
		}
	}

}	