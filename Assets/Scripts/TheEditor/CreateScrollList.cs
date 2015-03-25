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

[System.Serializable]
public class Item {
	public string name;
	public Sprite icon;
	public Button.ButtonClickedEvent thingToDo;
}

public class CreateScrollList : MonoBehaviour {
	
	public GameObject sampleButton;

	void Start () 
	{
		loadLevels();
	}

	void loadLevels()
	{
		// WinConditionEnum john = WinConditionEnum.Equipment;
		// Debug.Log(john.ToName());

		List<string> fileNames = RecursiveFileProcessor.ProcessDirectory(Path.Combine(Application.persistentDataPath, "levels/"));

		foreach (var name in fileNames) 
		{
			Debug.Log("En hest skabes!");
			Debug.Log("item name: "+name);

			GameObject instance = Instantiate(sampleButton);
			instance.GetComponentInChildren<Text>().text = Path.GetFileNameWithoutExtension(name);
			instance.transform.SetParent(this.transform);
		}
	}
}