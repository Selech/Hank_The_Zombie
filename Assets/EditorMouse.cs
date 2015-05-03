using UnityEngine;
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
		if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject (0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) 
		{
			if (Input.GetMouseButton (0)) 
			{ 
				// if left button pressed...
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				
				if (Physics.Raycast (ray, out hit)) 
				{
					// Object clicked
					GameObject hitObj = hit.collider.gameObject;

					// Tag of clicked Object
//					string tag = hitObj.tag;

					// Position of clicked object
					Vector3 position = hitObj.transform.position;

					// Cell of clicked object
					Cell cell = LevelDesigner.getCell(position);

					if (allowClick) //  && (tag == "Clickable" || tag == "Removable")
					{
						switch(mode)
						{
							case "InsertTile" : 	InsertTile(hitObj, position, cell);		break;
							case "Delete" : 		Delete(position, cell); 				break;
							case "InsertObject" : 	InsertObject(hitObj, position, cell); 	break;
							case "" : 				RotateObject(hitObj); 					break;
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

	private void InsertTile(GameObject hitObj, Vector3 position, Cell cell)
	{
		if (cell == null)
		{
			// Remove Empty Tile
			Destroy (hitObj);
			
			// Register a new cell along with a new Tile
			LevelDesigner.RegisterTile(position, LevelDesigner.TileResource.Tile);
		}
	}

	private void Delete(Vector3 position, Cell cell)
	{
		// Only start deleting if cell is not null
		if(cell != null)
		{
			// Delete order: "Object" -> "Wall" -> "Tile"
			if (cell.obj != null)
			{
				allowClick = false;
				Destroy (cell.obj);
				cell.obj = null;
			}
			else if (cell.wall != null)
			{
				allowClick = false;
				Destroy (cell.wall);
				cell.wall = null;
			}
			else if (cell.tile != null)
			{
				// Reset Cell
				LevelDesigner.RemoveCell(position);
			}
		}
	}

	private void InsertObject(GameObject hitObj, Vector3 position, Cell cell)
	{
		// Allow insertion of only 1 object at the time
		allowClick = false;

		// Insert object at position and register in a new cell
		LevelDesigner.RegisterObjectUsingGameObject(position, LevelDesigner.ObjectToBeInserted);
	}

	private void RotateObject(GameObject hitObj)
	{
		print ("ROTATE BIATCH!");

		// Allow one single rotation at the time
		allowClick = false;

		// Rotate object by 90 degrees
		hitObj.transform.Rotate(new Vector3(0,90,0));
	}

	GameObject LoadAssetFromString(string assetName)
	{
		GameObject instance = Instantiate(Resources.Load(assetName, typeof(GameObject))) as GameObject;
		
		return instance;
	}
}
