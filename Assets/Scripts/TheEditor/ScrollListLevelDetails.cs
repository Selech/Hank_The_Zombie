using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ScrollListLevelDetails : MonoBehaviour {

	public Button btnSave;
	public Text inputName;
	public Text inputAuthor;
	public Text inputDescription;
	public Text txtFieldNameOfLevel;

	public GameObject msgSavedGameMenu;
	public Text msgSavedGameText;
	public GameObject msgAlreadyExistingSaveGameMenu;
	public GameObject menu;

	public Button[] allWinConditions;
	public Button inputWinConditionInfectEverything;
	public Button inputWinConditionEscape;
	private string selectedWinCondition = "";

	public Button[] allLoseConditions;
	public Button inputLoseConditionKilled;
	public Button inputLoseConditionCaught;
	private string selectedLoseCondition = "";

	public Color colorSelected;
	public Color colorUnSelected;

	void Update()
	{
		if (inputName.text != "" 
		   && inputAuthor.text != ""
		   && inputDescription.text != "")
		{
			btnSave.interactable = true;
		}
	}

	void Start () 
	{
		// Setup Buttons for Win & Lose conditions
		setupWinLoseConditions();
		btnSave.GetComponentInChildren<Button>().onClick.AddListener(() => { saveLevel(); });
	}


	void setupWinLoseConditions()
	{
		inputWinConditionEscape.GetComponentInChildren<Text>().text = EnumHelper.ToName(Level.WinConditionEnum.Escape);
		inputWinConditionInfectEverything.GetComponentInChildren<Text>().text = EnumHelper.ToName(Level.WinConditionEnum.Infect);
		
		inputLoseConditionKilled.GetComponentInChildren<Text>().text = EnumHelper.ToName(Level.LoseConditionEnum.Killed);
		inputLoseConditionCaught.GetComponentInChildren<Text>().text = EnumHelper.ToName(Level.LoseConditionEnum.Caught);
		
		allWinConditions = new Button[]{inputWinConditionInfectEverything, inputWinConditionEscape};
		allLoseConditions = new Button[]{inputLoseConditionCaught, inputLoseConditionKilled};
		
		colorSelected = new Color(111/255.0F,212/255.0F,79/255.0F,255/255.0F);
		colorUnSelected = new Color(0.0f, 0.0f, 0.0f, 0.2f);
		
		createCheckboxGroup(allWinConditions, 1); 
		createCheckboxGroup(allLoseConditions, 2);
	}

	void createCheckboxGroup(Button[] btns, int winOrLoseCondition)
	{
		for (int i=0; i<btns.Length; i++)
		{
			int tempInt = i; // Hvis jeg bare bruger i så husker begge listenere i og den vil til sidst være 2 .... og så kigger begge knapper ved tryk på 2 i arrayet, hvilket jo giver out of index
			btns[i].onClick.AddListener(() => 
			{
				// Color all Un-selected
				for (int t=0; t<btns.Length; t++)
				{
					ColorBlock cb = btns[t].colors;
					cb.normalColor = colorUnSelected;
					btns[t].colors = cb;
				}

				// Color the selected "selected"
				ColorBlock cb2 = btns[tempInt].colors;
				cb2.normalColor = colorSelected;
				cb2.pressedColor = colorSelected;
				cb2.highlightedColor = colorSelected;
				btns[tempInt].colors = cb2; 

				// Remember selected value
				if (winOrLoseCondition == 1){
					selectedWinCondition = btns[tempInt].GetComponentInChildren<Text>().text;
				} else {
					selectedLoseCondition = btns[tempInt].GetComponentInChildren<Text>().text;
				}
			});
		}
	}

	public void saveLevel()
	{
		if(RecursiveFileProcessor.IsFileExisting(inputName.text+".xml", LevelDesigner.LevelsDirectory)) //LevelDesigner.checkForOverWrite(inputName.text)
		{
			msgAlreadyExistingSaveGameMenu.SetActive(true);	
		}
		else
		{
			Level lvl = LevelDesigner.currentLevel;
			lvl.name = inputName.text;
			lvl.author = inputAuthor.text;
			lvl.description = inputDescription.text;
			
			if(selectedWinCondition != "")
			{
				int winEnumValue = EnumHelper.GetCorrespondingEnumPositionFromDescriptionSearhWord<Level.WinConditionEnum>(selectedWinCondition);
				lvl.SetWinCondition((Level.WinConditionEnum) winEnumValue);
			}
			
			if(selectedLoseCondition != "")
			{
				int loseEnumValue = EnumHelper.GetCorrespondingEnumPositionFromDescriptionSearhWord<Level.LoseConditionEnum>(selectedLoseCondition);
				lvl.SetLoseCondition((Level.LoseConditionEnum) loseEnumValue);
			}
			
			// Update name of level textfield in editor
			txtFieldNameOfLevel.text = lvl.name;
			
			LevelDesigner.SaveLevel();

			// Hide this Menu
			menu.SetActive(false);

			// Show Confirmation Menu
			msgSavedGameMenu.SetActive(true);
			msgSavedGameText.text = "The level has been save as:\n' "+lvl.name+" '";
		}
	}
}