using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_UpgradesMenu : MonoBehaviour 
{
    [SerializeField]
    private Scripts scr;
    [SerializeField]
    private GameObject obj_MainMenu;
    [SerializeField]
    private Animator _anim_UpgrMenu;


    public void DisableUpgradesMenu()
    {
        gameObject.SetActive(false);
    }

    public void DisableMainMenu()
    {
        obj_MainMenu.SetActive(false);
    }

    public void EnableMainMenu()
    {
        obj_MainMenu.SetActive(true);
    }
}
