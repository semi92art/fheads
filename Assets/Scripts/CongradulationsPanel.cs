using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CongradulationsPanel : MonoBehaviour 
{
    [SerializeField]
	private Scripts scr;
    [Space(5)]
    public Animator anim_CongrPan;
    public GameObject obj_Conffeti;
	public GameObject congrPanel;
	public Rigidbody2D ballRb;
	public Text scoreText;
	public GameObject[] objsToDis;

   
	public void CongradulationsPanelCall()
	{
        if (TimeManager.resOfGame == 1)
        {
            scr.alPrScr.winsTotal++;

            if (Score.score1 == 0)
                scr.alPrScr.winsNoConcGoals++;
        }
            
		scoreText.text = Score.score1 + ":" + Score.score;
		congrPanel.SetActive(true);
        Enemy.gameStop = true;
        GameManager.Instance.MenuResultBack ();

        scr.camSize.SetCameraPositionForCongrPan();
        scr.alPrScr.doCh = true;
	}

	public void DisableSomeObjects()
	{
		for (int i = 0; i < objsToDis.Length; i++)
			objsToDis[i].SetActive(false);

		scr.camSize.enabled = false;
	}
}
