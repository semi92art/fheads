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
		
		scoreText.text = $"{Score.score1.ToString()}:{Score.score.ToString()}";
		congrPanel.SetActive(true);
        Enemy.gameStop = true;
        GameManager.Instance.MenuResultBack ();

        scr.camSize.SetCameraPositionForCongrPan();
	}

	public void DisableSomeObjects()
	{
		for (int i = 0; i < objsToDis.Length; i++)
			objsToDis[i].SetActive(false);

		scr.camSize.enabled = false;
	}
}
