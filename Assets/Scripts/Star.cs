using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour 
{
    public Animator anim;

    public void DisableAnimator()
    {
        anim.enabled = false;
        anim.gameObject.SetActive(false);
    }
}
