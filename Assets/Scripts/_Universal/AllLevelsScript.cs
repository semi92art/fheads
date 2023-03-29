using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Net;
using System.IO;

public class AllLevelsScript : MonoBehaviour
{
	[HideInInspector]
	public Sprite secondWinnerSprite;
	[HideInInspector]
	public string secondWinnerName;
	[HideInInspector]
	public string secondWinnerName0;
	[HideInInspector]
	public Sprite secondWinnerFlag;
	[HideInInspector]
	public Text roundTable1, roundTable2;

	[Header("Player:")]
	//[HideInInspector]
	public Sprite playerSprite;
	//[HideInInspector]
	public string playerName;
	//[HideInInspector]
	public string playerName0;
	//[HideInInspector]
	public Sprite playerFlag;
	[Header("Enemy:")]
	//[HideInInspector]
	public Sprite enemySprite;
	//[HideInInspector]
	public string enemyName;
	//[HideInInspector]
	public string enemyName0;
	//[HideInInspector]
	public Sprite enemyFlag;
	
	public int[] reservePlayers;
	public int[] reservePlayersRetro;
	
	[System.Serializable]
	public class CupVariants
	{
		public int index;
	}

	public List<CupVariants>varList;
	public List<CupVariants>varListRetro;

	public int[] cupPrices;
	public int[] cupPricesRetro;

	void Awake()
	{
		#if UNITY_EDITOR
		if (SceneManager.GetActiveScene().name == "MainMenu" ||
		    SceneManager.GetActiveScene().name == "Championship")
		{
			
		}
			// Profiler.maxNumberOfSamplesPerFrame = -1;
		#endif

		DontDestroyOnLoad (gameObject);
	}

	public string GetHtmlFromUri(string resource)
	{
		string html = string.Empty;
		HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
		try
		{
			using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
			{
				bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
				if (isSuccess)
				{
					using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
					{
						//We are limiting the array to 80 so we don't have
						//to parse the entire html document feel free to 
						//adjust (probably stay under 300)
						char[] cs = new char[80];
						reader.Read(cs, 0, cs.Length);
						foreach(char ch in cs)
						{
							html +=ch;
						}
					}
				}
			}
		}
		catch
		{
			return "";
		}
		return html;
	}
}

