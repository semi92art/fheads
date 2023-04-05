using UnityEngine;
//using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Countries
{
	public enum CountryName
	{
		Algeria,
		Argentina,
		Australia,
		Austria,
		Belgium,
		Bosnia,
		Brazil,
		Cameroon,
		Chilie,
		Colombia,
		Costa_Rica,
		Cote_d_Voire,
		Croatia,
		Czech,
		Denmark,
		Ecuador,
		England,
		France,
		Germany,
		Ghana,
		Greece,
		Hungary,
		Ireland,
		Italy,
		Mexico,
		Netherlands,
		Nigeria,
		Poland,
		Portugal,
		Puerto_Rico,
		Russia,
		Spain,
		Sweden,
		Switzerland,
		Turkey,
		Ukraine,
		Uruguay,
		USA,
		USSR,
		Venezuela,
		Wales,
        Scotland,
        Northern_Ireland
	}

	public CountryName country;
	public Sprite boot;
    public Color stCol, endCol;
    public Color stadCol;
}

public class CountriesList : MonoBehaviour 
{
	public List<Countries> Countries = new List<Countries>();
}
