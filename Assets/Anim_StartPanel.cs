using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_StartPanel : MonoBehaviour 
{
    public Scripts scr;
    public AudioSource src_Boom;
    public Animator anim_StartPanel;
    public GameObject obj_IntroPanel;


    public void EnableSound()
    {
        scr.levAudScr.EnableSound(1);
    }

    public void Play_Boom()
    {
        //src_Boom.Play();
    }

    public void StopIntro()
    {
        anim_StartPanel.enabled = false;
        obj_IntroPanel.SetActive(false);
    }
}
