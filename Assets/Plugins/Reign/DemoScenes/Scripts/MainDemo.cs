using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainDemo : MonoBehaviour
{
	public Button ResetPrefsButton;
	public Button MessageBoxesButton, EmailButton, StreamsButton, MarketingButton, AdsButton, InterstitialAdsButton, IAPButton, ScoresButton, InputExButton, SocialButton;

	void Start ()
	{
		// helpful message
		const string warning = "NOTE: Make sure to add all the Reign Demo projects you wish to test in the 'Build Settings' window!\nThis makes for easy device testing.";
		Debug.Log(warning);
		Debug.LogWarning(warning);

		// bind button events
		MessageBoxesButton.Select();
		ResetPrefsButton.onClick.AddListener(resetPrefsClicked);
		MessageBoxesButton.onClick.AddListener(messageBoxesClicked);
		EmailButton.onClick.AddListener(emailClicked);
		StreamsButton.onClick.AddListener(streamsClicked);
		MarketingButton.onClick.AddListener(marketingClicked);
		AdsButton.onClick.AddListener(adsClicked);
		InterstitialAdsButton.onClick.AddListener(interstitialAdsClicked);
		IAPButton.onClick.AddListener(iapClicked);
		ScoresButton.onClick.AddListener(scoresClicked);
		InputExButton.onClick.AddListener(inputExClicked);
		SocialButton.onClick.AddListener(socialClicked);

		// helpful utility to get screen size changed events!
		ReignServices.ScreenSizeChangedCallback += ReignServices_ScreenSizeChangedCallback;
	}

	private void resetPrefsClicked()
	{
		PlayerPrefs.DeleteAll();
		Debug.Log("Player Prefs Reset!");
	}

	void ReignServices_ScreenSizeChangedCallback(int oldWidth, int oldHeight, int newWidth, int newHeight)
	{
		Debug.Log(string.Format("Screen Size Changed: OldSize = {0}, {1} NewSize = {2}, {3}", oldWidth, oldHeight, newWidth, newHeight));
	}

	private void messageBoxesClicked()
	{
		SceneManager.LoadScene("MessageBoxDemo");
	}

	private void emailClicked()
	{
		SceneManager.LoadScene("EmailDemo");
	}

	private void streamsClicked()
	{
		SceneManager.LoadScene("StreamsDemo");
	}

	private void marketingClicked()
	{
		SceneManager.LoadScene("MarketingDemo");
	}

	private void adsClicked()
	{
		SceneManager.LoadScene("AdsDemo");
	}

	private void interstitialAdsClicked()
	{
		SceneManager.LoadScene("InterstitialAdDemo");
	}

	private void iapClicked()
	{
		SceneManager.LoadScene("IAPDemo");
	}

	private void scoresClicked()
	{
		SceneManager.LoadScene("ScoresDemo");
	}

	private void inputExClicked()
	{
		SceneManager.LoadScene("InputExDemo");
	}

	private void socialClicked()
	{
		SceneManager.LoadScene("SocialDemo");
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
	}
}
