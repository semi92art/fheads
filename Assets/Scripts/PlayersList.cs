using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;


public class Names
{
    public enum PlayerName
    {
        Aguero,
        Djonaruma,
        Bale,
        Benzema,
        Buffon,
        Silva,
        Casillias,
        de_Bruyne,
        Cavani,
        Cech,
        Costa,
        Courtois,
        de_Jea,
        Ramos,
        di_Maria,
        Reus,
        Dzeko,
        Dzyuba,
        Falcao,
        Getze,
        Giroud,
        Griezmann,
        Hart,
        Hazard,
        Ibrahimovic,
        Iniesta,
        Kane,
        Levandowski,
        Luiz,
        Mandzukic,
        Messi,
        Muller,
        Neuer,
        Neymar,
        Ozil,
        Piquet,
        Pogba,
        Rakitic,
        Ribery,
        Robben,
        Rodriguez,
        Ronaldo,
        Rooney,
        Sanchez,
        Suarez,
        Modric,
        Terry,
        Schweinsteiger,
        Vardi,
        Vidal,
    }
    
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
}

[System.Serializable]
public class ProfileItem 
{
    public string name;
    public Button.ButtonClickedEvent toDo;
    public Sprite icon;

    // Skills:
    public float skill_Speed;
    public float skill_Kick;
    public float skill_Jump;

    [HideInInspector]
    public int plInd;
    [HideInInspector]
    public int cntrInd;

    public Names.CountryName country;
    public Names.PlayerName player;
}

[Serializable]
public struct PlayerInList
{
    public Names.PlayerName player;
}

public class PlayersList : MonoBehaviour
{
	public List<PlayerInList> Players = new List<PlayerInList>();
}