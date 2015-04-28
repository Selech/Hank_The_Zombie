using UnityEngine;
using System.Collections;

public class SpawnPointScript : MonoBehaviour {

	public GameObject enemyPrefab;
	public int rate;
	private int counter;
	public GameObject startPoint;
	public bool running;
	private MouseControlsV3 gameController;

	// Use this for initialization
	void Start () {
		counter = rate;

		gameController = GameObject.Find ("Controller").GetComponent<MouseControlsV3> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (running) {
			counter--;
			
			if(counter == 0){
				GameObject enemy = Instantiate(enemyPrefab);

				float randomX; 
				float randomZ; 

				if(gameController.player.transform.position.x < 10){
					randomX = gameController.player.transform.position.x + Random.Range(3f,12f);
				}else{
					randomX = gameController.player.transform.position.x - Random.Range(3f,12f);
				}

				if(gameController.player.transform.position.z < 10){
					randomZ = gameController.player.transform.position.z + Random.Range(3f,12f);
				}else{
					randomZ = gameController.player.transform.position.z - Random.Range(3f,12f);
				}

				enemy.transform.position = new Vector3(randomX, 10f, randomZ);
				//enemy.GetComponent<Enemy>().SetStartPoint(startPoint.transform.position);
				
				
				counter = rate;
			}
		}
	}
}
