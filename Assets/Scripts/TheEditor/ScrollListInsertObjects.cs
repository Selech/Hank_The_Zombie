//using UnityEditor;
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
	public GameObject Menu;
	public string FilePath;
	public static string CurrentFilePath;
	public static GameObject[] InsertableObjects;
	private int CooldownBeforeDrawingIcons = 1;

	void Start () 
	{
		ShowObjects();
		AddOnOKClick();
	}

	void OnEnable()
	{
		CurrentFilePath = FilePath;
	}

	void AddOnOKClick()
	{
		OKButton.GetComponentInChildren<Button>().onClick.AddListener(() => 
		{ 
			EditorMouse.mode = "InsertObject";
			Menu.SetActive(false);
		});
	}

	void ShowObjects()
	{
		// muhaha
		InsertableObjects = Resources.LoadAll<GameObject>(FilePath);
		foreach (var obj in InsertableObjects) 
		{
			print ("gameobj: "+obj.name);
			GameObject listItemGameObject = Instantiate(SampleButton);
			listItemGameObject.GetComponentInChildren<Image>().sprite = obj.GetComponent<Image>().sprite;
			GameObject tempObj = obj;
			listItemGameObject.GetComponentInChildren<Button>().onClick.AddListener(() => { OnSelected(tempObj); });
			listItemGameObject.transform.SetParent(this.transform);
		}
	}
	public void OnSelected(GameObject gmObj)
	{
		print ("clicked: "+gmObj.name);
		LevelDesigner.ObjectToBeInserted = gmObj;
		OKButton.GetComponent<Button>().interactable = true;
	}
}