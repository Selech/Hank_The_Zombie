using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BtnLoadLevel : MonoBehaviour {

	public Button btn;

	// Use this for initialization
	void Start () 
	{
		// Make non-interactable if no levels are saved
		if(RecursiveFileProcessor.countFilesInDirectory(LevelDesigner.LevelsDirectory) == 0)
		{
			btn.GetComponent<Button>().interactable = false;
		}
	}
}
