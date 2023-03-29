using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CareerSampleButton : MonoBehaviour
{
	public Text nameLabel;
	public Text awardText, awardText1;
	public Text medalText, medalText1;

	public Image icon, lockedIcon;
	public Image showcase;

	public GameObject medal;
	public Button button;
	public Animator buttonAnim;

	void Start()
	{
		transform.localScale = new Vector3 (1, 1, 1);
	}
}
