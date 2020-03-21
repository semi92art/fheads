using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buffer : MonoBehaviour
{
    private Scripts scr;
    

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

    public List<CareerManager.CareerGame> _careerGames;
    public CareerManager.CareerGame _careerGame;

    [HideInInspector]
    public bool is2Enemies;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += SetScr;
    }

    private void SetScr(Scene scene, Scene scene1)
    {
        scr = FindObjectOfType<Scripts>();
    }

    void OnDestroy()
    {
        SceneManager.activeSceneChanged -= SetScr;
    }
        
    public void Set_Tournament_Data_0(int _game, int _lg)
    {
        _careerGame = _careerGames[_game];
        SetEnemyData();
    }

    public void Set_Tournament_Data(int _game, int _lg)
    {
        plInd = !PrefsManager.Instance.IsRandomOpponent ?
            PrefsManager.Instance.PlayerIndex : 0;

        SetPlayerData(plInd);
        Set_Tournament_Data_0(_game, _lg);
    }

    private void SetPlayerData(int _Index)
    {
        plSpr = scr.prMng.itemList[_Index].icon;
        plSkillSpeed = scr.prMng.itemList[_Index].skill_Speed;
        plSkillKick = scr.prMng.itemList[_Index].skill_Kick;
        plSkillJump = scr.prMng.itemList[_Index].skill_Jump;
        plCntrInd = scr.prMng.itemList[_Index].cntrInd;
        
        plBoot = scr.cntrL.Countries[plCntrInd].boot;
        plCol1 = scr.cntrL.Countries[plCntrInd].stCol;
        plCol2 = scr.cntrL.Countries[plCntrInd].endCol;
        plColStad = scr.cntrL.Countries[plCntrInd].stadCol;
    }

    private void SetEnemyData()
    {
        int _ind = 0;
        int max_ind = 0;

        if (!PrefsManager.Instance.IsRandomOpponent)
            _ind = EnemyIndex();
        else
        {
            max_ind = 50;
            _ind = Mathf.FloorToInt(((float)max_ind - 0.1f) * Random.value);
        }
        
        enCntrInd = scr.prMng.itemList[_ind].cntrInd;
        enSpr = scr.prMng.itemList[_ind].icon;
        enSkillSpeed = scr.prMng.itemList[_ind].skill_Speed;
        enSkillKick = scr.prMng.itemList[_ind].skill_Kick;
        enSkillJump = scr.prMng.itemList[_ind].skill_Jump;

        enBoot = scr.cntrL.Countries[enCntrInd].boot;
        enCol1 = scr.cntrL.Countries[enCntrInd].stCol;
        enCol2 = scr.cntrL.Countries[enCntrInd].endCol;
        enColStad = scr.cntrL.Countries[enCntrInd].stadCol;
    }

    private int EnemyIndex()
    {
        Names.PlayerName oppName = _careerGame.oppsMain[0].oppName;
        
        int _index = 0;
        
        for (int i = 0; i < scr.prMng.itemList.Count; i++)
            if (oppName == scr.prMng.itemList[i].player)
            {
                _index = i;
                break;
            }
        
        return _index;
    }

    public void SetRandomData()
    {
        int ind = Mathf.FloorToInt((50 - 0.01f) * Random.value);

        SetPlayerData(ind);
        SetEnemyData();
    }
}
