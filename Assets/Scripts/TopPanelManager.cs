using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TopPanelManager : MonoBehaviour 
{
	public Scripts scr;

	public Color retroColor;
	public Text moneyText;
	public Animator challImAnim;
	public int maxMoneyCount;
	public Image[] grayCareerImages;
	public Image[] colorCareerImages;
	public Image[] grayCareerImagesRetro;
	public Image[] colorCareerImagesRetro;

	void Start()
	{
		for (int i = 0; i < grayCareerImages.Length; i++)
		{
			if (i < scr.alPrScr.opndTrns)
			{
				grayCareerImages [i].enabled = false;
				colorCareerImages [i].enabled = true;
			} 
			else 
			{
				grayCareerImages[i].enabled = true;
				colorCareerImages [i].enabled = false;
			}
		}

		for (int i = 0; i < grayCareerImagesRetro.Length; i++)
		{
			grayCareerImagesRetro [i].enabled = false;
			colorCareerImagesRetro [i].enabled = true;
			colorCareerImagesRetro[i].color = retroColor;
		}
	}

	private string strNum2, strNum3;

	void Update()
	{
		if (scr.alPrScr.moneyCount > maxMoneyCount)
			scr.alPrScr.moneyCount = maxMoneyCount;
		
		moneyText.text = scr.gM.moneyString (scr.alPrScr.moneyCount);
	}
}
