using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;


[System.Serializable]
public class ChampImageItem
{
	public Sprite imageSprite;
	public string Name, Name0;
	public int currentGoals;
	public Sprite flag;
	public float summSkill;
	public int rand, Wins, Ties, Loses, GoalsDiff, Points;// indexOf;
}
	
public class ChampListImage : MonoBehaviour
{
	public List<ChampImageItem>itemList;
	public bool endOfChamp;

	void Start()
	{
		DontDestroyOnLoad (transform.gameObject);

		if (SceneManager.GetActiveScene().name=="MainMenu")
			DestroyImmediate(gameObject);
	}
}

	
