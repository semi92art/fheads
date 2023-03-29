// -----------------------------------------------
// Documentation: http://www.reign-studios.net/docs/unity-plugin/
// -----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Reign;
using UnityEngine.SceneManagement;

public class AdsDemo : MonoBehaviour
{
	[Header("Is testing mode on:")]
	public bool testing;
	[Header("Turn On/Off advertising in editor:")]
	public bool isAdvInEditor;

	public int isNoAdv;
//	private static AdsDemo singleton;
	private static bool created;
	private static Ad ad;

	[HideInInspector]
	public bool isVisible;

	void Awake() {
		isNoAdv = PlayerPrefs.GetInt("IsNoAdvertising");
	}
	
	// -----------------------------------------------
	// NOTE: You can set up multiple platforms at once and the API will use the correct desc data for each
	// -----------------------------------------------
	void Start() {
		//singleton = this;

		// make sure we don't init the same Ad twice
		if (created){
			if (ad != null){
				if (SceneManager.GetActiveScene().name != "Level"){
					ad.Visible = true;
				} else {
					ad.Visible = false;
				}
			} 
			return;
		}
		created = true;

		// Ads - NOTE: You can pass in multiple "AdDesc" objects if you want more then one ad.
		var desc = new AdDesc();

		// global settings
		//desc.Testing = true;// NOTE: To test ads on iOS, you must enable them in iTunes Connect.
		desc.Testing = testing;
		desc.Visible = true;
		desc.EventCallback = eventCallback;
		desc.UnityUI_AdMaxWidth = .3f;
		desc.UnityUI_AdMaxHeight = .15f;
		desc.UseClassicGUI = false;
		desc.GUIOverrideEnabled = false;
		desc.UnityUI_SortIndex = 1000;

		// Editor
		desc.Editor_AdAPI = AdAPIs.EditorTestAd;
		desc.Editor_AdGravity = AdGravity.BottomCenter;
		desc.Editor_AdScale = 2;

		desc.Editor_MillennialMediaAdvertising_APID = "";
		desc.Editor_MillennialMediaAdvertising_AdGravity = AdGravity.BottomCenter;
		//desc.Editor_MillennialMediaAdvertising_RefreshRate = 120,

			
		// WP8 settings (Windows Phone 8.0 & 8.1)

		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//WP8_AdAPI - to change MS Advertising/AdMob/Another
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


		//desc.WP8_AdAPI = AdAPIs.MicrosoftAdvertising;

		desc.WP8_MicrosoftAdvertising_ApplicationID = "";
		desc.WP8_MicrosoftAdvertising_UnitID = "";
		desc.WP8_MicrosoftAdvertising_AdGravity = AdGravity.BottomCenter;
		desc.WP8_MicrosoftAdvertising_AdSize = WP8_MicrosoftAdvertising_AdSize.Wide_480x80;
		//desc.WP8_MicrosoftAdvertising_UseBuiltInRefresh = false;
		//desc.WP8_MicrosoftAdvertising_RefreshRate = 120;

		desc.WP8_AdAPI = AdAPIs.AdMob;

		#if UNITY_EDITOR
			desc.WP8_AdMob_UnitID = "";// NOTE: You MUST have this even for Testing!
		#else
			desc.WP8_AdMob_UnitID = "ca-app-pub-1763919853958157/1329405626";
		#endif

		desc.WP8_AdMob_AdGravity = AdGravity.BottomRight;
		desc.WP8_AdMob_AdSize = Reign.WP8_AdMob_AdSize.Banner;

			
		desc.Android_AdMob_AdGravity = AdGravity.BottomCenter;
		desc.Android_AdMob_AdSize = Android_AdMob_AdSize.Banner_320x50;
			
		desc.Android_AmazonAds_ApplicationKey = "";
		desc.Android_AmazonAds_AdSize = Android_AmazonAds_AdSize.Wide_320x50;
		desc.Android_AmazonAds_AdGravity = AdGravity.BottomCenter;
		//desc.Android_AmazonAds_RefreshRate = 120;

		// create ad
		if (SceneManager.GetActiveScene().name=="MainMenu" && isNoAdv == 0){
			#if UNITY_EDITOR
				if (isAdvInEditor) ad = AdManager.CreateAd(desc, adCreatedCallback);
			#else
				ad = AdManager.CreateAd(desc, adCreatedCallback);
			#endif
		}
	}

	private void refreshClicked(){
		ad.Refresh();
	}

	public void visibilityClicked(){
		isNoAdv = PlayerPrefs.GetInt("IsNoAdvertising");
		if (isNoAdv == 0) ad.Visible = !ad.Visible;
	}

	public void checkVisibility(){
		isNoAdv = PlayerPrefs.GetInt("IsNoAdvertising");
		if (isNoAdv == 0) isVisible = ad.Visible;
	}

	private void adCreatedCallback(bool succeeded){

	}

	private static void eventCallback(AdEvents adEvent, string eventMessage){
		// NOTE: On BB10 these events never get called!
		switch (adEvent){
			case AdEvents.Refreshed: break;
			case AdEvents.Clicked: break;
			case AdEvents.Error: break;
		}
	}
}
