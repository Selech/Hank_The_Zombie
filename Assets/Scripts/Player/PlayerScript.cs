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
	public GameObject crosshair;
	private Vector3 target;
	private Vector3 targetVector;
	public bool shoot;
	public int ammoAmount;
	public int ammoCapacity;
	public Slider ammoSlider;

	public int pushCooldown;
	public int pushCooldownAmount;
	public Slider pushSlider;

	public AudioClip step;
	public AudioClip push;

	public ParticleSystem pushParticle;
	public ParticleSystem gunParticle;

	public bool powerActive = false;

	// Use this for initialization
	void Start ()
	{
		Initialize ();
	}

	public void Initialize()
	{
		if (LevelDesigner.isNotTesting == false) 
		{
			// Find and assign Exit
			ammoSlider = GameObject.Find ("SliderAmmo").GetComponent<Slider>();
			
			// Find and assign Exit
			pushSlider = GameObject.Find ("SliderPush").GetComponent<Slider>();

			cooldown = cooldownAmount;
			pushCooldown = pushCooldownAmount;
			
			target = this.transform.position;
			
			UpdateSliders ();
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (LevelDesigner.isNotTesting == false) 
		{
			PcControls ();
			//transform.position = new Vector3 (this.transform.position.x, 1f, this.transform.position.z);
			if (pushCooldown < pushCooldownAmount) {
				pushCooldown++;
			}
			
			
			
			if ((Input.GetKey (KeyCode.Space)) ) {
				Shoot();
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
		crosshair.SetActive (true);
		if(cooldown > 0){
			cooldown--;
		}

		if (cooldown <= 0) {
			if (ammoAmount > 0) {
				ammoAmount--;

				GameObject bullet = (GameObject)Instantiate (bulletPrefab);
				bullet.transform.position = direction.transform.position;
				bullet.GetComponent<BulletScript> ().direction = (direction.transform.position - gun.transform.position).normalized;

				gunParticle.Play();

				cooldown = cooldownAmount;
			}
		}
	}

	public void NotShoot(){
		crosshair.SetActive (false);

		shoot = false;
		cooldown = cooldownAmount;
	}

	public void SetTarget (Vector3 gameobj)
	{
		target = gameobj;
	}

	public void BoostStamina()
	{
		boosters++;

		if (boosters == boostersMax) 
		{
			movespeed = 0.04f;
			powerActive = true;
		}
	}

	public void GiveAmmo(int ammo)
	{
		ammoAmount += ammo;
		if (ammoAmount > ammoCapacity) 
		{
			ammoAmount = ammoCapacity;
		}
	}

	private void UpdateSliders()
	{
		//ammoSlider = GameObject.Find ("");
		ammoSlider.value = (float)ammoAmount / (float)ammoCapacity;
		pushSlider.value = (float)pushCooldown / (float) pushCooldownAmount;
	}


	public void ActivatePush(){
		AudioSource.PlayClipAtPoint (push, GameObject.Find("Main Camera").GetComponent<Transform>().position);

		float power = (float)pushCooldown / (float) pushCooldownAmount;
		//pushParticle.startSpeed = 5 * power;

		ParticleSystem tempPush = (ParticleSystem) Instantiate (pushParticle, this.transform.position, new Quaternion());
		tempPush.emissionRate = 1000 * power;
		tempPush.Play ();

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
		GameObject[] destr = GameObject.FindGameObjectsWithTag("Destructable");

		foreach(GameObject en in enemies){
			if(Vector3.Distance(en.transform.position, this.transform.position) < 2f){
				en.gameObject.GetComponent<Rigidbody>().AddForce((en.gameObject.transform.position - transform.position) * (5f * power),ForceMode.Impulse);			
			}
		}

		foreach(GameObject zom in zombies){
			if(Vector3.Distance(zom.transform.position, transform.position) < 3f){
				zom.gameObject.GetComponent<Rigidbody>().AddForce((zom.gameObject.transform.position - transform.position) * (3f * power),ForceMode.Impulse);
				zom.gameObject.GetComponent<ZombieScript>().hit = true;
			}
		}

		foreach(GameObject des in destr){
			if(Vector3.Distance(des.transform.position, transform.position) < 1.5f){
				des.gameObject.GetComponent<Rigidbody>().isKinematic = false;
				des.gameObject.GetComponent<Rigidbody>().AddForce((des.gameObject.transform.position - transform.position) * (10f),ForceMode.Impulse);
				des.gameObject.GetComponent<RemoveTimer>().StartTimer(100);
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