using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Tribunes
{
    public enum Weather { 
        clear,
        rain,
        snow,
        sun,
        rain_and_snow 
    };

    public Weather _weather;
    public Sprite spr_tribunes;
}

public class SkyScript : MonoBehaviour
{
    public Scripts scr;
    [Header("Stadiums and weather:")]
    public List<Tribunes> _tribunes;
    public GameObject bannerGroup1, bannerGroup2;
    public GameObject obj_Splashes;
    public Animator bannersAnim;
    public bool isGoal;
    public bool isRandTribunes;
    public SpriteRenderer[] wallSprs;
    public SpriteRenderer stadiumSprR;
    public Sprite[] tribunesSpr;
    public Color[] randCol;
    public GameObject obj_Snow;

    private float tim;


    void Awake()
    {
        SetTribunes();
        SetWeather();
    }

    void Start()
    {
        PlayerPrefs.SetInt("MenuTrigger_1", 0);

        if (scr.alPrScr.tribunes == 0)
        {
            bannersAnim.enabled = false;
            bannerGroup1.SetActive(false);
            bannerGroup2.SetActive(false);
            obj_Splashes.SetActive(false);
        }
        else
        {
            bannersAnim.enabled = true;
            bannerGroup1.SetActive(true);
            bannerGroup2.SetActive(true);
        }
    }

    void Update()
    {
        if (isGoal && scr.alPrScr.tribunes != 0)
        {
            string call_str = "call";
            int call_int = Animator.StringToHash(call_str);
            bannersAnim.ResetTrigger(call_int);
            bannersAnim.SetTrigger(call_int);
            isGoal = false;
        }
    }
        
    private void SetTribunes()
    {
        /*float rand0 = ((float)tribunesSpr.Length - 0.1f) * Random.value;
        int randStad = Mathf.FloorToInt(rand0);
        stad = scr.alPrScr.isRandGame == 0 ? scr.alPrScr.game : randStad;*/
        stadiumSprR.sprite = _tribunes[scr.alPrScr.tribunes].spr_tribunes;

        int randC = Mathf.FloorToInt(((float)randCol.Length - 0.1f) * Random.value);

        for (int i = 0; i < wallSprs.Length; i++)
            wallSprs[i].color = randCol[randC];
    }

    public void SetWeather()
    {
        switch (_tribunes[scr.alPrScr.tribunes]._weather)
        {
            case Tribunes.Weather.clear:
                scr.rainMan.isRainThisGame = false;
                obj_Snow.SetActive(false);
                break;
            case Tribunes.Weather.rain:
                scr.rainMan.isRainThisGame = true;
                obj_Snow.SetActive(false);
                break;
            case Tribunes.Weather.rain_and_snow:
                scr.rainMan.isRainThisGame = true;
                obj_Snow.SetActive(true);
                break;
            case Tribunes.Weather.snow:
                scr.rainMan.isRainThisGame = false;
                obj_Snow.SetActive(true);
                break;
            case Tribunes.Weather.sun:
                scr.rainMan.isRainThisGame = false;
                obj_Snow.SetActive(false);
                break;
        }
    }
}
