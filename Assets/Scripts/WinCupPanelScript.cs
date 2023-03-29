using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinCupPanelScript : MonoBehaviour
{
	public Scripts scr;

	public Animator passedTextAnim;
	public Image cupWinImage;
	public Text moneyWinText;
	public Text moneyBankText;
	private int[] cupPrices = new int[12];
	private int[] cupPricesRetro = new int[8];
	public Sprite[] cupSprites;

	private int totalPrice;
	private float totalPrice0;
	private float timer, timer1, timer2;
	private bool isWinCup;
	private AudioSource moneyWinSource1;
	private AudioSource moneyWinSource2;
	private int bankMoney;
	private string totalPriceStr;
	private string bankMoneyStr;

	private int plfGames;
	private int opndTrns;
	private int trn1;

	void Awake()
	{
		for (int i = 0; i < cupPrices.Length; i++)
			cupPrices [i] = scr.alScr.cupPrices [i];

		for (int i = 0; i < cupPricesRetro.Length; i++)
			cupPricesRetro [i] = scr.alScr.cupPricesRetro [i];

		cupWinImage.sprite = cupSprites[scr.alPrScr.trn - 1];
		totalPrice = cupPrices[scr.alPrScr.trn - 1];

		bankMoney = scr.alPrScr.moneyCount;
		moneyBankText.text = scr.gM.moneyString(bankMoney);
		moneyWinText.text = scr.gM.moneyString(totalPrice);

		if (SceneManager.GetActiveScene().name == "Playoff")
		{
			moneyWinSource1 = scr.objPlf.moneyWin1Source;
			moneyWinSource2 = scr.objPlf.moneyWin2Source;
		} 
		else if (SceneManager.GetActiveScene().name == "Championship")
		{
			moneyWinSource1 = scr.objCh.moneyWin1Source;
			moneyWinSource2 = scr.objCh.moneyWin2Source;
		}
	}
		
	void Update()
	{
		timer2++;

		if (timer2 == 1)
		{
			plfGames = scr.alPrScr.plfGames;
			opndTrns = scr.alPrScr.opndTrns;
			trn1 = scr.alPrScr.trn1;

			if (SceneManager.GetActiveScene().name=="Playoff")
			{
				if (plfGames == 3 && scr.alPrScr.isW == 1 && trn1 > opndTrns)
				{
					gameObject.GetComponent<Animator>().SetTrigger("call");
					passedTextAnim.SetTrigger("call");
					isWinCup = true;
				}
			} 
			else if (SceneManager.GetActiveScene().name == "Championship")
			{
				if (scr.chLIm.endOfChamp &&
					scr.chL.itemList[0].imageSprite == scr.alScr.playerSprite &&
					trn1 > opndTrns)
				{
					gameObject.GetComponent<Animator>().SetTrigger("call");
					passedTextAnim.SetTrigger("call");
					isWinCup = true;
				}
			}
		}

		if (isWinCup)
		{
			timer += Time.deltaTime;

			if (timer == Time.deltaTime)
			{
				scr.alPrScr.moneyCount += totalPrice;

				if (scr.alPrScr.moneyCount > 10000000)
					scr.alPrScr.moneyCount = 10000000;
				
				scr.alPrScr.setMoney = true;
			} 
			else if (timer >= 1)
			{
				if (totalPrice > 0)
				{
					if (timer >= 1 && timer < 1 + Time.deltaTime)
					{
						moneyWinSource1.Play();
						moneyWinSource2.PlayDelayed(0.2f);
					}

					if (totalPrice > 1000)
					{
						totalPrice -= 1000;
						bankMoney += 1000;

						if (bankMoney > 10000000)
							bankMoney = 10000000;
					} 
					else if (totalPrice <= 1000 && totalPrice > 0)
					{
						totalPrice -= 50;
						bankMoney += 50;

						if (bankMoney > 10000000)
							bankMoney = 10000000;
					}

					moneyWinText.text = scr.gM.moneyString (totalPrice);
					moneyBankText.text = scr.gM.moneyString (bankMoney);
				} 
				else
				{
					if (moneyWinSource1.isPlaying)
					{
						moneyWinSource1.Stop();
						moneyWinSource2.Stop();
					}

					timer1 += Time.deltaTime;

					if (timer1 < 1)
					{
						moneyWinText.gameObject.SetActive(false);
					} 
					else 
					{
						gameObject.GetComponent<Animator>().SetTrigger("back");
						gameObject.SetActive(false);
					}
				}
			}
		}
	}
}
