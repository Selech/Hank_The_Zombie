using UnityEngine;
using System.Collections;

public class BtnNewLevel : MonoBehaviour {

	public void onClick()
	{
		LevelDesigner.LastLoaded = "";
		Application.LoadLevel ("EditorDesigning");
	}
}
