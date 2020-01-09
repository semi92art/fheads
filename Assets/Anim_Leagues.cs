using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Leagues : MonoBehaviour
{
    public Scripts scr;
    public GameObject obj_MainMenu;
    public GameObject obj_ExitButton;

    public void DisableMainMenu()
    {
        obj_MainMenu.SetActive(false);
    }

    public void EnableMainMenu()
    {
        obj_MainMenu.SetActive(true);
    }

    public void LoadTournament(int _ind)
    {
        scr.carMng.LoadTournament(_ind);
    }

    public void DisableTournamentMenu()
    {
        gameObject.SetActive(false);
    }

    public void DisableExitButton()
    {
        obj_ExitButton.SetActive(false);
    }
}
