using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusType
{
    public enum bonusType
    {
        BallBig,
        BallSmall,
        PlayerBig,
        PlayerSmall,
        EnemyBig,
        EnemySmall,
        PlayerGatesBig,
        PlayerGatesSmall,
        EnemyGatesBig,
        EnemyGatesSmall,
        PlayerSpeedDown,
        EnemySpeedDown,
        WatchVideo,
        BallClown,
        BallBeach,
        BallRugby
    }
}

public class BonusObjManager : MonoBehaviour 
{
    public Scripts scr;
    [Header("Is Video Watched:")]
    public bool isWatchVideoInPause;
    [Header("Maximum number of bonuses at same time:")]
    public int maxBonSameTime;
    [Header("Existing time of one bonus:")]
    public float bonExistTime;
    [Header("Time between instantiating of bonuses:")]
    public float instDeltaT;
    [HideInInspector]
    public bool isSearchObjReady;
    [Header("Object, searching free space to instantiate a bonus:")]
    public Transform searchForPosTr;

    [Header("Ball Scale Data:")]
    public float ballLineWidthNorm;
    public float ballLineWidthBig;
    public float ballLineWidthSmall;
    [Header("Player Scale Data")]
    public float kickOverHeadTorque_Norm;
    public float kickOverHeadTorque_Big;
    public float kickOverHeadTorque_Small;
    [Header("Enemy Scale Data:")]
    public float distToBall_1_Norm;
    public float distToBall_1_Big;
    public float distToBall_1_Small;
    [Space(5)]
    public float whenKick_State_1_Norm;
    public float whenKick_State_1_Big;
    public float whenKick_State_1_Small;
    [Header("Player Gates Scale Data:")]
    public Vector3 plGatPosNorm;
    public Vector3 plGatScaleNorm;
    [Space(5)]
    public Vector3 plGatPosBig;
    public Vector3 plGatScaleBig;
    [Space(5)]
    public Vector3 plGatPosSmall;
    public Vector3 plGatScaleSmall;
    [Header("Enemy Gates Scale Data:")]
    public Vector3 enGatPosNorm;
    public Vector3 enGatScaleNorm;
    [Space(5)]
    public Vector3 enGatPosBig;
    public Vector3 enGatScaleBig;
    [Space(5)]
    public Vector3 enGatPosSmall;
    public Vector3 enGatScaleSmall;

    private float tim;
    public int[] bonStat;
    private Vector3[] startBonTrs = new Vector3[16];

    [Header("Bonus Objects Transforms:")]
    public Transform ballBigTr;
    public Transform ballSmallTr,
    plBigTr,
    plSmallTr,
    enBigTr,
    enSmallTr,
    plGatBigTr,
    plGatSmallTr,
    enGatBigTr,
    enGatSmallTr,
    plSpDownTr,
    enSpDownTr,
    watchVideoTr,
    clownBallTr,
    beachBallTr,
    rugbyBallTr;

    [Header("Bonus Animators:")]
    public Animator[] anim_Bonuses;

    [Header("Scene Objects:")]
    public Transform plTr;
    public Transform enTr;
    public Transform ballTr;
    public Transform plGatTr;
    public Transform enGatTr;
    public LineRenderer ballLineR;

    [SerializeField]
    private int randBon;
    public SpriteRenderer spr_PlayerMolnia;
    public SpriteRenderer spr_EnemyMolnia;


    void Awake()
    {
        for (int i = 0; i < bonStat.Length; i++)
            bonStat[i] = 1;

        for (int i = 0; i < anim_Bonuses.Length; i++)
            anim_Bonuses[i].enabled = false;

        startBonTrs[0] = ballBigTr.position;
        startBonTrs[1] = ballSmallTr.position;
        startBonTrs[2] = plBigTr.position;
        startBonTrs[3] = plSmallTr.position;
        startBonTrs[4] = enBigTr.position;
        startBonTrs[5] = enSmallTr.position;
        startBonTrs[6] = plGatBigTr.position;
        startBonTrs[7] = plGatSmallTr.position;
        startBonTrs[8] = enGatBigTr.position;
        startBonTrs[9] = enGatSmallTr.position;
        startBonTrs[10] = plSpDownTr.position;
        startBonTrs[11] = enSpDownTr.position;
        startBonTrs[12] = watchVideoTr.position;
        startBonTrs[13] = clownBallTr.position;
        startBonTrs[14] = beachBallTr.position;
        startBonTrs[15] = rugbyBallTr.position;

        SetStartData();

        spr_PlayerMolnia.color = Vector4.zero;
        spr_EnemyMolnia.color = Vector4.zero;
    }

