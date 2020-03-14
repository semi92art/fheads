using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buffer : MonoBehaviour
{
    private Scripts scr;

    public bool isLevelStarted;
    public bool is1stPractice;
    public bool isPractice;

    public int plInd;
    public int plCntrInd;
    public float plSkillSpeed;
    public float plSkillKick;
    public float plSkillJump;
    public Sprite plSpr;
    public Sprite plBoot;
    public Color plCol1, plCol2, plColStad;

    public int enInd;
    public int enCntrInd;
    public float enSkillSpeed;
    public float enSkillKick;
    public float enSkillJump;
    public Sprite enSpr;
    public Sprite enBoot;
    public Color enCol1, enCol2, enColStad;
    public OpponentType oppType;

    public int enInd_1;
    public int enCntrInd_1;
    public float enSkillSpeed_1;
    public float enSkillKick_1;
    public float enSkillJump_1;
    public Sprite enSpr_1;
    public Sprite enBoot_1;
    public Color enCol1_1, enCol2_1, enColStad_1;
    public OpponentType oppType_1;

    public List<CareerGame> _careerGames;
    public CareerGame _careerGame;

    [HideInInspector]
    public bool is2Enemies;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += SetScr;
        SceneManager.activeSceneChanged += OnLevelWasLoaded_0;
    }

    private void OnLevelWasLoaded_0(Scene scene, Scene scene1)
    {
        if (scene1.name == "Level")
            isLevelStarted = true;
    }

    private void SetScr(Scene scene, Scene scene1)
    {
        scr = FindObjectOfType<Scripts>();
    }

    void OnDestroy()
    {
        SceneManager.activeSceneChanged -= SetScr;
        SceneManager.activeSceneChanged -= OnLevelWasLoaded_0;
    }
        
    public void Set_Tournament_Data_0(int _game, int _lg)
    {
        _careerGame = _careerGames[_game];

        switch (_careerGame.oppsNumAndAge)
        {
            case OpponentsNumAndAge.Main:
                is2Enemies = false;
                SetEnemyData(0, 0);
                break;
            case OpponentsNumAndAge.Legend:
                is2Enemies = false;
                SetEnemyData(0, 1);
                break;
            case OpponentsNumAndAge.Main_Main:
                is2Enemies = true;
                SetEnemyData(0, 0);
                SetEnemyData(1, 0);
                break;
            case OpponentsNumAndAge.Legend_Legend:
                is2Enemies = true;
                SetEnemyData(0, 1);
                SetEnemyData(1, 1);
                break;
            case OpponentsNumAndAge.Main_Legend:
                is2Enemies = true;
                SetEnemyData(0, 0);
                SetEnemyData(1, 1);
                break;
            case OpponentsNumAndAge.Legend_Main:
                is2Enemies = true;
                SetEnemyData(0, 1);
                SetEnemyData(1, 0);
                break;
        }
    }

    public void Set_Tournament_Data(int _game, int _lg)
    {
        plInd = scr.alPrScr.isRandGame == 0 ?
            scr.alPrScr.playerIndex : scr.alPrScr.playerIndexRand;

        SetPlayerData(plInd, scr.alPrScr.plLg);
        Set_Tournament_Data_0(_game, _lg);
    }

    private void SetPlayerData(int _ind, int _lg)
    {
        switch (_lg)
        {
            case 1:
                plSpr = scr.prMng.itemList[_ind].icon;
                plSkillSpeed = scr.prMng.itemList[_ind].skill_Speed;
                plSkillKick = scr.prMng.itemList[_ind].skill_Kick;
                plSkillJump = scr.prMng.itemList[_ind].skill_Jump;
                plCntrInd = scr.prMng.itemList[_ind].cntrInd;
                break;
            case 2:
                plSpr = scr.prMng.itemList_2[_ind].icon;
                plSkillSpeed = scr.prMng.itemList_2[_ind].skill_Speed;
                plSkillKick = scr.prMng.itemList_2[_ind].skill_Kick;
                plSkillJump = scr.prMng.itemList_2[_ind].skill_Jump;
                plCntrInd = scr.prMng.itemList_2[_ind].cntrInd;
                break;
        }

        plBoot = scr.cntrL.Countries[plCntrInd].boot;
        plCol1 = scr.cntrL.Countries[plCntrInd].stCol;
        plCol2 = scr.cntrL.Countries[plCntrInd].endCol;
        plColStad = scr.cntrL.Countries[plCntrInd].stadCol;
    }

    private void SetEnemyData(int _num, int _lg)
    {
        int _ind = 0;
        int max_ind = 0;

        if (scr.alPrScr.isRandGame == 0)
            _ind = EnemyIndex(_num, _lg);
        else
        {
            max_ind = _lg == 0 ? 
                scr.alPrScr.openedPlayers.Length : scr.alPrScr.openedPlayers_2.Length;
            _ind = Mathf.FloorToInt(((float)max_ind - 0.1f) * Random.value);
        }

        Debug.Log("League = " + _lg + ", Index = " + _ind);

        if (_num == 0)
        {
            switch (_lg)
            {
                case 0:
                    enCntrInd = scr.prMng.itemList[_ind].cntrInd;
                    enSpr = scr.prMng.itemList[_ind].icon;
                    enSkillSpeed = scr.prMng.itemList[_ind].skill_Speed;
                    enSkillKick = scr.prMng.itemList[_ind].skill_Kick;
                    enSkillJump = scr.prMng.itemList[_ind].skill_Jump;
                    break;
                case 1:
                    enCntrInd = scr.prMng.itemList_2[_ind].cntrInd;
                    enSpr = scr.prMng.itemList_2[_ind].icon;
                    enSkillSpeed = scr.prMng.itemList_2[_ind].skill_Speed;
                    enSkillKick = scr.prMng.itemList_2[_ind].skill_Kick;
                    enSkillJump = scr.prMng.itemList_2[_ind].skill_Jump;
                    break;
            }

            enBoot = scr.cntrL.Countries[enCntrInd].boot;
            enCol1 = scr.cntrL.Countries[enCntrInd].stCol;
            enCol2 = scr.cntrL.Countries[enCntrInd].endCol;
            enColStad = scr.cntrL.Countries[enCntrInd].stadCol;
        }
        else if (_num == 1)
        {
            switch (_lg)
            {
                case 0:
                    enCntrInd_1 = scr.prMng.itemList[_ind].cntrInd;
                    enSpr_1 = scr.prMng.itemList[_ind].icon;
                    enSkillSpeed_1 = scr.prMng.itemList[_ind].skill_Speed;
                    enSkillKick_1 = scr.prMng.itemList[_ind].skill_Kick;
                    enSkillJump_1 = scr.prMng.itemList[_ind].skill_Jump;
                    break;
                case 1:
                    enCntrInd_1 = scr.prMng.itemList_2[_ind].cntrInd;
                    enSpr_1 = scr.prMng.itemList_2[_ind].icon;
                    enSkillSpeed_1 = scr.prMng.itemList_2[_ind].skill_Speed;
                    enSkillKick_1 = scr.prMng.itemList_2[_ind].skill_Kick;
                    enSkillJump_1 = scr.prMng.itemList_2[_ind].skill_Jump;
                    break;
            }
        }

        enBoot_1 = scr.cntrL.Countries[enCntrInd_1].boot;
        enCol1_1 = scr.cntrL.Countries[enCntrInd_1].stCol;
        enCol2_1 = scr.cntrL.Countries[enCntrInd_1].endCol;
        enColStad_1 = scr.cntrL.Countries[enCntrInd_1].stadCol;
    }

    private int EnemyIndex(int _num, int _lg)
    {
        OpponentType oppType_0 = OpponentType.Bycicle;
        Names.PlayerName oppName = Names.PlayerName.Aguero;
        Names.PlayerName_2 oppName_2 = Names.PlayerName_2.Baggio;

        switch (_careerGame.oppsNumAndAge)
        {
            case OpponentsNumAndAge.Main:
                oppType_0 = _careerGame.oppsMain[_num].oppType;
                oppName = _careerGame.oppsMain[_num].oppName;
                break;
            case OpponentsNumAndAge.Legend:
                oppType_0 = _careerGame.oppsLegend[_num].oppType;
                oppName_2 = _careerGame.oppsLegend[_num].oppName_2;
                break;
            case OpponentsNumAndAge.Main_Main:
                oppType_0 = _careerGame.oppsMain[_num].oppType;
                oppName = _careerGame.oppsMain[_num].oppName;
                break;
            case OpponentsNumAndAge.Legend_Legend:
                oppType_0 = _careerGame.oppsLegend[_num].oppType;
                oppName_2 = _careerGame.oppsLegend[_num].oppName_2;
                break;
            case OpponentsNumAndAge.Main_Legend:
                if (_num == 0)
                {
                    oppType_0 = _careerGame.oppsMain[0].oppType;
                    oppName = _careerGame.oppsMain[0].oppName;
                }
                else if (_num == 1)
                {
                    oppType_0 = _careerGame.oppsLegend[0].oppType;
                    oppName_2 = _careerGame.oppsLegend[0].oppName_2;
                }
                break;
            case OpponentsNumAndAge.Legend_Main:
                if (_num == 0)
                {
                    oppType_0 = _careerGame.oppsLegend[0].oppType;
                    oppName_2 = _careerGame.oppsLegend[0].oppName_2;
                }
                else if (_num == 1) 
                {
                    oppType_0 = _careerGame.oppsMain[0].oppType;
                    oppName = _careerGame.oppsMain[0].oppName;
                }
                break;
        }

        switch (_num)
        {
            case 0:
                oppType = oppType_0;
                break;
            case 1:
                oppType_1 = oppType_0;
                break;
        }

        int _index = 0;

        if (_lg == 0)
        {
            for (int i = 0; i < scr.prMng.itemList.Count; i++)
            {
                if (oppName == scr.prMng.itemList[i].player)
                {
                    _index = i;
                    break;
                }
                    
            }
        }
        else if (_lg == 1)
        {
            for (int i = 0; i < scr.prMng.itemList_2.Count; i++)
            {
                if (oppName_2 == scr.prMng.itemList_2[i].player)
                {
                    _index = i;
                    break;
                }
            }
        }

        return _index;
    }

    public void SetRandomData()
    {
        int lg = Random.value < 0.5f ? 1 : 2;
        int max_ind = lg == 1? 
            scr.alPrScr.openedPlayers.Length : scr.alPrScr.openedPlayers_2.Length;
        int ind = Mathf.FloorToInt(((float)max_ind - 0.01f) * Random.value);

        SetPlayerData(ind, lg);
        
        //number of opponent players
        //is2Enemies = Random.value > 0.5f ? true : false;
        is2Enemies = false;
        oppType = Random.value > 0.5f ? OpponentType.Bycicle : OpponentType.Classic;

        lg = Random.value < 0.5f ? 0 : 1;
        SetEnemyData(0, lg);

        if (is2Enemies)
        {
            lg = Random.value < 0.5f ? 0 : 1;
            SetEnemyData(1, lg);
        }
    }
}
