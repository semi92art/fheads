using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReviewScript : MonoBehaviour
{
	private Scripts scr;
	public Button _button;
	private Animator _animator;

	void Awake()
	{
		scr = FindObjectOfType<Scripts>();
		_animator = _button.GetComponent<Animator>();

		if (scr.alPrScr.launches == 1)
			PlayerPrefs.SetInt("ReviewShown", 0);

		_button.interactable = false;
	}

	public void ShowReviewButton()
	{
		if (scr.alPrScr.launches % 5 == 0 && PlayerPrefs.GetInt("ReviewShown") == 0)
		{
			_animator.SetTrigger("call");
			_button.interactable = true;
		}
	}

	public void GoToStore()
	{
		Application.OpenURL("market://details?id=com.Artem.FootballHeads");
		PlayerPrefs.SetInt("ReviewShown", 1);
		_animator.SetTrigger("back");
	}
}
