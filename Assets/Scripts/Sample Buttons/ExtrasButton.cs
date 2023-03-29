using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExtrasButton : MonoBehaviour {
	public Button button;
	public Image icon;
	public GameObject lockIcon;
	public Text moneyCoast;
	public Image showcase;

	void Start(){
		transform.localScale = new Vector3 (1, 1, 1);
	}
}
