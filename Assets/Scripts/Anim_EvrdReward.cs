using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_EvrdReward : MonoBehaviour 
{
    [SerializeField]
    private GameObject obj_MainMenu;


    public void DisableMainMenu()
    {
        obj_MainMenu.SetActive(false);
    }

    public void EnableMainMenu()
    {
        obj_MainMenu.SetActive(true);
    }
}
