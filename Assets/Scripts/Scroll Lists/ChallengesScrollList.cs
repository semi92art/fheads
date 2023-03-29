using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ChallengesItem {
	public bool isLocked, isGalochka;
	public Button.ButtonClickedEvent thingToDo;
	public int hardness;
}

public class ChallengesScrollList : MonoBehaviour {

	public Scripts scr;

	public Color myGreen, myYellow, myRed, myBlack;
	public Text challenges;
	public GameObject sampleButton;
	public Transform contentPanel;
	public List<ChallengesItem>itemList;
	[HideInInspector]
	public GameObject[] challButtons = new GameObject[25];
	private int numOfChall = -1;
	private int timer;

	void Awake(){
		DestroyEditorButtons();
	}

	void Start () {
		PopulateList ();
		challButtons = GameObject.FindGameObjectsWithTag("ChallButton");
	}

	void Update(){
		timer++;
		if (timer==2){
			scr.chalMan.ChallMedium();
		} else if (timer==3){
			scr.chalMan.ChallHard();
		} else if (timer==4){
			scr.chalMan.ChallEasy();
		}
	}

	void DestroyEditorButtons(){
		GameObject[] editorButtons = GameObject.FindGameObjectsWithTag("ChallButton");
		foreach (var item in editorButtons){
			DestroyImmediate(item);
		}
	}

	void PopulateList () {
		int num = 0;
		foreach (var item in itemList) {
			numOfChall++;
			GameObject newButton = Instantiate(sampleButton) as GameObject;
			ChallengesSampleButton button = newButton.GetComponent<ChallengesSampleButton>();
			num = numOfChall + 1;
			button.nameLabel.text = "" + num;
			int i = scr.chalMan.challNum[numOfChall] - 1;
			button.challText.text = scr.chalMan.prStr[i];
			button.lock1.SetActive(item.isLocked);
			if (!item.isGalochka){
				button.galochka.GetComponent<Text>().color = myYellow;
				button.galochka.GetComponent<Text>().text = "opened";
			}
			if (!item.isLocked){
				button.button.onClick = item.thingToDo;
			}
			if (item.hardness==0){
				button.hardness.text = "easy";
				button.hardness.color = myGreen;
			} else if (item.hardness==1){
				button.hardness.text = "medium";
				button.hardness.color = myYellow;
			} else if (item.hardness==2){
				button.hardness.text = "hard";
				button.hardness.color = myRed;
			} else if (item.hardness==3){
				button.hardness.text = "extrahard";
				button.hardness.color = myBlack;
			}
			newButton.transform.SetParent (contentPanel);
		}
	}
}
