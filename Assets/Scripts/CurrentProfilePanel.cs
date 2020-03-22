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
        ind = PrefsManager.Instance.PlayerIndex;
        
        cntrInd = ProfileManager.Instance.itemList[ind].cntrInd;
        playerImage.sprite = ProfileManager.Instance.itemList[ind].icon;
        
        legIm.sprite = scr.cntrL.Countries[cntrInd].boot;
        SetSkills();
	}

    private void SetSkills()
    {
        float new_h_val;
        float skill_Speed, skill_Kick, skill_Jump;

        skill_Speed = ProfileManager.Instance.itemList[ind].skill_Speed;
        skill_Kick = ProfileManager.Instance.itemList[ind].skill_Kick;
        skill_Jump = ProfileManager.Instance.itemList[ind].skill_Jump;
        
        im_Speed.rectTransform.sizeDelta = new Vector2(
                im_Speed.rectTransform.rect.width,
                150f * skill_Speed / 100f);
        im_Kick.rectTransform.sizeDelta = new Vector2(
                im_Kick.rectTransform.rect.width,
                150f * skill_Kick / 100f);
        im_Jump.rectTransform.sizeDelta = new Vector2(
                im_Jump.rectTransform.rect.width,
                150f * skill_Jump / 100f);
    }
}


