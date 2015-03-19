using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class LabTableWithLaptop : MonoBehaviour, IAttackable {

	private int HP = 10;
	public Slider HPBar;
	
	public void Attack(){
		
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Clicked(){

		HP--;
		HPBar.value = HP;
		
	}



	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player")
		{
			HP--;
			HPBar.value = HP;

			other.gameObject.GetComponent<PlayerScript>().SetTarget(other.gameObject.transform.position);

		}
	}
}
