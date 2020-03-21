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
         if (Instance != null)
         {
             DestroyImmediate(gameObject);
             return;
         }

         Instance = this;
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
         PurchaseCoast,
         LaunchesCount,
         ControlsType,
         IsRandomOpponent,
         Stadium,
         Tribunes,
         OpenedPlayers,
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
     private int? m_PurchaseCoast;
     private int? m_LaunchesCount;
     private bool? m_IsLegend;
     private int? m_ControlsType;
     private bool? m_IsRandomOpponent;
     private int? m_Stadium;
     private int? m_Tribunes;
     [CanBeNull] private int[] m_OpenedPlayers;
     [CanBeNull] private int[] m_OpenedRetroPlayers;
     
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
     
     public int PurchaseCoast
     {
         get
         {
             if (m_PurchaseCoast == null)
                 m_PurchaseCoast = GetIntPref(PrefKey.PurchaseCoast);
             return (int)m_PurchaseCoast;
         }
         set
         {
             m_PurchaseCoast = value;
             SetPref(PrefKey.PurchaseCoast, Mathf.Max(0, value));
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
             m_IsLegend = value;
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
     
     
     #endregion

     #region events

     public event System.EventHandler<MoneyCountEventArgs> OnMoneyCountChanged;

     #endregion

 }
