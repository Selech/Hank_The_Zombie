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
		// WinConditionEnum john = WinConditionEnum.Equipment;
		// Debug.Log(john.ToName());

		List<string> fileNames = RecursiveFileProcessor.ProcessDirectory(Path.Combine(Application.persistentDataPath, "levels/"));

		foreach (var name in fileNames) 
		{
			if(name != ".DS_Store")
			{
				GameObject instance = Instantiate(sampleButton);
				String fileName = Path.GetFileNameWithoutExtension(name);
				instance.GetComponentInChildren<Text>().text = fileName;
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