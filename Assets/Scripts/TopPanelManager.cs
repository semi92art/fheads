using UnityEngine;
using UnityEngine.UI;

public class TopPanelManager : MonoBehaviour 
{
    #region public fields

    public Text moneyText;

    #endregion

    #region engine methods
    
    private void Awake()
    {
        moneyText.text = Customs.Money(PrefsManager.Instance.MoneyCount);
        PrefsManager.Instance.OnMoneyCountChanged += Event_OnMoneyChanged;
    }
    
    private void OnDestroy()
    {
        PrefsManager.Instance.OnMoneyCountChanged -= Event_OnMoneyChanged;
    }
    
    #endregion
    
    #region private methods

    private void Event_OnMoneyChanged(object _Sender, PrefsManager.MoneyCountEventArgs _Args)
    {
        moneyText.text = Customs.Money(PrefsManager.Instance.MoneyCount);
    }
    
    #endregion

}
