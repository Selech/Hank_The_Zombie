using UnityEngine;
using System.Collections;
using System.IO;

public class DeleteLevel : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Delete()
	{
		string XMLFile = LevelDesigner.LevelsDirectory + LevelDesigner.currentLevel.name + ".xml";
		File.Delete (XMLFile);
	}
}
