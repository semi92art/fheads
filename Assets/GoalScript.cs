using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public Animator anim;

    void DisableAnimator()
    {
        anim.enabled = false;
        anim.gameObject.SetActive(false);
    }
}