    void Update()
    {
        tim += Time.deltaTime;

        if (tim > instDeltaT)
        {
            if (CurrenNumberOfBonuses() < maxBonSameTime)
            {
                ChooseNewBonus();
                tim = 0.0f;
            }
        }

        if (!isSearchObjReady)
            SearchForPosition();

        BonusTimers();

        if (isMolnia)
        {
            molnia_tim += Time.deltaTime;
            scr.molnia.run = true;

            if (molnia_tim > 0.05f)
                scr.molnia.rend.enabled = true;


            if (molnia_tim > molnia_TimeLength)
            {
                molnia_tim = 0f;
                scr.molnia.run = false;
                isMolnia = false;
                scr.molnia.LightningOff();
            }
        }
    }

    [SerializeField]
    private float molnia_TimeLength;
    private float molnia_tim;
    private bool isMolnia;


    private int CurrenNumberOfBonuses()
    {
        int num = 0;

        for (int i = 0; i < bonStat.Length; i++)
        {
            if (bonStat[i] == 0)
                num++;
        }

        return num;
    }

    private void SearchForPosition()
    {
        float rX_00 = Random.value;
        float rY_00 = Random.value;

        float minX = scr.marks.leftDownBonusTr.position.x;
        float maxX = scr.marks.rightUpBonusTr.position.x;
        float minY = scr.marks.leftDownBonusTr.position.y;
        float maxY = scr.marks.rightUpBonusTr.position.y;

        float rX_0 = rX_00 * (maxX - minX);
        float rY_0 = rY_00 * (maxY - minY);

        float rX = minX + rX_0;
        float rY = minY + rY_0;

        searchForPosTr.position = new Vector3(
            rX,
            rY, 
            searchForPosTr.position.z);
    }

    private void ChooseNewBonus()
    {
        isSearchObjReady = false;

        do
        {
            randBon = Mathf.FloorToInt(Random.value * ((float)bonStat.Length - 0.01f));
        }
        while (bonStat[randBon] == 0 ||
               (randBon == 12 &&
               (isWatchVideoInPause || scr.tM.matchPeriods == 1)));

        bonStat[randBon] = 0;
        anim_Bonuses[randBon].enabled = true;

        switch (randBon)
        {
            case 0:
                SetBonusPosition(BonusType.bonusType.BallBig);
                break;
            case 1:
                SetBonusPosition(BonusType.bonusType.BallSmall);
                break;
            case 2:
                SetBonusPosition(BonusType.bonusType.PlayerBig);
                break;
            case 3:
                SetBonusPosition(BonusType.bonusType.PlayerSmall);
                break;
            case 4:
                SetBonusPosition(BonusType.bonusType.EnemyBig);
                break;
            case 5:
                SetBonusPosition(BonusType.bonusType.EnemySmall);
                break;
            case 6:
                SetBonusPosition(BonusType.bonusType.PlayerGatesBig);
                break;
            case 7:
                SetBonusPosition(BonusType.bonusType.PlayerGatesSmall);
                break;
            case 8:
                SetBonusPosition(BonusType.bonusType.EnemyGatesBig);
                break;
            case 9:
                SetBonusPosition(BonusType.bonusType.EnemyGatesSmall);
                break;
            case 10:
                SetBonusPosition(BonusType.bonusType.PlayerSpeedDown);
                break;
            case 11:
                SetBonusPosition(BonusType.bonusType.EnemySpeedDown);
                break;
            case 12:
                SetBonusPosition(BonusType.bonusType.WatchVideo);
                break;
            case 13:
                SetBonusPosition(BonusType.bonusType.BallClown);
                break;
            case 14:
                SetBonusPosition(BonusType.bonusType.BallBeach);
                break;
            case 15:
                SetBonusPosition(BonusType.bonusType.BallRugby);
                break;
        }
    }

