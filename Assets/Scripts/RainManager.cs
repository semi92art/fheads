using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.RainMaker;
using UnityEngine.UI;

public class RainManager : MonoBehaviour 
{
    [SerializeField]
    private Scripts scr;

    public bool isHandleRain;
    public bool isRain;
    public RainScript2D rainScr;

    [Header("Not Rain Objects:")]
    public GameObject grObj;
    public GameObject gr4BallObj;

    [Header("Rain Objects:")]
    public GameObject rainCollObj;
    public GameObject rainGrObj;
    public GameObject rainGr4BallObj;

    private bool onOff;
    [HideInInspector]
    public bool isRainThisGame;
    public float rainIntensity;

    void Awake()
    {
        SetRain_Off();
        SetRainPoints();

        if (isRainThisGame)
            SetRain_On();
    }

    private void SetRainPoints()
    {
        rainIntensity = 0.3f;

        switch (PlayerPrefs.GetInt("Graph"))
        {
            case 0:
                rainIntensity = 0.2f;
                break;
            case 1:
                rainIntensity = 0.3f;
                break;
            case 2:
                rainIntensity = 0.5f;
                break;
        }
    }

    public void SetRain_On()
    {
        switch (PlayerPrefs.GetInt("Graph"))
        {
            case 0:
                rainIntensity = 0.2f;
                break;
            case 1:
                rainIntensity = 0.3f;
                break;
            case 2:
                rainIntensity = 0.5f;
                break;
        }

        rainScr.RainIntensity = rainIntensity;
        rainScr.gameObject.SetActive(true);
        grObj.SetActive(false);
        gr4BallObj.SetActive(false);

        rainGrObj.SetActive(true);
        rainGr4BallObj.SetActive(true);
        rainCollObj.SetActive(true);
    }

    public void SetRain_Off()
    {
        rainScr.RainIntensity = 0;
        grObj.SetActive(true);
        gr4BallObj.SetActive(true);

        rainGrObj.SetActive(false);
        rainGr4BallObj.SetActive(false);
        rainCollObj.SetActive(false);
        rainScr.gameObject.SetActive(false);
    }

    public void SetRain_On_Off()
    {
        onOff = !onOff;

        Debug.Log("Rain On = " + onOff);

        if (onOff)
            SetRain_On();
        else
            SetRain_Off();

        isRain = onOff;
    }
}
