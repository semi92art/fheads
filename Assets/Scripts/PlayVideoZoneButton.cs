using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayVideoZoneButton : MonoBehaviour 
{
	public Scripts scr;

	public int reward;
	public Text countText;
	public Button videoButton;
	public Text numberText;

	public string zoneIdKey = "";
	private string zoneId = "";
	private ADCVideoZone videoZone;

	void Awake()
	{
		countText.text = scr.gM.moneyString (reward);
		numberText.text = scr.alPrScr.videoAdvCount.ToString ();
	}

	void Start() 
	{
		ADCAdManager.AddOnAdAvailabilityChangeMethod(OnAvailabilityChange);
		ADCAdManager.AddOnVideoFinishedMethod (OnVideoFinishedMethod);

		if (scr.alPrScr.videoAdvCount == 0)
		{
			//videoButton.interactable = false;
			//gameObject.SetActive (false);
		}
		else
		{
			if (PlayerPrefs.GetInt("InternetConnection") == 1)
				GetVideoAdByZoneKey ();
		}
	}
		
	void Update() 
	{
		if (awardGet)
		{
			timer += Time.deltaTime;

			if (timer > 0.5f)
			{
				if (!awardGet_1)
				{
					scr.alPrScr.moneyCount += reward;
					scr.alPrScr.setMoney = true;

					awardGet_1 = true;
				} 
				else 
				{
					if (timer > 1.5f) 
					{
						TopPanelBack_1 ();
						timer = 0.0f;
						awardGet = false;
						awardGet_1 = false;
					}
				}
			}
		}

		if (isTopPanelBack_1)
		{
			timer1 += Time.deltaTime;

			if (timer1 > 1.0f)
			{
				scr.objM.topPanelAnim.gameObject.SetActive (false);
				Debug.Log ("Top Panel Set Active False");
				isTopPanelBack_1 = false;
				timer1 = 0.0f;
			}
		}
	}

	public void PerformButtonPressLogic() 
	{
		#if UNITY_EDITOR
		OnVideoFinishedMethod(true);
		#else
		ADCAdManager.ShowVideoAdByZoneKey(zoneIdKey, true, false);
		#endif
	}

	private void OnAvailabilityChange(bool availability, string zone) 
	{
		
	}

	private void TopPanelCall_1()
	{
		scr.objM.topPanelAnim.gameObject.SetActive (true);
		scr.objM.topPanelAnim.SetTrigger ("call1");
	}

	private float timer, timer1;
	public bool awardGet_1;

	public bool awardGet;
	private bool isTopPanelBack_1;

	private void TopPanelBack_1()
	{
		scr.objM.topPanelAnim.SetTrigger ("back1");
		isTopPanelBack_1 = true;
	}

	private void OnVideoFinishedMethod(bool ad_shown) 
	{
		if (ad_shown) 
		{
			//scr.alPrScr.videoAdvCount --;
			Debug.Log ("ad_shown" + ad_shown);
			awardGet = true;
			TopPanelCall_1 ();
		}

		numberText.text = scr.alPrScr.videoAdvCount.ToString ();

		//if (scr.alPrScr.videoAdvCount == 0)
			//videoButton.interactable = false;
	}

	private void GetVideoAdByZoneKey()
	{
		videoZone = ADCAdManager.GetVideoZoneObjectByKey(zoneIdKey);
		zoneId = ADCAdManager.GetZoneIdByKey(zoneIdKey);
	}

	private void ShowVideoAdByZoneKey()
	{
		if(videoZone.zoneType == ADCVideoZoneType.Interstitial && AdColony.IsVideoAvailable(zoneId)) 
			AdColony.ShowVideoAd(zoneId);
		else 
			Debug.Log("AdColony ---- The zone '" + zoneId + "' was requested to play, but it is NOT ready to play yet.");
	}
}

