using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {

	public void AnimationPlayer(string animationName)
	{
		GetComponent<Animation> ().Play (animationName);
	}

}
