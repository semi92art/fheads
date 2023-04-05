using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;


public class Names
{
    public enum PlayerName
    {
        Aguero,
        Djonaruma, // prev - Akinfeev;
        Bale,
        Benzema,
        Buffon,
        Silva, // prev - Cahill;
        Casillias,
        de_Bruyne, // prev - Cassano;
        Cavani,
        Cech,
        Costa,
        Courtois,
        de_Jea,
        Ramos, // prev - Dempsey;
        di_Maria,
        Reus, // prev - Donnovan;
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
        Modric, // prev - Tevez;
        Terry, // prev - Toure;
        Schweinsteiger, // prev - Turan;
        Vardi,
        Vidal,
    }

    public enum PlayerName_2
    {
        Baggio,
        Bartez,
        Batistuta,
        Beckenbauer,
        Beckham,
        Bergkamp,
        Totti, // prev. Cantona
        Davids,
        del_Piero,
        di_Stefano,
        Drogba,
        Eisebio,
        Figo,
        Gerrard,
        Giggs,
        Gullit,
        Henry,
        Best,
        Kahn,
        Keane,
        Krojf,
        Lampard,
        Maldini,
        Maradonna,
        Matteus,
        Nedved,
        Pele,
        Pirlo,
        Platini,
        Raul,
        Ronaldo,
        Romario,
        Ronaldinho,
        Seedorf,
        Shearer,
        Shevchenko,
        van_Basten,
        Xavi,
        Yashin,
        Zidane,
        coach_Mourinho,
        coach_Gvardiola,
        coach_Wenger,
        coach_del_Boske,
        coach_Klopp,
        coach_Simeone,
        coach_Emery,
        coach_Alex_Ferguson,
        coach_Konte,
        coach_Ancelotti
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

[System.Serializable]
public class ProfileItem_2
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
    public Names.PlayerName_2 player;
}

[Serializable]
public struct PlayerInList
{
    public Names.PlayerName player;
}

[Serializable]
public struct PlayerInList_2
{
    public Names.PlayerName_2 player;
}

public class PlayersList : MonoBehaviour
{
	public List<PlayerInList> Players = new List<PlayerInList>();
    public List<PlayerInList_2> Players_2 = new List<PlayerInList_2>();
}