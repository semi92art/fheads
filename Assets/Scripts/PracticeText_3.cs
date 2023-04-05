using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeText_3 : MonoBehaviour
{
    private Scripts scr;

    public void CallFinishPractice()
    {
        scr = FindObjectOfType<Scripts>();
        scr.practScr.CallPracticeText2(11);
        scr.practScr.text_Practice_2.text = "Nice! You have been trained and\n" +
            "now you are ready to play real battles!\n" +
            "Good luck!";
    }
}
