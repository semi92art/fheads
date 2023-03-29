// -----------------------------------------------
// Documentation: http://www.reign-studios.net/docs/unity-plugin/
// -----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Reign;
using UnityEngine.SceneManagement;

public class EmailDemo : MonoBehaviour
{
	GUIStyle uiStyle;
	public Button EmailButton, BackButton;

	void Start ()
	{
		// bind button events
		EmailButton.Select();
		EmailButton.onClick.AddListener(emailClicked);
		BackButton.onClick.AddListener(backClicked);
	}

	private void emailClicked()
	{
		EmailManager.Send("support@reign-studios.com", "Subject", "Some body content...");
	}

	private void backClicked()
	{
		SceneManager.LoadScene("MainDemo");
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
	}
}
