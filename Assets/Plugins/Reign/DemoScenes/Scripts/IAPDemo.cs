// -----------------------------------------------
// Documentation: http://www.reign-studios.net/docs/unity-plugin/
// -----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Reign;
using UnityEngine.SceneManagement;

public class IAPDemo : MonoBehaviour{
	public Text rebootText;

	[Header("Is testing mode on:")]
	public bool testing;

	public int moneyPack1, moneyPack2, moneyPack3;
	private static bool created;
	public int moneyCount;

	#if SAMSUNG
	private string item1 = "xxxxxxxxxxx1";
	private string item2 = "xxxxxxxxxxx2";
	private string item3 = "xxxxxxxxxxx3";
	#elif WINDOWS_PHONE	
	private string item1 = "money_pack_1_without_closing_advertising";
	private string item2 = "money_pack_2";
	private string item3 = "money_pack_3";
	#else
	private string item1 = "money_pack_1";
	private string item2 = "money_pack_2";
	private string item3 = "money_pack_3";
	//private string item1 = "com.reignstudios.test_app1";
	//private string item2 = "com.reignstudios.test_app2";
	//private string item3 = "com.reignstudios.test_app3";
	#endif

	public Text StatusText;
	//public Button BuyDurableButton, BuyConsumableButton, RestoreButton, GetPriceInfoButton, BackButton;


	void Start(){
		// make sure we don't init the same IAP items twice
		if (created) return;
		created = true;

		// InApp-Purchases - NOTE: you can set different "In App IDs" for each platform.
		var inAppIDs = new InAppPurchaseID[3];
		inAppIDs[0] = new InAppPurchaseID(item1, 0m, "$", InAppPurchaseTypes.Consumable);
		inAppIDs[1] = new InAppPurchaseID(item2, 1.29m, "$", InAppPurchaseTypes.Consumable);
		inAppIDs[2] = new InAppPurchaseID(item3, 4.99m, "$", InAppPurchaseTypes.Consumable);
		
		// create desc object
		var desc = new InAppPurchaseDesc();

		// Global
		desc.Testing = testing;
		desc.ClearNativeCache = false;
			
		// Editor
		desc.Editor_InAppIDs = inAppIDs;
			
		// WinRT
		desc.WinRT_InAppPurchaseAPI = InAppPurchaseAPIs.MicrosoftStore;
		desc.WinRT_MicrosoftStore_InAppIDs = inAppIDs;
			
		// WP8
		desc.WP8_InAppPurchaseAPI = InAppPurchaseAPIs.MicrosoftStore;
		desc.WP8_MicrosoftStore_InAppIDs = inAppIDs;
			
		// BB10
		desc.BB10_InAppPurchaseAPI = InAppPurchaseAPIs.BlackBerryWorld;
		desc.BB10_BlackBerryWorld_InAppIDs = inAppIDs;
			
		// iOS
		desc.iOS_InAppPurchaseAPI = InAppPurchaseAPIs.AppleStore;
		desc.iOS_AppleStore_InAppIDs = inAppIDs;
		desc.iOS_AppleStore_SharedSecretKey = "";// NOTE: Must set SharedSecretKey, even for Testing!
			
		// Android
		// Choose for either GooglePlay or Amazon.
		// NOTE: Use "player settings" to define compiler directives.
		#if AMAZON
		desc.Android_InAppPurchaseAPI = InAppPurchaseAPIs.Amazon;
		#elif SAMSUNG
		desc.Android_InAppPurchaseAPI = InAppPurchaseAPIs.Samsung;
		#else
		desc.Android_InAppPurchaseAPI = InAppPurchaseAPIs.GooglePlay;
		#endif

		desc.Android_GooglePlay_InAppIDs = inAppIDs;
		desc.Android_GooglePlay_Base64Key = "";// NOTE: Must set Base64Key for GooglePlay in Apps to work, even for Testing!
		desc.Android_Amazon_InAppIDs = inAppIDs;
		desc.Android_Samsung_InAppIDs = inAppIDs;
		desc.Android_Samsung_ItemGroupID = "";

		// init
		InAppPurchaseManager.Init(desc, createdCallback);
	}


	//***************************************************************

	public void BuyItem_1(){
		InAppPurchaseManager.MainInAppAPI.Buy(item1, buyAppCallback);
		GameObject.Find("ButtonsSource").GetComponent<AudioSource>().Play();
	}

	public void BuyItem_2(){
		InAppPurchaseManager.MainInAppAPI.Buy(item2, buyAppCallback);
		GameObject.Find("ButtonsSource").GetComponent<AudioSource>().Play();
	}

	public void BuyItem_3(){
		InAppPurchaseManager.MainInAppAPI.Buy(item3, buyAppCallback);
		GameObject.Find("ButtonsSource").GetComponent<AudioSource>().Play();
	}

	//***************************************************************

	private void buyDurableClicked()
	{
		StatusText.text = "";
		InAppPurchaseManager.MainInAppAPI.Buy(item1, buyAppCallback);
	}

	private void buyConsumableClicked()
	{
		StatusText.text = "";
		InAppPurchaseManager.MainInAppAPI.Buy(item3, buyAppCallback);
	}

	private void restoreClicked()
	{
		StatusText.text = "";
		InAppPurchaseManager.MainInAppAPI.Restore(restoreAppsCallback);
	}

