﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

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
    private int freezeTime_2;
    private int checkFreezeTime_2;


    void Awake()
    {
        freezeTime = freezeTime * frTimeScale;

        float freezeTime_1 = freezeTime / frTimeScale;
        text_FreezeTime.text = freezeTime_1.ToString("N1") + "s";
    }

        
    private bool isTextDisabled;

    void Update()
    {
        if (isFreeze)
        {
            text_FreezeTime.enabled = freezeTime > 0f;
            freezeTime = freezeTime - Time.deltaTime;

            float freezeTime_1 = !TimeManager.Instance.GamePaused ?
                freezeTime / TimeManager.Instance.TimeScale : freezeTime / GameManager.Instance.currTimeScale;

            freezeTime_2 = Mathf.RoundToInt(freezeTime_1 * 10f);

            if (freezeTime_2 != checkFreezeTime_2)
                text_FreezeTime.text = freezeTime_1.ToString("N1") + "s";    

            if (freezeTime <= 0)
            {
                text_FreezeTime.enabled = false;
                isStopFreezeTime = true;
                isFreeze = false;

                if (isTextDisabled)
                    enabled = false;

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
            TimeManager.Instance.TimeScale = frTimeScale;

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
        TimeManager.Instance.TimeScale = 1f;

        scr.levAudScr.timeSlow_Out.Play();

        if (scr.levAudScr.timeSlow_In.isPlaying)
            scr.levAudScr.timeSlow_In.Stop();
    }

    public void GetAdditionalFreeze_0()
    {
        /*#if UNITY_EDITOR
        GetAdditionalFreeze(10f);
        #else
        Customs.ShowRewardedVideoAd();
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
