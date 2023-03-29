using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelStadiumSampleButton : MonoBehaviour {
	public Image stadiumIcon;
	public Button button;
	[HideInInspector]
	public int stadNumber;

	void Start(){
		gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
	}
}

