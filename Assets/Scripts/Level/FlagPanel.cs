using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlagPanel : MonoBehaviour
{
	public Scripts scr;

	public Image playerFlag, enemyFlag;
	public Text playerName, enemyName;

	void Start()
	{
		playerName.text = scr.alScr.playerName0;
		enemyName.text = scr.alScr.enemyName0;
	}
}
