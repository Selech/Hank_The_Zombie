using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.ComponentModel;
using System.Reflection;
using System.IO;

public class ScrollListLoadLevel : MonoBehaviour {
	
	public GameObject sampleButton;
	public GameObject loadButton;
	public String nameOfSelected;

	void Start () 
	{
		showLevels();
		addOnLoadClick();
	}

	void addOnLoadClick()
	{
		loadButton.GetComponentInChildren<Button>().onClick.AddListener(() => 
		{ 
			LevelDesigner.LastLoaded = nameOfSelected;
			Application.LoadLevel ("EditorDesigning");
		});
	}

	void showLevels()
	{
		List<string> fileNames = RecursiveFileProcessor.ProcessDirectory(Path.Combine(Application.persistentDataPath, LevelDesigner.LevelsDirectory));

		foreach (var name in fileNames) 
		{
			if(name != LevelDesigner.LevelsDirectory + ".DS_Store")
			{
				GameObject instance = Instantiate(sampleButton);
				String fileName = Path.GetFileNameWithoutExtension(name);
				instance.GetComponentInChildren<Text>().text = fileName;
				
				//get the screen's width
				float sWidth = Screen.width;
//				float sHeight = Screen.height;
				
				//calculate the rescale ratio
				float guiRatioX;
				float magicScaleNum = 1.41f;
//				bool itsFreaky = true;
//				
//				#if (UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER)
//				itsFreaky = false; 
//				#endif
				
				guiRatioX = (sWidth / 800.0f) / magicScaleNum;
				Vector3 GUIsF = new Vector3(guiRatioX, guiRatioX, 0);
				instance.transform.localScale = GUIsF;
				
				instance.transform.SetParent(this.transform);

				instance.GetComponentInChildren<Button>().onClick.AddListener(() => { onSelected(fileName); });
			}
		}
	}

	public void onSelected(String fileName)
	{
		this.nameOfSelected = fileName;
		loadButton.GetComponent<Button>().interactable = true;
	}
}