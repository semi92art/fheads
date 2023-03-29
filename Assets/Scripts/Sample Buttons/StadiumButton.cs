using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StadiumButton : MonoBehaviour {
	public Button button;
	public Image icon;
	public GameObject lockIcon;
	public Text moneyCoast;
	public Text galochka;
	public Image showcase;
	
	void Start(){
		transform.localScale = new Vector3 (1, 1, 1);
	}
}
