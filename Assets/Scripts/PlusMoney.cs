using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlusMoney : MonoBehaviour 
{
    public Scripts scr;
    public int moneyForGoal;


    public void Set_Money()
    {
        float coeff_0 = Mathf.Pow((PrefsManager.Instance.League + 1), 2f);
        float coeff_1 = Mathf.Pow((PrefsManager.Instance.Game + 1), 1.5f);
        float coeff_2 = Score.score * 50f + 300f;
        float upgrCoeff = (100f + PrefsManager.Instance.UpgradeMoneyIncome * 5f)/100f;
        float moneyForGoal_0 = coeff_0 * coeff_1 * coeff_2 * upgrCoeff;
        moneyForGoal = (int)moneyForGoal_0;
        scr.pMov.text_Money.text = "+" + scr.univFunc.Money(moneyForGoal);
    }

    public void Plus_Money()
    {
        PrefsManager.Instance.MoneyCount += moneyForGoal;
        scr.pMov.text_Bank.text = scr.univFunc.Money(PrefsManager.Instance.MoneyCount);
    }
}
