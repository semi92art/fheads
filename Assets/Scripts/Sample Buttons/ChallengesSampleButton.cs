using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChallengesSampleButton : MonoBehaviour {
	public Button button;
	public Text nameLabel, challText, hardness;
	public GameObject galochka, lock1;
	public Animator anim;

	void Start(){
		transform.localScale = new Vector3 (1, 1, 1);
	}
}
