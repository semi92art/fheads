using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CurrentProfilePanel : MonoBehaviour
{
    public Scripts scr;

	public Image legIm;
	public Image playerImage;
    // Skills:
    public Image im_Speed;
    public Image im_Kick;
    public Image im_Jump;

	[HideInInspector]
	public bool isChange;

    private int 
        ind,
        ind1,
        cntrInd;

	void Update()
	{
		if (isChange)
		{
			ChangeCurrentProfile();
			isChange = false;
		}
	}

	private void ChangeCurrentProfile() 
	{
        ind = scr.alPrScr.playerIndex;

        if (scr.prMng.previewPlayerLg == 1)
        {
            cntrInd = scr.prMng.itemList[ind].cntrInd;
            playerImage.sprite = scr.prMng.itemList[ind].icon;
            ind1 = 50 - scr.alPrScr.buttonIndex;
        }
        else if (scr.prMng.previewPlayerLg == 2)
        {
            cntrInd = scr.prMng.itemList_2[ind].cntrInd;
            playerImage.sprite = scr.prMng.itemList_2[ind].icon;
            ind1 = 40 - scr.alPrScr.buttonIndex;
        }

        legIm.sprite = scr.cntrL.Countries[cntrInd].boot;
        SetSkills();
	}

    private void SetSkills()
    {
        float new_h_val;
        float skill_Speed = 0f;
        float skill_Kick = 0f;
        float skill_Jump = 0f;

        if (scr.prMng.previewPlayerLg == 1)
        {
            skill_Speed = scr.prMng.itemList[ind].skill_Speed;
            skill_Kick = scr.prMng.itemList[ind].skill_Kick;
            skill_Jump = scr.prMng.itemList[ind].skill_Jump;
        }
        else if (scr.prMng.previewPlayerLg == 2)
        {
            skill_Speed = scr.prMng.itemList_2[ind].skill_Speed;
            skill_Kick = scr.prMng.itemList_2[ind].skill_Kick;
            skill_Jump = scr.prMng.itemList_2[ind].skill_Jump;
        }

        im_Speed.rectTransform.sizeDelta = new Vector2(
                im_Speed.rectTransform.rect.width,
                150f * skill_Speed / 100f);
        im_Kick.rectTransform.sizeDelta = new Vector2(
                im_Kick.rectTransform.rect.width,
                150f * skill_Kick / 100f);
        im_Jump.rectTransform.sizeDelta = new Vector2(
                im_Jump.rectTransform.rect.width,
                150f * skill_Jump / 100f);

        /*new_h_val = 2 * (120f / 400f) * (skill_Speed / 100f - 0.5f);
        im_Speed.color = Color.HSVToRGB(
            new_h_val,
            scr.univFunc.HSV_from_RGB(im_Speed.color).y,
            scr.univFunc.HSV_from_RGB(im_Speed.color).z);

        new_h_val = 2 * (120f / 400f) * (skill_Kick / 100f - 0.5f);
        im_Kick.color = Color.HSVToRGB(
            new_h_val,
            scr.univFunc.HSV_from_RGB(im_Kick.color).y,
            scr.univFunc.HSV_from_RGB(im_Kick.color).z);

        new_h_val = 2 * (120f / 400f) * (skill_Jump / 100f - 0.5f);
        im_Jump.color = Color.HSVToRGB(
            new_h_val,
            scr.univFunc.HSV_from_RGB(im_Jump.color).y,
            scr.univFunc.HSV_from_RGB(im_Jump.color).z);*/
    }
}


