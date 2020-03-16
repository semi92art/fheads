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
        float coeff_0 = Mathf.Pow((float)(scr.alPrScr.lg + 1), 2f);
        float coeff_1 = Mathf.Pow((float)(scr.alPrScr.game + 1), 1.5f);
        float coeff_2 = (float)Score.score * 50f + 300f;
        float upgrCoeff = (100f + (float)scr.alPrScr.upgrMoney * 5f)/100f;
        float moneyForGoal_0 = coeff_0 * coeff_1 * coeff_2 * upgrCoeff;
        moneyForGoal = (int)moneyForGoal_0;
        scr.pMov.text_Money.text = "+" + scr.univFunc.moneyString(moneyForGoal);
    }

    public void Plus_Money()
    {
        
        scr.alPrScr.moneyCount += moneyForGoal;
        scr.alPrScr.setMoney = true;
        scr.pMov.text_Bank.text = scr.univFunc.moneyString(scr.alPrScr.moneyCount);
    }
}
