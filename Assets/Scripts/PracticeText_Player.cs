using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeText_Player : MonoBehaviour
{
    public Animator anim_PractText_2;

    public void CallPracticeText_2()
    {
        if (gameObject.name == "Player Text")
            anim_PractText_2.SetTrigger("0");
        else if (gameObject.name == "Enemy Text")
            GetComponent<Animator>().SetTrigger("0");
    }
}
