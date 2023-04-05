using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointerScript : MonoBehaviour {
	public ScrollRect scrRect;
	public RectTransform contentPanel, scrRectTr;
	public Image[] pointImage;
	public Sprite pointSprite, defaultSprite;
	private int width;
	private int normPosX;

	void Awake(){
		width = (int)scrRectTr.rect.width - 20;
	}

	void Update(){
		CheckPoints ();
	}

	public void CheckPoints(){
		for (int i = 0; i < pointImage.Length; i++) {
			normPosX = (int)(-contentPanel.anchoredPosition.x / width - 0.5f);
			if (normPosX < 0){
				normPosX = 0;
			} else if (normPosX > pointImage.Length - 1){
				normPosX = pointImage.Length - 1;
			}

			if (normPosX.Equals(i)){
				pointImage[i].sprite = pointSprite;
			} else {
				pointImage[i].sprite = defaultSprite;
			}
		}
	}

	public void SetPoint(int point){
		float posY = contentPanel.anchoredPosition.y;
		contentPanel.anchoredPosition = new Vector3 (-width * point, posY, 0);
		scrRect.velocity = new Vector2 (0, 0);
	}
}
