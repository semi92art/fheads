using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anim_ButtonPlay : MonoBehaviour 
{
    public Animator _anim;
    private int anim_num;
    private int anim_last = 1;
    public Text text_Play;
    public Font[] fonts;

    public void SetAnimTrigger()
    {
        string _trigger = anim_num.ToString();
        _anim.SetTrigger(Animator.StringToHash(_trigger));
    }

    public void SetAnimation(int anim_num_0)
    {
        anim_num = anim_num_0;
    }

    public void SetFont(int _ind)
    {
        text_Play.font = fonts[_ind];
    }
}
