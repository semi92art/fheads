using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;


[System.Serializable]
public class CupImageItem
{
	public Sprite imageSprite;
	public string Name, Name0;
	public Sprite flag;
	public int currentGoals;
	public int index;
}

public class CupListImage : MonoBehaviour 
{
	public List<CupImageItem>itemList;

	void Awake()
	{
		//DontDestroyOnLoad(gameObject);
	}
}
