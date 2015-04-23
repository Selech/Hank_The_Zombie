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
	public Text txt;

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
			Button btn = listItemGameObject.GetComponentInChildren<Button>();

			//get the screen's width
			float sWidth = Screen.width;
			float sHeight = Screen.height;

			//calculate the rescale ratio
			float guiRatioX;
			float magicScaleNumberDesktop = 1.45f;
			float magicScaleNumberMobile = 1.6f;
			bool itsFreaky = true;

			#if (UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER)
			itsFreaky = false; 
			#endif
			
			print ("itsFreaky: "+itsFreaky);
			txt.text = "itsFreaky: "+itsFreaky+ "\n" +txt.text;

			guiRatioX = (sWidth / 800.0f) / ((itsFreaky) ? magicScaleNumberMobile : magicScaleNumberDesktop);

			print ("sWidth / 800.0f): "+(sWidth / 800.0f));
			print ("sWidth: "+sWidth);
			print ("guiRatioX: "+guiRatioX);

			txt.text = "sHeight: "+sHeight+ "\n" +txt.text;
			txt.text = "sWidth: "+sWidth+ "\n" +txt.text;
			txt.text = "guiRatioX: "+guiRatioX+ "\n" +txt.text;

			//create a rescale Vector3 with the above ratio
			print ("før: "+btn.transform.localScale.ToString());
			txt.text = ("før: "+btn.transform.localScale.ToString())+ "\n" +txt.text;

			Vector3 GUIsF = new Vector3(guiRatioX, guiRatioX, 0);

			btn.transform.localScale = GUIsF;
			print ("efter: "+btn.transform.localScale.ToString());
			txt.text = ("efter: "+btn.transform.localScale.ToString())+ "\n" +txt.text;
			
			btn.onClick.AddListener(() => { OnSelected(tempObj); });

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