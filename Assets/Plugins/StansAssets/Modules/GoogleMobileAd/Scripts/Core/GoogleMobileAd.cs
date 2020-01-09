////////////////////////////////////////////////////////////////////////////////
//  
// @module Google Ads Unity SDK 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////



using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GoogleMobileAd  {

	public static GoogleMobileAdInterface controller;
	private static bool _IsInited = false ;
	private static bool _IsInterstitialReady = false;
	private static bool _IsRewardedVideoReady = false;

	
	//Actions
	public static event Action OnInterstitialLoaded 			= delegate {};
	public static event Action OnInterstitialFailedLoading	 	= delegate {};
	public static event Action OnInterstitialOpened 			= delegate {};
	public static event Action OnInterstitialClosed 			= delegate {};
	public static event Action OnInterstitialLeftApplication	= delegate {};
	public static event Action<string> OnAdInAppRequest			= delegate {};



	public static event Action<string, int> OnRewarded				= delegate {};
	public static event Action OnRewardedVideoAdClosed				= delegate {};
	public static event Action<int> OnRewardedVideoAdFailedToLoad 	= delegate {};
	public static event Action OnRewardedVideoAdLeftApplication 	= delegate {};
	public static event Action OnRewardedVideoLoaded 				= delegate {};
	public static event Action OnRewardedVideoAdOpened 				= delegate {};
	public static event Action OnRewardedVideoStarted 				= delegate {};


	public static void Init() 
	{
		switch(Application.platform) 
		{
		/*case RuntimePlatform.IPhonePlayer:
			controller = IOSAdMobController.Instance;
			controller.Init(GoogleMobileAdSettings.Instance.IOS_BannersUnitId);

			if(!GoogleMobileAdSettings.Instance.IOS_InterstisialsUnitId.Equals(string.Empty)) 
				controller.SetInterstisialsUnitID(GoogleMobileAdSettings.Instance.IOS_InterstisialsUnitId);

			if(!GoogleMobileAdSettings.Instance.IOS_RewardedVideoAdUnitId.Equals(string.Empty))
				controller.SetRewardedVideoAdUnitID(GoogleMobileAdSettings.Instance.IOS_RewardedVideoAdUnitId);
			
			break;
		case RuntimePlatform.WP8Player:
			controller = WP8AdMobController.Instance;
			controller.Init(GoogleMobileAdSettings.Instance.WP8_BannersUnitId);
			if(!GoogleMobileAdSettings.Instance.WP8_InterstisialsUnitId.Equals(string.Empty))
				controller.SetInterstisialsUnitID(GoogleMobileAdSettings.Instance.WP8_InterstisialsUnitId);

			break;*/
		default:
			controller = AndroidAdMobController.Instance;
			controller.Init(GoogleMobileAdSettings.Instance.Android_BannersUnitId);

			if(!GoogleMobileAdSettings.Instance.Android_InterstisialsUnitId.Equals(string.Empty))
				controller.SetInterstisialsUnitID(GoogleMobileAdSettings.Instance.Android_InterstisialsUnitId);

			if(!GoogleMobileAdSettings.Instance.Android_RewardedVideoAdUnitId.Equals(string.Empty))
					controller.SetRewardedVideoAdUnitID(GoogleMobileAdSettings.Instance.Android_RewardedVideoAdUnitId);

			break;
		}

		controller.InitEditorTesting(GoogleMobileAdSettings.Instance.IsEditorTestingEnabled, GoogleMobileAdSettings.Instance.EditorFillRate);

		controller.OnInterstitialLoaded			 	+= OnInterstitialLoadedListner;
		controller.OnInterstitialFailedLoading 		+= OnInterstitialFailedLoadingListner;
		controller.OnInterstitialOpened 			+= OnInterstitialOpenedListner;
		controller.OnInterstitialClosed 			+= OnInterstitialClosedListner;
		controller.OnInterstitialLeftApplication 	+= OnInterstitialLeftApplicationListner;
		controller.OnAdInAppRequest 				+= OnAdInAppRequestListner;

		controller.OnRewarded			 			+= OnRewardedListner;
		controller.OnRewardedVideoAdClosed 			+= OnRewardedVideoAdClosedListner;
		controller.OnRewardedVideoAdFailedToLoad 	+= OnRewardedVideoAdFailedToLoadListner;
		controller.OnRewardedVideoAdLeftApplication += OnRewardedVideoAdLeftApplicationListner;
		controller.OnRewardedVideoLoaded 			+= OnRewardedVideoLoadedListner;
		controller.OnRewardedVideoAdOpened 			+= OnRewardedVideoAdOpenedListner;
		controller.OnRewardedVideoStarted 			+= OnRewardedVideoStartedListner;

		_IsInited = true;

		if(GoogleMobileAdSettings.Instance.testDevices.Count > 0)
		{
			List<string> ids = new List<string>();
			foreach(GADTestDevice device in GoogleMobileAdSettings.Instance.testDevices) 
				ids.Add(device.ID);
			
			AddTestDevices(ids.ToArray());
		}
			
		TagForChildDirectedTreatment(GoogleMobileAdSettings.Instance.TagForChildDirectedTreatment);

		foreach(string keywrod in GoogleMobileAdSettings.Instance.DefaultKeywords) 
			AddKeyword(keywrod);
	}




	public static void SetBannersUnitID(string android_unit_id, string ios_unit_id, string wp8_unit_id) {
		if(!_IsInited) {
			Debug.LogWarning ("ChangeBannersUnitID shoudl be called only after Init function. Call ignored");
			return;
		}

		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			controller.SetBannersUnitID(ios_unit_id);
			break;
		case RuntimePlatform.Android:
			controller.SetBannersUnitID(android_unit_id);
			break;
			
		default:
			controller.SetBannersUnitID(wp8_unit_id);
			break;
		}

	}


	public static void SetInterstisialsUnitID(string android_unit_id, string ios_unit_id, string wp8_unit_id) {
		if(!_IsInited) {
			Debug.LogWarning ("ChangeInterstisialsUnitID shoudl be called only after Init function. Call ignored");
			return;
		}

		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			controller.SetInterstisialsUnitID(ios_unit_id);
			break;
		case RuntimePlatform.Android:
			controller.SetInterstisialsUnitID(android_unit_id);
			break;
			
		default:
			controller.SetInterstisialsUnitID(wp8_unit_id);
			break;
		}


	}



	public static GoogleMobileAdBanner CreateAdBanner(TextAnchor anchor, GADBannerSize size)  {
		if(!_IsInited) {
			Debug.LogWarning ("CreateBannerAd shoudl be called only after Init function. Call ignored");
			return null;
		}


		return controller.CreateAdBanner(anchor, size);
	}


	public static GoogleMobileAdBanner CreateAdBanner(int x, int y, GADBannerSize size)  {
		if(!_IsInited) {
			Debug.LogWarning ("CreateBannerAd shoudl be called only after Init function. Call ignored");
			return null;
		}

		return controller.CreateAdBanner(x, y, size);
	}

	

	public static GoogleMobileAdBanner GetBanner(int id) {
		if(!_IsInited) {
			Debug.LogWarning ("GetBanner shoudl be called only after Init function. Call ignored");
			return null;
		}
		
		return controller.GetBanner(id);
	}


	public static void DestroyBanner(int id) {
		if(!_IsInited) {
			Debug.LogWarning ("DestroyCurrentBanner shoudl be called only after Init function. Call ignored");
			return;
		}
		
		controller.DestroyBanner(id);
	}





	//Add a keyword for targeting purposes.
	public static void AddKeyword(string keyword)  {
		if(!_IsInited) {
			Debug.LogWarning ("AddKeyword shoudl be called only after Init function. Call ignored");
			return;
		}
		
		controller.AddKeyword(keyword);
		
	}

	public static void SetBirthday(int year, AndroidMonth month, int day)  {
		if(!_IsInited) {
			Debug.LogWarning ("SetBirthday shoudl be called only after Init function. Call ignored");
			return;
		}
		
		controller.SetBirthday(year, month, day);
		
	}

	public static void TagForChildDirectedTreatment(bool tagForChildDirectedTreatment) {
		if(!_IsInited) {
			Debug.LogWarning ("TagForChildDirectedTreatment shoudl be called only after Init function. Call ignored");
			return;
		}

		controller.TagForChildDirectedTreatment(tagForChildDirectedTreatment);
	}
	
	//Causes a device to receive test ads. The deviceId can be obtained by viewing the logcat output after creating a new ad.
	public static void AddTestDevice(string deviceId) {
		if(!_IsInited) {
			Debug.LogWarning ("AddTestDevice shoudl be called only after Init function. Call ignored");
			return;
		}
		
		controller.AddTestDevice(deviceId);
	}

	//Causes a device to receive test ads. The deviceId can be obtained by viewing the logcat output after creating a new ad.
	public static void AddTestDevices(params string[] ids) {
		if(!_IsInited) {
			Debug.LogWarning ("AddTestDevice shoudl be called only after Init function. Call ignored");
			return;
		}
		
		controller.AddTestDevices(ids);
	}
	
	//Set the user's gender for targeting purposes. This should be GADGenger.GENDER_MALE, GADGenger.GENDER_FEMALE, or GADGenger.GENDER_UNKNOWN
	public static void SetGender(GoogleGender gender) {
		if(!_IsInited) {
			Debug.LogWarning ("SetGender shoudl be called only after Init function. Call ignored");
			return;
		}
		
		controller.SetGender(gender);
	}
	
	
	


	public static void StartInterstitialAd() {
		if(!_IsInited) {
			Debug.LogWarning ("StartInterstitialAd shoudl be called only after Init function. Call ignored");
			return;
		}
		
		controller.StartInterstitialAd();
	}
	
	public static void LoadInterstitialAd() {
		if(!_IsInited) {
			Debug.LogWarning ("LoadInterstitialAd shoudl be called only after Init function. Call ignored");
			return;
		}
		
		controller.LoadInterstitialAd();
	}
	
	public static void ShowInterstitialAd() {
		if(_IsInterstitialReady) {
			_IsInterstitialReady = false;
		} else {
			Debug.LogWarning ("ShowInterstitialAd shoudl be called only what  Interstitial Ad is Ready ");
			return;
		}


		if(!_IsInited) {
			Debug.LogWarning ("ShowInterstitialAd shoudl be called only after Init function. Call ignored");
			return;
		}
		
		controller.ShowInterstitialAd();
	}

	public static void RecordInAppResolution(GADInAppResolution resolution) {
		if(!_IsInited) {
			Debug.LogWarning ("RecordInAppResolution shoudl be called only after Init function. Call ignored");
			return;
		}
		
		controller.RecordInAppResolution(resolution);
	}






	public static void StartRewardedVideo() {
		if(!_IsInited) {
			Debug.LogWarning ("StartRewardedVideo shoudl be called only after Init function. Call ignored");
			return;
		}

		controller.StartRewardedVideo();
	}

	public static void LoadRewardedVideo() {
		if(!_IsInited) {
			Debug.LogWarning ("LoadRewardedVideo shoudl be called only after Init function. Call ignored");
			return;
		}

		controller.LoadRewardedVideo();
	}

	public static void ShowRewardedVideo() {
		if(_IsRewardedVideoReady) {
			_IsRewardedVideoReady = false;
		} else {
			Debug.LogWarning ("ShowRewardedVideo shoudl be called only what  Interstitial Ad is Ready ");
			return;
		}


		if(!_IsInited) {
			Debug.LogWarning ("ShowRewardedVideo shoudl be called only after Init function. Call ignored");
			return;
		}

		controller.ShowRewardedVideo();
	}


	//--------------------------------------
	//  Actions
	//--------------------------------------

	//--------------------------------------
	//  GET / SET
	//--------------------------------------


	public static bool IsInited {
		get {
			return _IsInited;
		}
	}
	
	public static string BannersUunitId {
		get {
			return controller.BannersUunitId;
		}
	}
	
	public static string InterstisialUnitId {
		get {
			return controller.InterstisialUnitId;
		}
	}

	public static bool IsInterstitialReady {
		get {
			return _IsInterstitialReady;
		}
	}
		

	public static string RewardedVideoUnitId {
		get {
			return controller.RewardedVideoAdUnitId;
		}
	}
	public static bool IsRewardedVideoReady {
		get {
			return _IsRewardedVideoReady;
		}
	}




	public bool IsEditorTestingEnabled {
		get {
			return SA_EditorTesting.IsInsideEditor && GoogleMobileAdSettings.Instance.IsEditorTestingEnabled;
		}
	}

	//--------------------------------------
	// Actions Impl
	//--------------------------------------

	private static void OnInterstitialLoadedListner () {
		_IsInterstitialReady = true;
		OnInterstitialLoaded();
	}

	private static void OnInterstitialFailedLoadingListner () {
		_IsInterstitialReady = false;
		OnInterstitialFailedLoading();
	}

	private static void OnInterstitialOpenedListner () {
		_IsInterstitialReady = false;
		OnInterstitialOpened();
	}

	private static void OnInterstitialClosedListner () {
		_IsInterstitialReady = false;
		OnInterstitialClosed();
	}

	private static void OnInterstitialLeftApplicationListner () {
		OnInterstitialLeftApplication();
	}

	private static void	OnAdInAppRequestListner(string itemId) {
		OnAdInAppRequest (itemId);
	}



	private static void OnRewardedVideoLoadedListner () {
		_IsRewardedVideoReady = true;
		OnRewardedVideoLoaded();
	}

	private static void OnRewardedVideoAdFailedToLoadListner (int errorCode) {
		_IsRewardedVideoReady = false;
		OnRewardedVideoAdFailedToLoad(errorCode);
	}

	private static void OnRewardedVideoStartedListner () {
		_IsRewardedVideoReady = false;
		OnRewardedVideoStarted();
	}

	private static void OnRewardedVideoAdOpenedListner () {
		_IsRewardedVideoReady = false;
		OnRewardedVideoAdOpened();
	}

	private static void OnRewardedVideoAdClosedListner () {
		_IsRewardedVideoReady = false;
		OnRewardedVideoAdClosed();
	}

	private static void OnRewardedVideoAdLeftApplicationListner() {
		_IsRewardedVideoReady = false;
		OnRewardedVideoAdLeftApplication ();
	}


	private static void OnRewardedListner (string itemId, int count) {
		_IsRewardedVideoReady = false;
		OnRewarded(itemId, count);
	}





				










}


