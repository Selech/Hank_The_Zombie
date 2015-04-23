﻿using UnityEngine;
using System.Collections;

public class EditorMouse : MonoBehaviour 
{
	public static string mode = "";
	private static bool allowClick = true;

	public void SwitchMode(string mode)
	{
		EditorMouse.mode = mode;
	}
	
	// Update is called once per frame
	void Update () 
	{
		print ("mode: "+mode);
		if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject (0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) 
		{
			if (Input.GetMouseButton (0)) 
			{ 
				// if left button pressed...
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				
				if (Physics.Raycast (ray, out hit)) 
				{
					string tag = hit.collider.gameObject.tag;
					print ("tag: "+tag);
					print ("allowClick: "+allowClick);
					if (tag == "Clickable" || tag == "Removable") 
					{
						switch(mode)
						{
							case "InsertTile" :
								if (hit.collider.gameObject.name == "EmptyTile(Clone)")
								{
									Vector3 position = hit.collider.gameObject.transform.position;
									Destroy (hit.collider.gameObject);
									GameObject tile = (GameObject) LoadAssetFromString("Tile");
									tile.transform.position = position;
								
									// Register Placed Tile
									LevelDesigner.RegisterOnTile(position, "Tile");
								}
								break;

							case "DeleteTile" :
								if (allowClick)
								{
									GameObject hitObj 	= hit.collider.gameObject;
									Vector3 vec = hitObj.transform.position;
									string nameOfRegistered	= hit.collider.gameObject.name.Replace("(Clone)", "");
									string pathOfPrefab = LevelDesigner.GetValueOfTileGridIndex(vec);
									string nameofPrefab = pathOfPrefab.Substring((pathOfPrefab.IndexOf("/")+1));
									
									print ("Name of Registered: "+nameOfRegistered);
									print ("nameofPrefab: "+nameofPrefab);
									
									if(nameOfRegistered != "EmptyTile")
									{
										// The object we click must be of the registered on the tile
										if (nameOfRegistered == nameofPrefab)
										{
											// Place Empty Tile if removed obj was a "Tile"
											if(nameOfRegistered == "Tile")
											{
												GameObject obj = (GameObject) LoadAssetFromString("EmptyTile");
												obj.transform.position = vec;
												
												// Register Placed Tile
												LevelDesigner.RegisterOnTile(vec, "EmptyTile");
											}
											else
											{
												// If not a "Tile" only allow 1 object deletion at the time
												allowClick = false;
											}
										
											// Remove Tile + Place Empty Tile
											print ("Destroying:  "+hit.collider.gameObject.name);
											Destroy (hitObj);
											
											// Register Placed Tile
											LevelDesigner.RegisterOnTile(vec, "Tile");
										}
									}
								}
								break;

							case "InsertObject" :
								if (allowClick)
								{
									// Allow insertion of only 1 object at the time
									allowClick = false;

									string clickedObjectName = hit.collider.gameObject.name.Replace("(Clone)", "");
									Vector3 PositionInsertObject = hit.collider.gameObject.transform.position;
									
									print ("LevelDesigner.IsTileOccupied(PositionInsertObject): "+LevelDesigner.IsTileClear(PositionInsertObject));

									if(LevelDesigner.IsTileClear(PositionInsertObject))
									{
										print ("Name of object: "+clickedObjectName);
										GameObject ObjectInsert = Instantiate(LevelDesigner.ObjectToBeInserted);
										ObjectInsert.transform.position = PositionInsertObject;
										ObjectInsert.tag = "Clickable";
										
										// Register Placed object
										string nameOfObjectToInsert = LevelDesigner.ObjectToBeInserted.name.Replace("(Clone)", "");
										LevelDesigner.RegisterOnTile(PositionInsertObject, ScrollListInsertObjects.CurrentFilePath+"/"+nameOfObjectToInsert);
									}
								}
								break;

						case "" :
							if (allowClick)
							{
								allowClick = false;
								GameObject hitObj = hit.collider.gameObject;
								hitObj.transform.Rotate(new Vector3(0,90,0));
							}
							break;
						}
					}
				}
			}

			// Check for release of left mouse
			if (Input.GetMouseButtonUp(0))
			{
				allowClick = true;
			}
		}
	}

	GameObject LoadAssetFromString(string assetName)
	{
		GameObject instance = Instantiate(Resources.Load(assetName, typeof(GameObject))) as GameObject;
		
		return instance;
	}
}
