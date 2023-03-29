using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CongradulationsPanel : MonoBehaviour 
{
	public Scripts scr;

	public GameObject congrPanel;
	public Rigidbody2D ballRb;
	public Animator nextButton;
	public Image playerImage, enemyImage;
	public Image playerFlag, enemyFlag;
	public Text playerName, enemyName;
	public Text scoreText;
	public Canvas mainCanvas;
	
	private int timer;

	[HideInInspector]
	public bool isLoadPanel;

	void Awake()
	{
		congrPanel.SetActive(false);
	}

	void Start()
	{
		playerImage.sprite = scr.alScr.playerSprite;
		enemyImage.sprite = scr.alScr.enemySprite;
		playerFlag.sprite = scr.alScr.playerFlag;
		enemyFlag.sprite = scr.alScr.enemyFlag;
		playerName.text = scr.alScr.playerName0;
		enemyName.text = scr.alScr.enemyName0;
	}
		
	void Update()
	{
		/*if (scr.gM.exitBool)
		{
			scr.gM.GoToMenu();
			scoreText.text = Score.score1 + ":" + Score.score;
			scr.gM.exitBool = false;
		}*/
	}



	public void CongradulationsPanelCall()
	{
		scoreText.text = Score.score1 + ":" + Score.score;
		congrPanel.SetActive(true);
		ballRb.constraints = RigidbodyConstraints2D.FreezeAll;
		scr.enAlg.gameStop = true;
		scr.gM.MenuResultBack ();
	}

	public void CallLoadingPanel() 
	{
		mainCanvas.enabled = false;
	}
}
