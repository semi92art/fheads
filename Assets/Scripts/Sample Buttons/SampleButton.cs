using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour {
	public GameObject noAds;
	public Button button;
	public Text nameLabel;
	public Image icon;
	public Text price;

	void Start(){
		transform.localScale = new Vector3 (1, 1, 1);
	}
}
