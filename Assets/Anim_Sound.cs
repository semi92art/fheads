using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Sound : MonoBehaviour 
{
    enum WhatButton
    {
        sound, tilt
    }

    [SerializeField]
    private Scripts scr;
    [SerializeField]
    private WhatButton _whatButton;

    public void SetButton()
    {
        switch (_whatButton)
        {
            case WhatButton.sound:
                scr.levAudScr.EnableSound(1);
                break;
            case WhatButton.tilt:
                scr.objLev.EnableTilt(1);
                break;
        }
    }
}
