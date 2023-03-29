using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ShopItem 
{
	public bool isNoAds;
	public string name;
	public Sprite icon;
	public string price;
	public Button.ButtonClickedEvent thingToDo;
}

public class ShopScrollList : MonoBehaviour 
{
	public Scripts scr;

	public Sprite showcaseUp, showcaseDown;
	public Sprite showcase1Up, showcase1Down;
	public Animator
		anCareer, 
		anFP, 
		anProfile, 
		anPrPlayers, 
		anPrStadiums;
	public GameObject sampleButton;
	public List<ShopItem>itemList;

	public void CallMenuMoney()
	{
		if (scr.gM.MainMenuBools.menuCareer)
		{
			scr.carScrL.CareerPreviewBack();
			anCareer.SetTrigger("call1");
		} 
		else if (scr.gM.MainMenuBools.menuFreePlay)
		{
			scr.objM.currProfileAnim.SetTrigger("back");
			scr.dblScr.SetPlayerNotFP();
			anFP.SetTrigger("fpback");
		}
		else if (scr.gM.MainMenuBools.menuProfile)
		{
			anProfile.SetTrigger("back");
			if (scr.gM.MainMenuBools.menuProfilePlayers) 		
				anPrPlayers.SetBool("call", false);
			else if (scr.gM.MainMenuBools.menuProfileStadiums) 	
				anPrStadiums.SetBool("call", false);
		}
			
		scr.gM.MainMenuBools.menuCareer = false;
		scr.gM.MainMenuBools.menuFreePlay = false;
		scr.gM.MainMenuBools.menuProfile = false;
		scr.gM.MainMenuBools.menuProfilePlayers = false;
		scr.gM.MainMenuBools.menuProfileStadiums = false;
	}

	public void ShowcaseDown(Image showcase)
	{
		showcase.sprite = showcaseDown;
	}
	
	public void ShowcaseUp(Image showcase)
	{
		showcase.sprite = showcaseUp;
	}

	public void Showcase1Down(Image showcase)
	{
		showcase.sprite = showcase1Down;
	}

	public void Showcase1Up(Image showcase)
	{
		showcase.sprite = showcase1Up;
	}
}
