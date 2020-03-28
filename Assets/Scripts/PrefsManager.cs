using JetBrains.Annotations;
using UnityEngine;

 public class PrefsManager : MonoBehaviour
 {
     #region static proterties
     
     public static PrefsManager Instance { get; private set; }
     
     #endregion
     
     #region engine methods

     private void Awake()
     {
         Debug.LogWarning("PrefsManagerAwake!");
         if (Instance != null)
         {
             DestroyImmediate(gameObject);
             return;
         }

         Instance = this;
         DontDestroyOnLoad(gameObject);
     }
     
     #endregion
     
     #region private types
     
     private enum PrefKey
     {
         PlayedGames = 0,
         PlayerIndex,
         ProfileIndex,
         CameraType,
         MoneyCount,
         LaunchesCount,
         ControlsType,
         IsRandomOpponent,
         Stadium,
         Tribunes,
         League,
         Game,
         UpgradeSpeed,
         UpgradeJump,
         UpgradeKick,
         UpgradeShield,
         UpgradeMoneyIncome,
         BycicleKickEnabled,
         GraphicsQuality,
         Tilt,
         ButtonsSize,
         SoundOn,
         Language
     }

     public class MoneyCountEventArgs : System.EventArgs
     {
         public readonly int moneyCount;

         public MoneyCountEventArgs(int _MoneyCount)
         {
             moneyCount = _MoneyCount;
         }
     }
     
     #endregion
     
     #region private methods
     
     private int GetIntPref(PrefKey _Key)
     {
         return PlayerPrefs.GetInt(_Key.ToString());
     }

     private void SetPref(PrefKey _Key, int _Value)
     {
         PlayerPrefs.SetInt(_Key.ToString(), _Value);
     }
     
     
     #endregion

     #region private fields

     private int? m_PlayedGames;
     private int? m_PlayerIndex;
     private int? m_ProfileIndex;
     private int? m_CameraType;
     private int? m_MoneyCount;
     private int? m_LaunchesCount;
     private int? m_ControlsType;
     private bool? m_IsRandomOpponent;
     private int? m_Stadium;
     private int? m_Tribunes;
     private int? m_League;
     private int? m_Game;
     private int? m_UpgradeSpeed;
     private int? m_UpgradeKick;
     private int? m_UpgradeJump;
     private int? m_UpgradeShield;
     private int? m_UpgradeMoneyIncome;
     private bool? m_BycicleKickEnabled;
     private int? m_GraphicsQuality;
     private bool? m_Tilt;
     private int? m_ButtonsSize;
     private bool? m_SoundOn;
     private int? m_Language;

     #endregion
     
     #region public methods
     
     public int PlayedGames
     {
         get
         {
             if (m_PlayedGames == null)
                 m_PlayedGames = GetIntPref(PrefKey.PlayedGames);
             return (int)m_PlayedGames;
         }
         set
         {
             m_PlayedGames = value;
             SetPref(PrefKey.PlayedGames, value);
         }
     }

     public int PlayerIndex
     {
         get
         {
             if (m_PlayerIndex == null)
                 m_PlayerIndex = GetIntPref(PrefKey.PlayerIndex);
             return (int)m_PlayerIndex;
         }
         set
         {
             m_PlayerIndex = value;
             SetPref(PrefKey.PlayerIndex, value);
         }
     }

     public int ProfileIndex
     {
         get
         {
             if (m_ProfileIndex == null)
                 m_ProfileIndex = GetIntPref(PrefKey.ProfileIndex);
             return (int)m_ProfileIndex;
         }
         set
         {
             m_ProfileIndex = value;
             SetPref(PrefKey.ProfileIndex, value);
         }
     }

     public int CameraType
     {
         get
         {
             if (m_CameraType == null)
                 m_CameraType = GetIntPref(PrefKey.CameraType);
             return (int)m_CameraType;
         }
         set
         {
             m_CameraType = value;
             SetPref(PrefKey.CameraType, value);
         }
     }
     
     public int MoneyCount
     {
         get
         {
             if (m_MoneyCount == null)
                 m_MoneyCount = GetIntPref(PrefKey.MoneyCount);
             return (int)m_MoneyCount;
         }
         set
         {
             m_MoneyCount = value;
             SetPref(PrefKey.MoneyCount, Mathf.Max(0, value));
             OnMoneyCountChanged?.Invoke(this, new MoneyCountEventArgs(value));
         }
     }

     public int LaunchesCount
     {
         get
         {
             if (m_LaunchesCount == null)
                 m_LaunchesCount = GetIntPref(PrefKey.LaunchesCount);
             return (int)m_LaunchesCount;
         }
         set
         {
             m_LaunchesCount = value;
             SetPref(PrefKey.LaunchesCount, Mathf.Max(0, value));
         }
     }

     public int ControlsType
     {
         get
         {
             if (m_ControlsType == null)
                 m_ControlsType = GetIntPref(PrefKey.ControlsType);
             return (int)m_ControlsType;
         }
         set
         {
             m_ControlsType = value;
             SetPref(PrefKey.ControlsType, Mathf.Max(0, value));
         }
     }
     
     public bool IsRandomOpponent
     {
         get
         {
             if (m_IsRandomOpponent == null)
                 m_IsRandomOpponent = GetIntPref(PrefKey.IsRandomOpponent) != 0;
             return (bool)m_IsRandomOpponent;
         }
         set
         {
             m_IsRandomOpponent = value;
             SetPref(PrefKey.IsRandomOpponent, value ? 1 : 0);
         }
     }
     
     public int Stadium
     {
         get
         {
             if (m_Stadium == null)
                 m_Stadium = GetIntPref(PrefKey.Stadium);
             return (int)m_Stadium;
         }
         set
         {
             m_Stadium = value;
             SetPref(PrefKey.Stadium, value);
         }
     }
     
     public int Tribunes
     {
         get
         {
             if (m_Tribunes == null)
                 m_Tribunes = GetIntPref(PrefKey.Tribunes);
             return (int)m_Tribunes;
         }
         set
         {
             m_Tribunes = value;
             SetPref(PrefKey.Tribunes, value);
         }
     }

     public int League
     {
         get
         {
             if (m_League == null)
                 m_League = GetIntPref(PrefKey.League);
             return (int)m_League;
         }
         set
         {
             m_League = value;
             SetPref(PrefKey.League, value);
         }
     }
     
     public int Game
     {
         get
         {
             if (m_Game == null)
                 m_Game = GetIntPref(PrefKey.Game);
             return (int)m_Game;
         }
         set
         {
             m_Game = value;
             SetPref(PrefKey.Game, value);
         }
     }
     
     public int UpgradeSpeed
     {
         get
         {
             if (m_UpgradeSpeed == null)
                 m_UpgradeSpeed = GetIntPref(PrefKey.UpgradeSpeed);
             return (int)m_UpgradeSpeed;
         }
         set
         {
             m_UpgradeSpeed = value;
             SetPref(PrefKey.UpgradeSpeed, value);
         }
     }
     
     public int UpgradeKick
     {
         get
         {
             if (m_UpgradeKick == null)
                 m_UpgradeKick = GetIntPref(PrefKey.UpgradeKick);
             return (int)m_UpgradeKick;
         }
         set
         {
             m_UpgradeKick = value;
             SetPref(PrefKey.UpgradeKick, value);
         }
     }
     
     public int UpgradeJump
     {
         get
         {
             if (m_UpgradeJump == null)
                 m_UpgradeJump = GetIntPref(PrefKey.UpgradeJump);
             return (int)m_UpgradeJump;
         }
         set
         {
             m_UpgradeJump = value;
             SetPref(PrefKey.UpgradeJump, value);
         }
     }
     
     public int UpgradeShield
     {
         get
         {
             if (m_UpgradeShield == null)
                 m_UpgradeShield = GetIntPref(PrefKey.UpgradeShield);
             return (int)m_UpgradeShield;
         }
         set
         {
             m_UpgradeShield = value;
             SetPref(PrefKey.UpgradeShield, value);
         }
     }
     
     public int UpgradeMoneyIncome
     {
         get
         {
             if (m_UpgradeMoneyIncome == null)
                 m_UpgradeMoneyIncome = GetIntPref(PrefKey.UpgradeMoneyIncome);
             return (int)m_UpgradeMoneyIncome;
         }
         set
         {
             m_UpgradeMoneyIncome = value;
             SetPref(PrefKey.UpgradeMoneyIncome, value);
         }
     }
     
     public bool BycicleKickEnabled
     {
         get
         {
             if (m_BycicleKickEnabled == null)
                 m_BycicleKickEnabled = GetIntPref(PrefKey.BycicleKickEnabled) != 0;
             return (bool)m_BycicleKickEnabled;
         }
         set
         {
             m_BycicleKickEnabled = value;
             SetPref(PrefKey.BycicleKickEnabled, value ? 1 : 0);
         }
     }
     
     public int GraphicsQuality
     {
         get
         {
             if (m_GraphicsQuality == null)
                 m_GraphicsQuality = GetIntPref(PrefKey.GraphicsQuality);
             return (int)m_GraphicsQuality;
         }
         set
         {
             m_GraphicsQuality = value;
             if (m_GraphicsQuality == 3)
                 m_GraphicsQuality = 0;
             SetPref(PrefKey.GraphicsQuality, (int)m_GraphicsQuality);
         }
     }
     
     public bool Tilt
     {
         get
         {
             if (m_Tilt == null)
                 m_Tilt = GetIntPref(PrefKey.Tilt) != 0;
             return (bool)m_Tilt;
         }
         set
         {
             m_Tilt = value;
             SetPref(PrefKey.Tilt, value ? 1 : 0);
         }
     }
     
     public int ButtonsSize
     {
         get
         {
             if (m_ButtonsSize == null)
                 m_ButtonsSize = GetIntPref(PrefKey.ButtonsSize);
             return (int)m_ButtonsSize;
         }
         set
         {
             m_ButtonsSize = value;
             SetPref(PrefKey.ButtonsSize, value);
         }
     }
     
     public bool SoundOn
     {
         get
         {
             if (m_SoundOn == null)
                 m_SoundOn = GetIntPref(PrefKey.SoundOn) != 0;
             return (bool)m_SoundOn;
         }
         set
         {
             m_SoundOn = value;
             SetPref(PrefKey.SoundOn, value ? 1 : 0);
         }
     }
     
     public int Language
     {
         get
         {
             if (m_Language == null)
                 m_Language = GetIntPref(PrefKey.Language);
             return (int)m_Language;
         }
         set
         {
             m_Language = value;
             SetPref(PrefKey.Language, value);
         }
     }

     #endregion

     #region events

     public event System.EventHandler<MoneyCountEventArgs> OnMoneyCountChanged;

     #endregion

 }
