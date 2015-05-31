using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonTestLevel : MonoBehaviour 
{
	public GameObject objNoHank;
	public GameObject objNoExit;
	public GameObject msgSaveLevelFirst;
	public GameObject btnStartTesting;
	public GameObject btnStopTesting;

	public GameController script_GameController;

	public Camera cam3D;
	public Camera cam2D;
	public Camera camTesting;
	public GameObject uiPushAndAmmo;
	public GameObject uiControllers;
	public GameObject uiGameController;

	/** Checks whether the player has been placed and an exit exists in the level **/
	public void CheckIfTestIsPossible()
	{
		if (LevelDesigner.HankPlaced == false)
		{
			msgNoHank();
		}
		else if (LevelDesigner.ExitPlaced == false) 
		{
			msgNoExit();
		}
		else 
		{
			// Check if level has been saved. This is to reroll the level to its original state when done.
			if (LevelDesigner.currentLevel.name == "<< Name of level >>"
			   || LevelDesigner.currentLevel.name == "")
			{
				msgSaveLevelFirst.SetActive(true);
			}
			else
			{
				// START TEST
				EditorMouse.mode = "Testing";
				LevelDesigner.isNotTesting = false;
				camTesting.enabled = true;
				uiPushAndAmmo.SetActive(true);
				uiControllers.SetActive(true);
				btnStartTesting.SetActive(false);
				btnStopTesting.SetActive(true);
				Popup.PopupOpen = true; // Hack for at man kan bruge a,s,w og d til at bevæge sig under test
				uiGameController.SetActive(true);
				script_GameController.Initialize();
			}
		}
	}

	public void StopTesting()
	{
		Application.LoadLevel ("EditorDesigning");
	}

	void msgNoHank()
	{
		objNoHank.SetActive (true);
	}
	
	void msgNoExit()
	{
		objNoExit.SetActive (true);
	}
}
