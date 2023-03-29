using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CurrentProfilePanel : MonoBehaviour
{
	public Scripts scr;

	public GameObject enemy;
	public Text profileText, enemyText;
	public Image flagIm, flagEnemyIm;
	public Animator topPanelAnim;
	public Image playerImage, enemyImage;
	[HideInInspector]
	public bool isChange, isChangeEnemy;


	void Awake()
	{
		ChangeCurrentProfile ();
		ChangeEnemyProfile ();
		enemy.SetActive (false);

		scr.alScr.enemySprite = scr.prScrL.itemList[scr.alPrScr.enemyIndexFP].icon;
		scr.alScr.enemyName = scr.prScrL.itemList[scr.alPrScr.enemyIndexFP].name;
		scr.alScr.enemyName0 = scr.prScrL.itemList[scr.alPrScr.enemyIndexFP].name0;
		scr.alScr.enemyFlag = scr.prScrL.itemList[scr.alPrScr.enemyIndexFP].flag;
	}

	void Update()
	{
		if (isChange)
		{
			ChangeCurrentProfile();
			isChange = false;
		}

		if (isChangeEnemy) 
		{
			ChangeEnemyProfile ();
			isChangeEnemy = false;
		}
	}

	private void ChangeEnemyProfile()
	{
		if (scr.gM.MainMenuBools.menuProfile || scr.gM.MainMenuBools.mainMenu) 
		{
			enemyImage.sprite = scr.prScrL.itemList [scr.alPrScr.enemyIndexFP].icon;
			flagEnemyIm.sprite = scr.prScrL.itemList [scr.alPrScr.enemyIndexFP].flag;
			enemyText.text = scr.prScrL.itemList [scr.alPrScr.enemyIndexFP].name0;
		} 
	}


	private void ChangeCurrentProfile() 
	{
		if (scr.gM.MainMenuBools.menuProfile || scr.gM.MainMenuBools.mainMenu) 
		{
			playerImage.sprite = scr.prScrL.itemList [scr.alPrScr.playerIndex].icon;
			flagIm.sprite = scr.prScrL.itemList [scr.alPrScr.playerIndex].flag;
			profileText.text = scr.prScrL.itemList [scr.alPrScr.playerIndex].name0;
		} 
	}

	//If menu is "players", then button opens players profile, if "balls", then balls profile.
	public void GoToProfilePlayers(string menu)
	{
		if (scr.gM.MainMenuBools.menuCareer)
		{
			scr.carScrL.CareerPreviewBack();
			scr.shScrL.anCareer.SetTrigger("back");
			scr.shScrL.anCareer.gameObject.SetActive(false);
		} 
		else if (scr.gM.MainMenuBools.menuFreePlay)
		{
			scr.shScrL.anFP.SetTrigger("fpback");
		} 
		else if (scr.gM.MainMenuBools.mainMenu)
		{
			//topPanelAnim.SetTrigger("call");
			scr.gM.LogoOff();
			scr.objM.mainMenuAnim.SetTrigger("call");
		} 
		else if (scr.gM.MainMenuBools.menuSinglePlayer)
		{
			scr.objM.singlePlayerRect.GetComponent<Animator> ().SetBool("call", false);
			//topPanelAnim.SetTrigger("call");
			scr.gM.LogoOff();
		} 
		else if (scr.gM.MainMenuBools.menuOptions)
		{
			scr.gM.MainMenuBools.menuOptions = false;
			scr.objM.menuOptionsAnim.SetTrigger("back");
			topPanelAnim.SetTrigger("call");
			scr.gM.LogoOff();
		}
			
		scr.objM.menuProfAnim.SetTrigger ("call");

		if (menu == "players")
		{
			scr.shScrL.anPrPlayers.SetBool("call", true);
			scr.gM.MainMenuBools.menuProfilePlayers = true;
		} 
		else if (menu == "balls")
			scr.gM.MainMenuBools.menuProfilePlayers = false;

		scr.gM.MainMenuBools.menuProfile = true;
		scr.gM.MainMenuBools.menuCareer = false;
		scr.gM.MainMenuBools.menuFreePlay = false;
		scr.gM.MainMenuBools.mainMenu = false;
		scr.gM.MainMenuBools.menuSinglePlayer = false;
		scr.gM.MainMenuBools.menuProfileStadiums = false;
	}

	public void GoToProfileBalls()
	{

	}

	public void ButtonDown(RectTransform imTr)
	{
		//imTr.localScale = new Vector3 (1.2f, 1.2f, 1);
	}

	public void ButtonUp(RectTransform imTr)
	{
		//imTr.localScale = new Vector3 (1, 1, 1);
	}
}


