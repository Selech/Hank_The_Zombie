using UnityEngine;
using System.Collections;

public class EditorMouse : MonoBehaviour 
{
	public static string mode = "";
	private static bool allowClick = true;

	public GameObject msgOnlyOneHank;
	public GameObject msgOnlyOneExit;

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
					GameObject hitObj = hit.collider.transform.root.gameObject;

					// Tag of clicked Object
//					string tag = hitObj.tag;

					// Position of clicked object
					Vector3 position = hitObj.transform.position;

					// Cell of clicked object
					Cell cell = LevelDesigner.getCell(position);

					//print ("position: "+position.ToString());
					//if(cell != null) print ("cell: "+cell.xPos+", "+cell.yPos+", "+cell.zPos);

					if (allowClick) //  && (tag == "Clickable" || tag == "Removable")
					{
						switch(mode)
						{
							case "InsertTile" : 	InsertTile(hitObj, position, cell);		break;
							case "Delete" : 		Delete(position, cell); 				break;
							case "InsertObject" : 	InsertObject(hitObj, position, cell); 	break;
							case "" : 				RotateObject(hitObj, cell); 			break;
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

	bool checkForMultipleHanks ()
	{
		// Hank is the current selected insertable object?
		string insertName = LevelDesigner.ObjectToBeInserted.name;
		if (insertName == "CubeHank" || insertName == "CubeHank-NoGun")
		{
			// Hank is already existing in scene?
			if (LevelDesigner.HankPlaced == true) 
			{
				msgOnlyOneHank.SetActive (true);
				return true;
			}
		}

		return false;
	}

	bool checkForMultipleExits ()
	{
		// Exit is the current selected insertable object?
		string insertName = LevelDesigner.ObjectToBeInserted.name;
		if (insertName == "EndDoor") 
		{
			// An Exit is already existing in scene?
			if (LevelDesigner.ExitPlaced == true) 
			{
				msgOnlyOneExit.SetActive (true);
				return true;
			}
		}

		return false;
	}

	private void InsertObject(GameObject hitObj, Vector3 position, Cell cell)
	{
		if (cell != null)
		{
			// If object is already in Cell, then rotate it instead
			if (cell.obj != null) 
			{
				RotateObject (hitObj, cell);
				return;
			}
			
			// Check for multiple Hanks or Exits
			if(checkForMultipleHanks () == true) return;
			else if(checkForMultipleExits () == true) return;
			
			// Allow insertion of only 1 object at the time
			allowClick = false;
			
			// Insert object at position and register in a new cell
			LevelDesigner.RegisterObjectUsingGameObject(position, LevelDesigner.ObjectToBeInserted);
		}
	}

	private void RotateObject(GameObject hitObj, Cell cell)
	{
		if(cell != null)
		{			
			// Rotate order: "Object" -> "Wall"
			if (cell.obj != null)
			{
				// Restrict mouse down rotating
				allowClick = false;

				// Rotate object by 90 degrees
				cell.obj.transform.Rotate(new Vector3(0,90,0));
			}
			else if (cell.wall != null)
			{
				// Restrict mouse down rotating
				allowClick = false;

				// Rotate object by 90 degrees
				cell.wall.transform.Rotate(new Vector3(0,90,0));
			}
		}
	}

	GameObject LoadAssetFromString(string assetName)
	{
		GameObject instance = Instantiate(Resources.Load(assetName, typeof(GameObject))) as GameObject;
		
		return instance;
	}
}
