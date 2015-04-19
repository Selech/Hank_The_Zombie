using UnityEngine;
using System.Collections;

public class SpawnPointScript : MonoBehaviour {

	public GameObject enemyPrefab;
	public int rate;
	private int counter;
	public GameObject startPoint;
	public bool running;

	// Use this for initialization
	void Start () {
		counter = rate;
	}
	
	// Update is called once per frame
	void Update () {
		if (running) {
			counter--;
			
			if(counter == 0){
				GameObject enemy = Instantiate(enemyPrefab);
				enemy.transform.position = this.transform.position;
				enemy.GetComponent<Enemy>().SetStartPoint(startPoint.transform.position);
				
				
				counter = rate;
			}
		}
	}
}
