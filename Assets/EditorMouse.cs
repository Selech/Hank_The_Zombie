using UnityEngine;
using System.Collections;

public class EditorMouse : MonoBehaviour {
	
	public string mode = "";

	// Use this for initialization
	void Start () {

	}

	public void SwitchMode(string mode){
		this.mode = mode;
	}
	
	// Update is called once per frame
	void Update () {
		if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject (0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {
			
			if (Input.GetMouseButton (0)) { // if left button pressed...
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.gameObject.tag == "Clickable") {

						switch(mode)
						{
						case "InsertTile" :
							Vector3 position = hit.collider.gameObject.transform.position;
							Destroy (hit.collider.gameObject);
							GameObject tile = (GameObject) LoadAssetFromString("Tile");
							tile.transform.position = position;
							break;


						case "DeleteTile" :
							Vector3 positionDel = hit.collider.gameObject.transform.position;
							Destroy (hit.collider.gameObject);
							GameObject tileDel = (GameObject) LoadAssetFromString("EmptyTile");
							tileDel.transform.position = positionDel;

							break;
						}
					}
				}
			}
		}
	}

	GameObject LoadAssetFromString(string assetName)
	{
		GameObject instance = Instantiate(Resources.Load(assetName, typeof(GameObject))) as GameObject;
		
		return instance;
	}
}
