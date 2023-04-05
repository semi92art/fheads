using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkScript : MonoBehaviour 
{
    [SerializeField]
    private Scripts scr;
    [Space(5)]
    public ParticleSystem[] partSyst_Fireworks;


    public void SetActiveGoalFirework()
    {
        partSyst_Fireworks[0].Play();
    }

    public void SetActiveWinFirework()
    {
        partSyst_Fireworks[1].Play();
    }
}
