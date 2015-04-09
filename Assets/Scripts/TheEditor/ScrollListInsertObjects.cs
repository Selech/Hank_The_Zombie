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

public class ScrollListInsertObjects : MonoBehaviour 
{
	
	public GameObject SampleButton;
	public GameObject OKButton;
	public string SameOfSelected;
	public string FilePath;
	public static GameObject[] InsertableObjects;

	void Start () 
	{
		ShowObjects();
		AddOnOKClick();
	}

	void AddOnOKClick()
	{
		OKButton.GetComponentInChildren<Button>().onClick.AddListener(() => 
		{ 
			LevelDesigner.LastLoaded = SameOfSelected;
			Application.LoadLevel ("EditorDesigning");
		});
	}

	void ShowObjects()
	{
		InsertableObjects = Resources.LoadAll<GameObject>("Prefabs/" + FilePath);
	
		foreach (var name in InsertableObjects) 
		{
			GameObject Instance = Instantiate(SampleButton);
			Sprite ImageName = name.gameObject.GetComponent<Image>().sprite;

			Instance.GetComponentInChildren<Image>().sprite = ImageName;

			//string ImageName = name.name;
			//Instance.GetComponentInChildren<Text>().text = ImageName;

			Instance.transform.SetParent(this.transform);

			//Instance.GetComponentInChildren<Button>().onClick.AddListener(() => { onSelected(ImageName); });
		}
	}

	public void onSelected(String fileName)
	{
		this.SameOfSelected = fileName;
		OKButton.GetComponent<Button>().interactable = true;
	}
}