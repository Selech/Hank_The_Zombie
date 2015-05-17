using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour 
{
	public GameObject uiTestSucces;

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") 
		{
			print ("lol");
			print ("script "+GameObject.Find("Level Designer"));
			GameObject.Find("Level Designer").GetComponent<LevelDesigner>().popupTestComletedSuccesFully.SetActive(true);
			//Destroy (other.gameObject);

			//GameObject.Find("TestSucces").SetActive(true);

//			GameObject.Find ("Controller").GetComponent<GameController> ().GameEnd ();
//			GameObject.Find ("Controller").GetComponent<TimeController> ().started = false;
		}
	}
}
