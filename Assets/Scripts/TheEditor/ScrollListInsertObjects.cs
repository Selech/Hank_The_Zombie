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
using UnityEditor;

public class ScrollListInsertObjects : MonoBehaviour 
{
	
	public GameObject SampleButton;
	public GameObject OKButton;
	public string SameOfSelected;
	public string FilePath;
	public static GameObject[] InsertableObjects;
	private int CooldownBeforeDrawingIcons = 100;

	void Start () 
	{
		preLoadIcons();
		AddOnOKClick();
	}

	void Update ()
	{
		checkForIconDrawing();
	}

	void preLoadIcons()
	{
		InsertableObjects = Resources.LoadAll<GameObject>(FilePath);
		
		foreach (var name in InsertableObjects) 
		{
			GameObject Instance = Instantiate(SampleButton);
			Texture2D tex = AssetPreview.GetAssetPreview(name.gameObject);
		}
	}

	void checkForIconDrawing()
	{
		if (CooldownBeforeDrawingIcons==0)
			ShowObjects();
		else if (CooldownBeforeDrawingIcons>1){
			CooldownBeforeDrawingIcons--;
			print (CooldownBeforeDrawingIcons);}
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
		foreach (var name in InsertableObjects) 
		{
			GameObject Instance = Instantiate(SampleButton);
			Texture2D tex = AssetPreview.GetAssetPreview(name.gameObject);
			Sprite ImageName = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(0.5f, 0.5f));

			Instance.GetComponentInChildren<Image>().sprite = ImageName;
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