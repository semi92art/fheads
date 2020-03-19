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
         PurchaseCoast
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
     
     #endregion

     #region events

     public event System.EventHandler<MoneyCountEventArgs> OnMoneyCountChanged;

     #endregion

 }
