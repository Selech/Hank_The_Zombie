using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	private int boosters;
	private int boostersMax = 2;
	public int cooldownAmount;
	private int cooldown;
	private float movespeed = 0.02f;
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

	public bool powerActive = false;

	// Use this for initialization
	void Start ()
	{
		cooldown = cooldownAmount;
		target = this.transform.position;
	}

	public void EquipThrowable (GameObject throwable)
	{
		EquippedThrowable = (GameObject)Instantiate (throwable, transform.position, new Quaternion ());
	}

	// Update is called once per frame
	void Update ()
	{
		//transform.position = new Vector3 (this.transform.position.x, 1f, this.transform.position.z);

		cooldown--;

		if (Sliding) {
			targetVector -= (transform.position - targetVector) / 100;
			transform.position = Vector3.MoveTowards (transform.position, targetVector, movespeed);
			transform.LookAt (targetVector);
			

		} else {
			if ((Input.GetKey (KeyCode.Space) || shoot) && cooldown <= 0) {
				GameObject bullet = (GameObject) Instantiate(bulletPrefab);
				bullet.transform.position = direction.transform.position;
				bullet.GetComponent<BulletScript>().direction = (direction.transform.position - gun.transform.position).normalized;
				cooldown = cooldownAmount;
			}

			if (target != new Vector3 (-1, -1, -1)) {
				//transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
				
				Vector3 targetposition = new Vector3 (target.x, transform.position.y, target.z);
				transform.position = Vector3.MoveTowards (transform.position, targetposition, movespeed); //speed 0.015

				if (!Input.GetKey (KeyCode.Space) && !shoot) {
					transform.LookAt (targetposition);
				}
				
				if (transform.position == targetposition) {
					target = new Vector3 (-1, -1, -1);
				}
			} else {

			}
		}
	}

	public void Shoot(){
		shoot = true;
		if(cooldown <= 0){
			GameObject bullet = (GameObject) Instantiate(bulletPrefab);
			bullet.transform.position = direction.transform.position;
			bullet.GetComponent<BulletScript>().direction = (direction.transform.position - gun.transform.position).normalized;
			cooldown = cooldownAmount;
		}
	}

	public void NotShoot(){
		shoot = false;
	}

	public void SetTarget (Vector3 gameobj)
	{
		if (EquippedThrowable != null) {
			EquippedThrowable.GetComponent<IThrowable> ().SetTarget (gameobj);
			EquippedThrowable = null;
		} else {
			if (!Sliding) {
				target = gameobj;
			}
		}
	}

	public void BoostStamina(){
		boosters++;

		if (boosters == boostersMax) {
			movespeed = 0.04f;
			powerActive = true;
		}
	}

	public void GiveAmmo(){
	
	}

	public void SelectedObject (GameObject selectObject)
	{
		selectedObject = selectObject;
		target = new Vector3 (-1, -1, -1);
		ActionUI.gameObject.SetActive (true);
	}

	public void PickUpObject ()
	{
		selectedObject.transform.SetParent (this.transform);
	}

	public void EnableSliding ()
	{
		Sliding = true;
		targetVector = new Vector3 (target.x, transform.position.y, target.z);
	}

	void OnCollisionEnter (Collision other)
	{

		if (other.gameObject.tag != "Clickable" && Sliding) {
			Sliding = false;

			target = new Vector3 (-1, -1, -1);
		}
	}
}	