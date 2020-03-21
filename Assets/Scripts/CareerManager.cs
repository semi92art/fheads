using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class CareerManager : MonoBehaviour
{
    [SerializeField]
    private Scripts scr;

    public int[] lg_cost;
    [SerializeField]
    private Text text_winPercent;
    [SerializeField]
    private Color col_Gold;
    [SerializeField]
    private Color col_Gray;
    [SerializeField]
    private Animator anim_LeagueMenu;


    public List<CareerGame> lg_1_games;
    public List<CareerGame> lg_2_games;
    public List<CareerGame> lg_3_games;
    public List<CareerGame> lg_4_games;
    public List<CareerGame> lg_5_games;
    public List<CareerGame> lg_6_games;

    [Header("Leagues UI Elemens:")]
    public List<League_UI_Elements> lg_UI_List;


    void Awake()
    {
        SetLeague_UI();
    }

    private void SetLeague_UI()
    {
        int winsTotal = 0;

        for (int i = 0; i < lg_UI_List.Count; i++)
        {
            lg_UI_List[i].im_Cup.sprite = lg_UI_List[i].spr_CupGold;
            lg_UI_List[i].text_cupName_1.color = col_Gold;
            lg_UI_List[i].text_cupName_2.color = col_Gold;

            int wins = 0;

            for (int j = 0; j < 10; j++)
            {
                if (scr.alPrScr.wonGames[j, i] == 1)
                {
                    wins++;
                    lg_UI_List[i].im_Wins[j].color = col_Gold;
                }
                else
                    lg_UI_List[i].im_Wins[j].color = col_Gray;
            }

            winsTotal += wins;
            lg_UI_List[i].im_CirclePercent.fillAmount = (float)wins / 10f;
        }

        int winsTotal_1 = Mathf.RoundToInt(100f * (float)winsTotal / 60f); 
        text_winPercent.text = winsTotal_1.ToString() + "%";
    }

    public void LeagueButton(int _lg)
    {
        scr.objM.Button_Sound();

        scr.alPrScr.game = 0;
        scr.alPrScr.lg = _lg;
        SetLeagueData(_lg);
        scr.buf.Set_Tournament_Data(0, _lg);
        PlayerPrefs.SetInt("CanRestart", 3);
        anim_LeagueMenu.SetTrigger(Animator.StringToHash("l" + _lg.ToString()));
        GameManager.Instance.SetStadium();
    }

    [HideInInspector]
    public int _lgPrev;

    public void LoadTournament (int _lg)
    {
        GameManager.Instance.LoadSimpleLevel();
    }

    private void SetLeagueData(int _lg)
    {
        switch (_lg)
        {
            case 0:
                scr.buf._careerGames = lg_1_games;
                break;
            case 1:
                scr.buf._careerGames = lg_2_games;
                break;
            case 2:
                scr.buf._careerGames = lg_3_games;
                break;
            case 3:
                scr.buf._careerGames = lg_4_games;
                break;
            case 4:
                scr.buf._careerGames = lg_5_games;
                break;
            case 5:
                scr.buf._careerGames = lg_6_games;
                break;
        }
    }

    public void UnlockLeague(int _lg)
    {
        PrefsManager.Instance.MoneyCount -= lg_cost[_lg];

        scr.topPanMng.moneyText.text = 
            scr.univFunc.moneyString(PrefsManager.Instance.MoneyCount);
        
        lg_UI_List[_lg].im_Cup.sprite = lg_UI_List[_lg].spr_CupGold;
        lg_UI_List[_lg].text_cupName_1.color = col_Gold;
        lg_UI_List[_lg].text_cupName_2.color = col_Gold;
    }
}
