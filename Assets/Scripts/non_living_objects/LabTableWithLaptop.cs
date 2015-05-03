using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class LabTableWithLaptop : MonoBehaviour, IAttackable
{

	private int HP = 10;
	public Slider HPBar;
	
	public void Attack ()
	{
		HP--;
		HPBar.value = HP;

	}

	// Use this for initialization
	void Start ()
	{
		HPBar.maxValue = HP;
		HPBar.value = HPBar.maxValue;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Bullet") {
			Attack ();

		}
	}
}
