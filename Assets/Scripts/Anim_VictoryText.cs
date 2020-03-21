using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_VictoryText : MonoBehaviour 
{
    public Scripts scr;
    [HideInInspector]
    public int winState;
    public Animator _anim;


    public void LoadNextGame()
    {
        if (!PrefsManager.Instance.IsRandomOpponent && scr.alPrScr.game == 10)
            scr.objLev.ContinueTournament();
        else
            GameManager.Instance.GoToMenu();
    }

    public void Call_Animation()
    {
        _anim.SetTrigger(Animator.StringToHash(winState.ToString()));
    }
}
