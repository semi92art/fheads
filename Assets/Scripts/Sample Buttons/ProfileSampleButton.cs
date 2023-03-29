using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProfileSampleButton : MonoBehaviour {
	public Button button;
	public Text nameLabel;
	public Image icon;
	public GameObject lockIcon;
	public Text moneyCoast;
	public Image flagShowcase;
	public Image flag;
	public Image showcase;

	void Start(){
		transform.localScale = new Vector3 (1, 1, 1);
	}
}
