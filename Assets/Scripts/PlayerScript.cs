using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

	private GameObject target;
	public Canvas won;

	public Animator animator;
	public RectTransform ActionUI;
	private GameObject selectedObject;

	// Use this for initialization
	void Start () {
		Statics.LevelWon = false;
		animator.SetTrigger ("TriggerIdle");

		
	}
	
	// Update is called once per frame
	void Update () {

		if (target != null){
			
			//transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

			Vector3 targetposition = new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z);
			transform.position = Vector3.MoveTowards (transform.position, targetposition, 0.015f);
			transform.LookAt (targetposition);

			if (transform.position == targetposition) {
				animator.SetTrigger ("TriggerIdle");
				animator.ResetTrigger ("TriggerWalk");

				target = null;
			}
		} else {

			animator.SetTrigger ("TriggerIdle");
		}
	}	
	
	public void SetTarget(GameObject gameobj){
		target = gameobj;
		animator.ResetTrigger ("TriggerIdle");
		animator.SetTrigger ("TriggerWalk");
	}

	public void SelectedObject(GameObject selectObject)
	{
		selectedObject = selectObject;
		target = null;
		ActionUI.gameObject.SetActive (true);
	}

	public void PickUpObject()
	{
		selectedObject.transform.SetParent (this.transform);
	}
}	