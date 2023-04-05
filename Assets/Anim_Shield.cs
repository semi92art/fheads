using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Shield : MonoBehaviour 
{
    public SpriteRenderer _sprRend;
    public Animator anim_Shield;
    private bool isFollow;


    void Awake()
    {
        DisableShield();   
    }

    public void EnableShield()
    {
        anim_Shield.SetTrigger(Animator.StringToHash("0"));
    }

    public void DisableShield()
    {
        anim_Shield.SetTrigger(Animator.StringToHash("1"));
    }
}
