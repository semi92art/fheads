using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ChampItem
{
	public Sprite imageSprite;
	public string Name, Name0;
	public int currentGoals;
	public Sprite flag;
	public float summSkill;

	[HideInInspector]
	public int rand, Wins, Ties;
	public int Loses;
	public int GoalsDiff;
	public int Points;
	public int indexOf;
}
	
public class ChampList : MonoBehaviour 
{
	public List<ChampItem>itemList;

	void Start () 
	{
		DontDestroyOnLoad (transform.gameObject);

		if (SceneManager.GetActiveScene().name == "MainMenu")
			DestroyImmediate(gameObject);
	}
}

	