	private void getPriceInfoClicked()
	{
		StatusText.text = "";
		InAppPurchaseManager.MainInAppAPI.GetProductInfo(productInfoCallback);
	}

	private void backClicked()
	{
		SceneManager.LoadScene("MainDemo");
	}
	
	private void createdCallback(bool succeeded)
	{
		//StatusText.text = "Init: " + succeeded + System.Environment.NewLine + System.Environment.NewLine;
		InAppPurchaseManager.MainInAppAPI.AwardInterruptedPurchases(awardInterruptedPurchases);
	}

	private void awardInterruptedPurchases(string inAppID, bool succeeded)
	{
		/*int appIndex = InAppPurchaseManager.MainInAppAPI.GetAppIndexForAppID(inAppID);
		if (appIndex != -1)
		{
			StatusText.text += "Interrupted Restore Status: " + inAppID + ": " + succeeded + " Index: " + appIndex;
			StatusText.text += System.Environment.NewLine + System.Environment.NewLine;
		}*/
	}

	private void productInfoCallback(InAppPurchaseInfo[] priceInfos, bool succeeded)
	{
		/*if (succeeded)
		{
			StatusText.text = "";
			foreach (var info in priceInfos)
			{
				if (info.ID == item1) StatusText.text += string.Format("ID: {0} Price: {1}", info.ID, info.FormattedPrice);
			}
		}
		else
		{
			StatusText.text += "Get Price Info Failed!";
		}*/
	}

	void buyAppCallback(string inAppID, string receipt, bool succeeded) {

		int appIndex = InAppPurchaseManager.MainInAppAPI.GetAppIndexForAppID(inAppID);

		if (appIndex != -1)
		{
			/*StatusText.text += "Buy Status: " + inAppID + ": " + succeeded + " Index: " + appIndex;
			if (!string.IsNullOrEmpty(receipt))
			{
				StatusText.text += System.Environment.NewLine + System.Environment.NewLine;
				StatusText.text += receipt;
			}*/
			if (inAppID == "money_pack_1"){
				PlayerPrefs.SetInt("MoneyPurchase", moneyPack1);

				//PlayerPrefs.SetInt("IsNoAdvertising", 1);

				/*#if UNITY_EDITOR
					if (FindObjectOfType<AdsDemo>().isAdvInEditor){
						FindObjectOfType<AdsDemo>().checkVisibility();
						if (FindObjectOfType<AdsDemo>().isVisible){
							FindObjectOfType<AdsDemo>().visibilityClicked();
						}
					}
				#elif WINDOWS_PHONE
					FindObjectOfType<AdsDemo>().checkVisibility();
					if (FindObjectOfType<AdsDemo>().isVisible){
						FindObjectOfType<AdsDemo>().visibilityClicked();
					}	
				#endif*/

				//rebootText.enabled = true;

			} else if (inAppID == "money_pack_2"){
				PlayerPrefs.SetInt("MoneyPurchase", moneyPack2);
				PlayerPrefs.SetInt("IsNoAdvertising", 1);

				#if UNITY_EDITOR
					if (FindObjectOfType<AdsDemo>().isAdvInEditor){
						FindObjectOfType<AdsDemo>().checkVisibility();
						if (FindObjectOfType<AdsDemo>().isVisible){
							FindObjectOfType<AdsDemo>().visibilityClicked();
						}
					}
				#elif WINDOWS_PHONE
					FindObjectOfType<AdsDemo>().checkVisibility();
					if (FindObjectOfType<AdsDemo>().isVisible){
						FindObjectOfType<AdsDemo>().visibilityClicked();
					}	
				#endif
				rebootText.enabled = true;

			} else if (inAppID == "money_pack_3"){
				PlayerPrefs.SetInt("MoneyPurchase", moneyPack3);
				PlayerPrefs.SetInt("IsNoAdvertising", 1);

				#if UNITY_EDITOR
					if (FindObjectOfType<AdsDemo>().isAdvInEditor){
						FindObjectOfType<AdsDemo>().checkVisibility();
						if (FindObjectOfType<AdsDemo>().isVisible){
							FindObjectOfType<AdsDemo>().visibilityClicked();
						}
					}
				#elif WINDOWS_PHONE
					FindObjectOfType<AdsDemo>().checkVisibility();
					if (FindObjectOfType<AdsDemo>().isVisible){
						FindObjectOfType<AdsDemo>().visibilityClicked();
					}	
				#endif
				rebootText.enabled = true;

			}
		}
		//else
		//{
			//StatusText.text += "Failed: " + inAppID + System.Environment.NewLine;
		//}
	}

	public void PlusMoney(){
		PlayerPrefs.SetInt("MoneyPurchase", moneyPack1);
	}

	void restoreAppsCallback(string inAppID, bool succeeded)
	{
		/*int appIndex = InAppPurchaseManager.MainInAppAPI.GetAppIndexForAppID(inAppID);
		if (appIndex != -1)
		{
			StatusText.text += "Restore Status: " + inAppID + ": " + succeeded + " Index: " + appIndex;
			StatusText.text += System.Environment.NewLine + System.Environment.NewLine;
		}
		else
		{
			StatusText.text += "Failed: " + inAppID + System.Environment.NewLine;
		}*/
	}
}
