using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSampleButton : MonoBehaviour 
{
	public Image playerIm;
	public Text playerName;

	void Start()
	{
		transform.localScale = new Vector3 (1, 1, 1);
	}
}
