using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;


public class CareerManager : MonoBehaviour
{
    public static CareerManager Instance { get; private set; }
    
    [System.Serializable]
    public class League_UI_Elements
    {
        public Image im_CirclePercent;
        public Image im_Cup;
        public Image[] im_Wins;
        public Sprite spr_CupGold;
        public Sprite spr_CupGray;
        public Text text_cupName_1;
        public Text text_cupName_2;
        public Button _button;
    }
    
    [System.Serializable]
    public class CareerOpponentMain
    {
        public Names.PlayerName oppName;
    }

    [System.Serializable]
    public class CareerGame
    {
        public List<CareerOpponentMain> oppsMain;
    }
    
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

    private List<CareerGame> careerGames;
    private CareerGame m_CurrentGame;

    public CareerGame CurrentGame => m_CurrentGame;

    void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
        
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
                wins++;
                lg_UI_List[i].im_Wins[j].color = col_Gold;
            }

            winsTotal += wins;
            lg_UI_List[i].im_CirclePercent.fillAmount = wins / 10f;
        }

        int winsTotal_1 = Mathf.RoundToInt(100f * winsTotal / 60f); 
        text_winPercent.text = $"{winsTotal_1.ToString()}%";
    }

    public void LeagueButton(int _lg)
    {
        scr.objM.Button_Sound();

        PrefsManager.Instance.Game = 0;
        PrefsManager.Instance.League = _lg;
        SetLeagueData(_lg);
        Set_Tournament_Data_0(0, _lg);
        PlayerPrefs.SetInt("CanRestart", 3);
        anim_LeagueMenu.SetTrigger(Animator.StringToHash($"l{_lg.ToString()}"));
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
                careerGames = lg_1_games;
                break;
            case 1:
                careerGames = lg_2_games;
                break;
            case 2:
                careerGames = lg_3_games;
                break;
            case 3:
                careerGames = lg_4_games;
                break;
            case 4:
                careerGames = lg_5_games;
                break;
            case 5:
                careerGames = lg_6_games;
                break;
        }
    }

    public void UnlockLeague(int _lg)
    {
        PrefsManager.Instance.MoneyCount -= lg_cost[_lg];

        lg_UI_List[_lg].im_Cup.sprite = lg_UI_List[_lg].spr_CupGold;
        lg_UI_List[_lg].text_cupName_1.color = col_Gold;
        lg_UI_List[_lg].text_cupName_2.color = col_Gold;
    }
    
    public void Set_Tournament_Data_0(int _game, int _lg)
    {
        m_CurrentGame =  Instance.careerGames[_game];
    }
}
