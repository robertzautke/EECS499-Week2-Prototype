using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemySpawner : MonoBehaviour {

	public GameObject enemyType;
	private List<Object> spawnedEnemies = new List<Object>();

	public int numberOfEnemies = 1;
	private int currentNumberOfEnemies = 0;

	void Start () {
	
	}
	void Update () {
		if(currentNumberOfEnemies < numberOfEnemies)
		{ 
			
			spawnedEnemies.Add(Object.Instantiate(enemyType, this.transform.position, this.transform.rotation));
			currentNumberOfEnemies++;

		}

		foreach(Object O in spawnedEnemies)
		{
			if(O == null)
			{ 
				spawnedEnemies.Remove(O);
				currentNumberOfEnemies--;
			}
		}
	}
}
