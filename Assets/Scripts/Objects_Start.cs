using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Objects_Start : MonoBehaviour
{
	public AllPrefsScript alPrScr;
	public GameObject reporter;
	public bool destroyReporterOnStart;
	public Text loadingText;
	public Button startButton;
	public bool isLoaded;
	private int state;
	private bool isRusLang;
	public bool isTextTransparent;
	private bool isTextTransparent1 = true;
	private int loadingTimer;


	void Awake ()
	{
		Application.targetFrameRate = 70;
		startButton.interactable = false;

		if (Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian)
			isRusLang = true;

		if (destroyReporterOnStart)
			DestroyImmediate(reporter);

		if (isRusLang)
			loadedStr = "начать игру";
		else
			loadedStr = "start game";
	}

	void Update()
	{
		if (!isLoaded)
		{
			if (Time.frameCount % 30 == 0)
			{
				state ++;

				if (state == 4)
					state = 0;

				switch (state)
				{
				case 0:
					if (isRusLang)
						loadingText.text = "Загрузка";
					else
						loadingText.text = "Loading";
					break;
				case 1:
					if (isRusLang)
						loadingText.text = "Загрузка.";
					else
						loadingText.text = "Loading.";
					break;
				case 2:
					if (isRusLang)
						loadingText.text = "Загрузка..";
					else
						loadingText.text = "Loading..";
					break;
				case 3:
					if (isRusLang)
						loadingText.text = "Загрузка...";
					else
						loadingText.text = "Loading...";

					loadingTimer ++;
					break;
				}

				if (loadingTimer == 1)
					isLoaded = true;
			}
		}
		else
		{
			startButton.interactable = true;

			if (Input.GetKeyDown(KeyCode.Escape) || loadingTimer == 2)
				startButton.onClick.Invoke();

			if (!isLoaded1)
			{
				if (!isTextTransparent)
				{
					if (loadingText.color.a > 0)
					{
						loadingText.color = new Vector4(
							loadingText.color.r,
							loadingText.color.g,
							loadingText.color.b,
							loadingText.color.a - 0.02f);
					}
					else
					{
						isTextTransparent = true;
						loadingText.text = loadedStr;
					}	
				}
				else
				{
					if (isTextTransparent1)
					{
						if (loadingText.color.a < 1)
						{
							loadingText.color = new Vector4(
								loadingText.color.r,
								loadingText.color.g,
								loadingText.color.b,
								loadingText.color.a + 0.02f);
						}
						else
							isTextTransparent1 = false;
					}
					else
					{
						if (loadingText.color.a > 0)
						{
							loadingText.color = new Vector4(
								loadingText.color.r,
								loadingText.color.g,
								loadingText.color.b,
								loadingText.color.a - 0.02f);
						}
						else
						{
							loadingTimer ++;
							isTextTransparent1 = true;
						}
							
					}
				}
			}
			else
			{
				
			}


		}
	}

	private bool isLoaded1;
	private string loadedStr;

	public void Load_MainMenu ()
	{
		alPrScr.launches ++;
		alPrScr.isChange0 = true;
		Debug.Log("Load Scene Main Menu");
		SceneManager.LoadScene("MainMenu");
	}
}
