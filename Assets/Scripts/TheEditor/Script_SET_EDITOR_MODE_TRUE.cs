using UnityEngine;
using System.Collections;

public class Script_SET_EDITOR_MODE_TRUE : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		LevelDesigner.isNotTesting = true;
		LevelDesigner.isUsingEditor = true;
	}
}
