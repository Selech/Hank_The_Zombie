using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

	private Vector3 target;
	private Vector3 targetVector;
	public Canvas won;

	public Animator animator;
	public RectTransform ActionUI;
	private GameObject selectedObject;

	private bool Sliding;

	// Use this for initialization
	void Start () {
		animator.SetTrigger ("TriggerIdle");
	}

	// Update is called once per frame
	void Update () {
		if (Sliding) {
			targetVector -= (transform.position - targetVector)/100;
			transform.position = Vector3.MoveTowards (transform.position, targetVector, 0.015f);
			transform.LookAt (targetVector);
			

		} else {
			if (target != new Vector3(-1,-1,-1)){
				
				//transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
				
				Vector3 targetposition = new Vector3 (target.x, transform.position.y, target.z);
				transform.position = Vector3.MoveTowards (transform.position, targetposition, 0.015f);
				transform.LookAt (targetposition);
				
				if (transform.position == targetposition) {
					animator.SetTrigger ("TriggerIdle");
					animator.ResetTrigger ("TriggerWalk");
					
					target = new Vector3(-1,-1,-1);
				}
			} else {
				
				animator.SetTrigger ("TriggerIdle");
			}
		}
	}

	public void SetTarget(Vector3 gameobj){
		if (!Sliding) {
			target = gameobj;
			animator.ResetTrigger ("TriggerIdle");
			animator.SetTrigger ("TriggerWalk");
		}
	}

	public void SelectedObject(GameObject selectObject)
	{
		selectedObject = selectObject;
		target = new Vector3(-1,-1,-1);
		ActionUI.gameObject.SetActive (true);
	}

	public void PickUpObject()
	{
		selectedObject.transform.SetParent (this.transform);
	}

	public void EnableSliding(){
		Sliding = true;
		targetVector = new Vector3 (target.x, transform.position.y, target.z);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag != "Clickable" && Sliding) {
			Sliding = false;
			animator.SetTrigger ("TriggerIdle");
			animator.ResetTrigger ("TriggerWalk");
			
			target = new Vector3(-1,-1,-1);
		}
	}
}	