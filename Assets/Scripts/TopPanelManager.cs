using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TopPanelManager : MonoBehaviour 
{
    public Scripts scr;

    public Text moneyText;
    private int m_MoneyCount;


    private void Awake()
    {
        moneyText.text = Customs.Money(PrefsManager.Instance.MoneyCount);
        PrefsManager.Instance.OnMoneyCountChanged += (_Sender, _Args) =>
            moneyText.text = Customs.Money(PrefsManager.Instance.MoneyCount);
    }
}
