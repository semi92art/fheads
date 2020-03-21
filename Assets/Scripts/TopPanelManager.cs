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
        moneyText.text = scr.univFunc.Money(PrefsManager.Instance.MoneyCount);
        PrefsManager.Instance.OnMoneyCountChanged += (_Sender, _Args) =>
            moneyText.text = scr.univFunc.Money(PrefsManager.Instance.MoneyCount);
    }
}