    public void SetBonusPosition(BonusType.bonusType _bonusType)
    {
        switch (_bonusType)
        {
            case BonusType.bonusType.BallBig:
                ballBigTr.position = searchForPosTr.position;
                break;
            case BonusType.bonusType.BallSmall:
                ballSmallTr.position = searchForPosTr.position;
                break;
            case BonusType.bonusType.PlayerBig:
                plBigTr.position = searchForPosTr.position;
                break;
            case BonusType.bonusType.PlayerSmall:
                plSmallTr.position = searchForPosTr.position;
                break;
            case BonusType.bonusType.EnemyBig:
                enBigTr.position = searchForPosTr.position;
                break;
            case BonusType.bonusType.EnemySmall:
                enSmallTr.position = searchForPosTr.position;
                break;
            case BonusType.bonusType.PlayerGatesBig:
                plGatBigTr.position = searchForPosTr.position;
                break;
            case BonusType.bonusType.PlayerGatesSmall:
                plGatSmallTr.position = searchForPosTr.position;
                break;
            case BonusType.bonusType.EnemyGatesBig:
                enGatBigTr.position = searchForPosTr.position;
                break;
            case BonusType.bonusType.EnemyGatesSmall:
                enGatSmallTr.position = searchForPosTr.position;
                break;
            case BonusType.bonusType.PlayerSpeedDown:
                plSpDownTr.position = searchForPosTr.position;
                break;
            case BonusType.bonusType.EnemySpeedDown:
                enSpDownTr.position = searchForPosTr.position;
                break;
            case BonusType.bonusType.WatchVideo:
                watchVideoTr.position = searchForPosTr.position;
                break;
            case BonusType.bonusType.BallClown:
                clownBallTr.position = searchForPosTr.position;
                break;
            case BonusType.bonusType.BallBeach:
                beachBallTr.position = searchForPosTr.position;
                break;
            case BonusType.bonusType.BallRugby:
                rugbyBallTr.position = searchForPosTr.position;
                break;
        }
    }

    /// <summary>
    /// 0 if normal, 1 if big, 2 if small.
    /// </summary>
    private int isBallBig;

    /// <summary>
    /// 0 if normal, 1 if big, 2 if small.
    /// </summary>
    private int isPlayerBig;

    /// <summary>
    /// 0 if normal, 1 if big, 2 if small.
    /// </summary>
    private int isEnemyBig;

    /// <summary>
    /// 0 if normal, 1 if big, 2 if small.
    /// </summary>
    private int isPlayerGatesBig;

    /// <summary>
    /// 0 if normal, 1 if big, 2 if small.
    /// </summary>
    private int isEnemyGatesBig;

    /// <summary>
    /// 0 if normal, 1 if fast, 2 if slow.
    /// </summary>
    [HideInInspector]
    public int isPlayerFast;

    /// <summary>
    /// 0 if normal, 1 if fast, 2 if slow.
    /// </summary>
    [HideInInspector]
    public int isEnemyFast;


    private float 
    timBallBig,
    timPlBig,
    timEnBig,
    timPlGatBig,
    timEnGatBig,
    timPlSpeed,
    timEnSpeed;

    private void BonusTimers()
    {
        if (isBallBig != 0)
        {
            timBallBig += Time.deltaTime;

            if (timBallBig > bonExistTime)
                BallBig(0);
        }

        if (isPlayerBig != 0)
        {
            timPlBig += Time.deltaTime;

            if (timPlBig > bonExistTime)
                PlayerBig(0);
        }

        if (isEnemyBig != 0)
        {
            timEnBig += Time.deltaTime;

            if (timEnBig > bonExistTime)
                EnemyBig(0);
        }

        if (isPlayerGatesBig != 0)
        {
            timPlGatBig += Time.deltaTime;

            if (timPlGatBig > bonExistTime)
                PlayerGatesBig(0);
        }

        if (isEnemyGatesBig != 0)
        {
            timEnGatBig += Time.deltaTime;

            if (timEnGatBig > bonExistTime)
                EnemyGatesBig(0);
        }

        if (isPlayerFast != 0)
        {
            timPlSpeed += Time.deltaTime;
            spr_PlayerMolnia.color = new Vector4(1f, 1f, 1f,
                1.2f - timPlSpeed / bonExistTime);

            if (timPlSpeed > bonExistTime)
            {
                PlayerSpeedUp(0);
                spr_PlayerMolnia.color = Vector4.zero;
            }  
        }

        if (isEnemyFast != 0)
        {
            timEnSpeed += Time.deltaTime;
            spr_EnemyMolnia.color = new Vector4(1f, 1f, 1f,
                1.2f - timEnSpeed / bonExistTime);

            if (timEnSpeed > bonExistTime)
            {
                EnemySpeedUp(0);
                spr_EnemyMolnia.color = Vector4.zero;
            } 
        }
    }

    private Vector3 ballScale_0;
    private Vector3 plScale_0;
    private Vector3 enScale_0;
    private float plSpeed_0;
    private float enSpeed_0;


    private void SetStartData()
    {
        ballScale_0 = ballTr.localScale;
        plScale_0 = plTr.localScale;
        enScale_0 = enTr.localScale;
        plSpeed_0 = Player.Instance.maxSpeed;
        enSpeed_0 = Enemy.Instance.maxSpeed;
    }

