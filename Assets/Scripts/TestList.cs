using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestList : MonoBehaviour 
{
    public enum PlayerName
    {
        Aguero,
        Akinfeev,
        Bale
    }
	
    public PlayerName name1;
    public string nameStr;

    void Awake()
    {
        nameStr = name.ToString();
    }
}
