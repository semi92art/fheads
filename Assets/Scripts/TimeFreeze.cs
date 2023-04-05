using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeFreeze : MonoBehaviour
{
    public Scripts scr;
    [Space(5)]
    [Header("Freeze Time:")]
    public float freezeTime;
    [Header("Freeze Scale:")]
    public float frTimeScale;
    public Text text_FreezeTime;
    public GameObject obj_getSlowdownButton;
    public GameObject obj_TimeFreezeButton;
    public Image sprRend_TimFrButton;
    public Sprite spr_TimFr_0, spr_TimFr_1;

    public bool isHandleUnlim;

    [HideInInspector]
    public bool isFreeze;
    private bool isStopFreezeTime;
    private int freezeCount;
    private bool isNoFreezes;
    public bool timFr;
    private int freezeTime_2;
    private int checkFreezeTime_2;


    void Awake()
    {
        freezeTime = (freezeTime + (float)scr.alPrScr.upgrSlowdown) * frTimeScale;
        timFr = scr.univFunc.Int2Bool(PlayerPrefs.GetInt("UnlimFreeze"));
        if (!timFr) freezeCount = 1;
        if (timFr || isHandleUnlim) EnableUnlimitedFreeze();

        float freezeTime_1 = freezeTime / frTimeScale;
        text_FreezeTime.text = freezeTime_1.ToString("N1") + "s";
    }

    public void EnableUnlimitedFreeze()
    {
        timFr = true;
        obj_getSlowdownButton.SetActive(false);
    }
        
    private bool isTextDisabled;

    void Update()
    {
        if (isFreeze)
        {
            text_FreezeTime.enabled = freezeTime > 0f ? true : false;
            freezeTime = !timFr ? freezeTime - Time.deltaTime : freezeTime;

            float freezeTime_1 = Time.timeScale > 0f ?
                freezeTime / Time.timeScale : freezeTime / scr.gM.currTimeScale;

            freezeTime_2 = Mathf.RoundToInt(freezeTime_1 * 10f);

            if (freezeTime_2 != checkFreezeTime_2)
                text_FreezeTime.text = freezeTime_1.ToString("N1") + "s";    

            if (freezeTime <= 0)
            {
                text_FreezeTime.enabled = false;
                isStopFreezeTime = true;
                isFreeze = false;   

                if (isTextDisabled)
                    GetComponent<TimeFreeze>().enabled = false;

                isTextDisabled = true;
            }
        }

        checkFreezeTime_2 = freezeTime_2;

        if (isStopFreezeTime)
        {
            TimeFreeze_Stop();
            freezeCount--;

            if (freezeCount == 0)
            {
                isNoFreezes = true;
                //sprRend_TimFrButton.color = Color.clear;
                sprRend_TimFrButton.enabled = false;
            }

            isStopFreezeTime = false;
        }
    }

    public void TimeFreeze_StartOrStop()
    {
        if (isFreeze)
            TimeFreeze_Stop();
        else
            TimeFreeze_Start(); 
    }

    public void TimeFreeze_Start()
    {
        if (!isNoFreezes)
        {
            sprRend_TimFrButton.sprite = spr_TimFr_1;
            isFreeze = true;
            Time.fixedDeltaTime = 0.01f * frTimeScale;
            Time.timeScale = frTimeScale;

            scr.levAudScr.timeSlow_In.Play();

            if (scr.levAudScr.timeSlow_Out.isPlaying)
                scr.levAudScr.timeSlow_Out.Stop();
        }
    }

    public void TimeFreeze_Stop()
    {
        sprRend_TimFrButton.sprite = spr_TimFr_0;
        isFreeze = false;
        Time.fixedDeltaTime = 0.01f;
        Time.timeScale = 1f;

        scr.levAudScr.timeSlow_Out.Play();

        if (scr.levAudScr.timeSlow_In.isPlaying)
            scr.levAudScr.timeSlow_In.Stop();
    }

    public void GetAdditionalFreeze_0()
    {
        /*#if UNITY_EDITOR
        GetAdditionalFreeze(10f);
        #else
        scr.univFunc.ShowRewardedVideoAd();
        #endif*/
    }

    public void GetAdditionalFreeze(float addFreezeTime)
    {
        freezeTime += addFreezeTime;

        isNoFreezes = false;
        sprRend_TimFrButton.color = new Vector4(
            1f, 1f, 1f, scr.objLev.scrBar_ButtCap.value);
    }

    public void DisableTimeFreezeControls()
    {
        obj_TimeFreezeButton.SetActive(false);
        sprRend_TimFrButton.gameObject.SetActive(false);
    }

    public void EnableTimeFreezeControls()
    {
        obj_TimeFreezeButton.SetActive(true);
        sprRend_TimFrButton.gameObject.SetActive(true);
    }
}