    public void BallBig(int step)
    {
        if (step == 1)
        {
            ballBigTr.position = startBonTrs[0];
            bonStat[0] = 1;

            ballTr.localScale = ballScale_0 * 1.5f;
            ballLineR.startWidth = ballLineWidthBig;
            isBallBig = 1;
            anim_Bonuses[0].enabled = false;
        }
        else if (step == 2)
        {
            ballSmallTr.position = startBonTrs[1];
            bonStat[1] = 1;

            ballTr.localScale = ballScale_0 / 1.5f;
            ballLineR.startWidth = ballLineWidthSmall;
            isBallBig = 1;
            anim_Bonuses[1].enabled = false;
        }
        else if (step == 0)
        {
            scr.ballScr.SetBonusBall(0);
            ballTr.localScale = ballScale_0;
            ballLineR.startWidth = ballLineWidthNorm;
            isBallBig = 0;
        }
        else if (step == -1)
        {
            clownBallTr.position = startBonTrs[13];
            bonStat[13] = 1;
            scr.ballScr.SetBonusBall(1);
            ballTr.localScale = ballScale_0;
            ballLineR.startWidth = ballLineWidthNorm;
            isBallBig = 1;
            anim_Bonuses[13].enabled = false;
        }
        else if (step == -2)
        {
            beachBallTr.position = startBonTrs[14];
            bonStat[14] = 1;
            scr.ballScr.SetBonusBall(2);
            ballTr.localScale = ballScale_0;
            ballLineR.startWidth = ballLineWidthNorm;
            isBallBig = 1;
            anim_Bonuses[14].enabled = false;
        }
        else if (step == -3)
        {
            rugbyBallTr.position = startBonTrs[15];
            bonStat[15] = 1;
            scr.ballScr.SetBonusBall(3);
            ballTr.localScale = ballScale_0;
            ballLineR.startWidth = ballLineWidthNorm;
            isBallBig = 1;
            anim_Bonuses[15].enabled = false;
        }

        timBallBig = 0;
    }

    public void PlayerBig(int step)
    {
        if (step == 1)
        {
            plBigTr.position = startBonTrs[2];
            bonStat[2] = 1;

            plTr.localScale = plScale_0 * 1.5f;
            Player.Instance.kickOvHTorq_0 = kickOverHeadTorque_Big;
            isPlayerBig = 1;
            anim_Bonuses[2].enabled = false;
        }
        else if (step == 2)
        {
            plSmallTr.position = startBonTrs[3];
            bonStat[3] = 1;

            plTr.localScale = plScale_0 / 1.3f;
            Player.Instance.kickOvHTorq_0 = kickOverHeadTorque_Small;
            isPlayerBig = 2;
            anim_Bonuses[3].enabled = false;
        }
        else if (step == 0)
        {
            plTr.localScale = plScale_0;
            Player.Instance.kickOvHTorq_0 = kickOverHeadTorque_Norm;
            isPlayerBig = 0;
        }
        else if (step == -1)
        {
            plSmallTr.position = startBonTrs[3];
            bonStat[3] = 1;
        }
        else if (step == -2)
        {
            plBigTr.position = startBonTrs[4];
            bonStat[4] = 1;
        }

        timPlBig = 0;
    }

    public void EnemyBig(int step)
    {
        if (step == 1)
        {
            enBigTr.position = startBonTrs[4];
            bonStat[4] = 1;

            enTr.localScale = enScale_0 * 1.5f;
            Enemy.Instance.kickOvHTorq_0 = -kickOverHeadTorque_Big;
            Enemy.Instance.whenKick_State_1 = whenKick_State_1_Big;
            isEnemyBig = 1;
            anim_Bonuses[4].enabled = false;
        }
        else if (step == 2)
        {
            enSmallTr.position = startBonTrs[5];
            bonStat[5] = 1;

            Enemy.Instance.kickOvHTorq_0 = -kickOverHeadTorque_Small;
            Enemy.Instance.whenKick_State_1 = whenKick_State_1_Small;
            enTr.localScale = enScale_0 / 1.3f;
            isEnemyBig = 2;
            anim_Bonuses[5].enabled = false;
        }
        else if (step == 0)
        {
            Enemy.Instance.kickOvHTorq_0 = -kickOverHeadTorque_Norm;
            Enemy.Instance.whenKick_State_1 = whenKick_State_1_Norm;
            enTr.localScale = enScale_0;
            isEnemyBig = 0;
        }
        else if (step == -1)
        {
            enSmallTr.position = startBonTrs[5];
            bonStat[5] = 1;
        }
        else if (step == -2)
        {
            enBigTr.position = startBonTrs[6];
            bonStat[6] = 1;
        }

        timEnBig = 0;
    }

