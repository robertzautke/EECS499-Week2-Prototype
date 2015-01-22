﻿using UnityEngine;
using System.Collections;

public class currentLevelController : MonoBehaviour {

	public int currentLevel = 0;
	public GameObject[] levels;

	public void nextLevel()
	{
		if (currentLevel < levels.Length) 
		{
			levels[currentLevel].SetActive(false);
			currentLevel++;
			levels[currentLevel].SetActive(true);
		}

	}

	public void previousLevel()
	{
		if (currentLevel > 0)
		{
			levels[currentLevel].SetActive(false);
			currentLevel--;
			levels[currentLevel].SetActive(true);
		}
	}
}
