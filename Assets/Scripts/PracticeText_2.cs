using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PracticeText_2 : MonoBehaviour 
{
    /*[SerializeField]
    private Scripts scr;
    public Text text_Practice_2;


    public void SetTextString(int state)
    {
        int state_int = 0;

        switch (state)
        {
            case 0:
                text_Practice_2.text = scr.practScr.str_MoveHimUsing;
                scr.practScr.obj_MoveLeftButton.SetActive(true);
                scr.practScr.obj_MoveRightButton.SetActive(true);
                break;
            case 2:
                text_Practice_2.text = scr.practScr.str_KickJump;
                scr.practScr.obj_KickButton.SetActive(true);
                scr.practScr.obj_JumpButton.SetActive(true);
                break;
            case 4:
                text_Practice_2.text = scr.practScr.str_BycicleKick;
                scr.objLev.obj_BK_But1.SetActive(true);
                break;
            case 6:
                text_Practice_2.alignment = TextAnchor.UpperRight;
                text_Practice_2.text = scr.practScr.str_Slowdown;
                scr.timFr.EnableTimeFreezeControls();
                break;
            case 9:
                state_int = Animator.StringToHash("9");
                scr.practScr.text_Practice_2.GetComponent<Animator>().SetTrigger(state_int);
                scr.practScr.text_Practice_2.text = scr.practScr.str_MeetYourOpp;
                scr.practScr.text_Practice_2.alignment = TextAnchor.MiddleCenter;
                scr.pMov.restart = true;
                break;
            case 10:
                scr.practScr.text_Practice_2.alignment = TextAnchor.MiddleCenter;
                scr.practScr.text_Practice_2.text = scr.practScr.str_Score3Goals;
                break;
            case 11:
                scr.practScr.isFinish = true;
                break;
        }
    }

    

    public void Enable_Practice_ScoreGoal()
    {
        int int_0 = Animator.StringToHash("0");
        scr.practScr.enGatesTr.GetComponent<Animator>().SetTrigger(int_0);
        scr.practScr.plGatesTr.GetComponent<Animator>().SetTrigger(int_0);
        scr.enAlg._tr.gameObject.SetActive(true);
        scr.scoreScr.playerScore.enabled = true;
        scr.scoreScr.enemyScore.enabled = true;
        scr.practScr.text_Practice_0.enabled = false;
    }

    public void LoadMenu()
    {
        GameManager.Instance.GoToMenu();
    }*/
}