    public void PlayerGatesBig(int step)
    {
        if (step == 1)
        {
            plGatBigTr.position = startBonTrs[6];
            bonStat[6] = 1;

            plGatTr.position = plGatPosBig;
            plGatTr.localScale = plGatScaleBig;
            isPlayerGatesBig = 1;
            anim_Bonuses[6].enabled = false;
        }
        else if (step == 2)
        {
            plGatSmallTr.position = startBonTrs[7];
            bonStat[7] = 1;

            plGatTr.position = plGatPosSmall;
            plGatTr.localScale = plGatScaleSmall;
            isPlayerGatesBig = 2;
            anim_Bonuses[7].enabled = false;
        }
        else if (step == 0)
        {
            plGatTr.position = plGatPosNorm;
            plGatTr.localScale = plGatScaleNorm;
            isPlayerGatesBig = 0;
        }
        else if (step == -1)
        {
            plGatBigTr.position = startBonTrs[6];
            bonStat[6] = 1;
        }
        else if (step == -2)
        {
            plGatSmallTr.position = startBonTrs[7];
            bonStat[7] = 1;
        }

        timPlGatBig = 0;
    }

    public void EnemyGatesBig(int step)
    {
        if (step == 1)
        {
            enGatBigTr.position = startBonTrs[8];
            bonStat[8] = 1;

            enGatTr.position = enGatPosBig;
            enGatTr.localScale = enGatScaleBig;
            isEnemyGatesBig = 1;
            anim_Bonuses[8].enabled = false;
        }
        else if (step == 2)
        {
            enGatSmallTr.position = startBonTrs[9];
            bonStat[9] = 1;

            enGatTr.position = enGatPosSmall;
            enGatTr.localScale = enGatScaleSmall;
            isEnemyGatesBig = 2;
            anim_Bonuses[9].enabled = false;
        }
        else if (step == 0)
        {
            enGatTr.position = enGatPosNorm;
            enGatTr.localScale = enGatScaleNorm;
            isEnemyGatesBig = 0;
        }
        else if (step == -1)
        {
            enGatBigTr.position = startBonTrs[8];
            bonStat[8] = 1;
        }
        else if (step == -2)
        {
            enGatSmallTr.position = startBonTrs[9];
            bonStat[9] = 1;
        }

        timEnGatBig = 0;
    }

    public void PlayerSpeedUp(int step)
    {
        if (step == 2)
        {
            plSpDownTr.position = startBonTrs[10];
            bonStat[10] = 1;
            scr.levAudScr.molniya.Play();
            Player.Instance.maxSpeed = 0f;
            scr.molnia.target = Player.Instance.gameObject;
            isMolnia = true;
            isPlayerFast = 2;
            anim_Bonuses[10].enabled = false;
        }
        else if (step == 0)
        {
            Player.Instance.maxSpeed = plSpeed_0;
            isPlayerFast = 0;
        }
        else if (step == -1)
        {
            plSpDownTr.position = startBonTrs[10];
            bonStat[10] = 1;
        }

        timPlSpeed = 0;
    }

    public void EnemySpeedUp(int step)
    {
        if (step == 2)
        {
            enSpDownTr.position = startBonTrs[11];
            bonStat[11] = 1;
            scr.levAudScr.molniya.Play();
            Enemy.Instance.maxSpeed = 0f;
            scr.jScr.isMolniya = true;
            scr.molnia.target = Enemy.Instance.gameObject;
            isMolnia = true;
            isEnemyFast = 2;
            anim_Bonuses[11].enabled = false;
        }
        else if (step == 0)
        {
            scr.jScr.isMolniya = false;
            Enemy.Instance.maxSpeed = enSpeed_0;
            isEnemyFast = 0;
        }
        else if (step == -1)
        {
            enSpDownTr.position = startBonTrs[11];
            bonStat[11] = 1;
        }

        timEnSpeed = 0;
    }

    [HideInInspector]
    public bool isVideoCalled;

    public void WatchVideo(int step)
    {
        switch (step)
        {
            case 0:
                watchVideoTr.position = startBonTrs[12];
                isWatchVideoInPause = true;
                anim_Bonuses[12].enabled = false;
                break;
            case 1:
                isVideoCalled = true;
                isWatchVideoInPause = false;
                FindObjectOfType<UnityAds_0>().ShowRewardedAd();
                break;
            case 2:
                watchVideoTr.position = startBonTrs[12];
                break;
        }
    }
}
