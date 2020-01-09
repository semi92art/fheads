using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundVolumeScript : MonoBehaviour {
	public Slider soundSlider;
	public Image soundImage;
	public Sprite sprite1, sprite2;

	void Update(){
		if (soundSlider.value.Equals(0)){
			soundImage.sprite = sprite2;
		} else {
			soundImage.sprite = sprite1;
		}
	}
}
