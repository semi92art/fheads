// -----------------------------------------------
// Documentation: http://www.reign-studios.net/docs/unity-plugin/
// -----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Reign;

public class InterstitialAdDemo : MonoBehaviour {
	[Header("Is testing mode on:")]
	public bool testing;

	[Header("Turn On/Off advertising in editor:")]
	public bool isAdvInEditor;
	private int isNoAdv;
	private static bool created;
	private static InterstitialAd ad;

	void Awake() {
		isNoAdv = PlayerPrefs.GetInt("IsNoAdvertising");
	}

	void Start() {
		// make sure we don't init the same Ad twice
		if (created) return;
		created = true;

		// create add
		var desc = new InterstitialAdDesc();

		// Global
		desc.Testing = testing;
		desc.EventCallback = eventCallback;
		desc.UseClassicGUI = false;
		desc.GUIOverrideEnabled = false;
		desc.UnityUI_SortIndex = 1001;

		// WP8
		desc.WP8_AdAPI = InterstitialAdAPIs.AdMob;
		#if UNITY_EDITOR
			desc.WP8_AdMob_UnitID = "";// NOTE: Must set event for testing
		#else
			desc.WP8_AdMob_UnitID = "ca-app-pub-1763919853958157/6398733624";
		#endif

			
		// iOS
		desc.iOS_AdAPI = InterstitialAdAPIs.AdMob;
		desc.iOS_AdMob_UnitID = "";// NOTE: Must set event for testing
		
		// Android
		#if AMAZON
			desc.Android_AdAPI = InterstitialAdAPIs.Amazon;
		#else
			desc.Android_AdAPI = InterstitialAdAPIs.AdMob;
		#endif
		desc.Android_AdMob_UnitID = "";// NOTE: Must set event for testing
		desc.Android_Amazon_ApplicationKey = "";// NOTE: Must set event for testing

		// create ad
		if (isNoAdv == 0) {
			#if UNITY_EDITOR
				if (isAdvInEditor) {
					ad = InterstitialAdManager.CreateAd(desc, createdCallback);
					ad.Cache();
				}
			#else
				ad = InterstitialAdManager.CreateAd(desc, createdCallback);
				ad.Cache();
			#endif
		}

	}
	
	private void createdCallback(bool success){

	}

	private static void eventCallback(InterstitialAdEvents adEvent, string eventMessage){
		if (adEvent == InterstitialAdEvents.Cached) ad.Show();
	}
}
