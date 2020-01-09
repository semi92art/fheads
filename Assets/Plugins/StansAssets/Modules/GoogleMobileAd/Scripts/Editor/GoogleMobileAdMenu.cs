////////////////////////////////////////////////////////////////////////////////
//  
// @module V2D
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

public class GoogleMobileAdMenu : EditorWindow {
	


	#if UNITY_EDITOR

	//--------------------------------------
	//  GENERAL
	//--------------------------------------

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Edit Settings", false, 106)]
	public static void Edit() {
		Selection.activeObject = GoogleMobileAdSettings.Instance;
	}

	//--------------------------------------
	//  SETUP
	//--------------------------------------

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/Setup/Plugin Setup", false, 106)]
	public static void GMASPluginSetup() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/plugin-set-up-114");
	}

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/Setup/Plugin Update", false, 106)]
	public static void GMASPluginUpdate() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/updates-115");
	}

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/Setup/Manifest Requirements", false, 106)]
	public static void GMASManifestRequirements() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/android-manifest-requirements-196");
	}

	//--------------------------------------
	//  GETTING STARTED
	//--------------------------------------

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/Getting Started/Before you begin", false, 106)]
	public static void GMAGSBeforeYouBegin() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/plugin-set-up-117");
	}

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/Getting Started/Setup for IOS", false, 106)]
	public static void GMAGSSetupForIOS() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/setup-for-ios-119");
	}

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/Getting Started/Setup for Android", false, 106)]
	public static void GMAGSSetupForAndroid() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/setup-for-android-120");
	}

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/Getting Started/Setup for WP8", false, 106)]
	public static void GMAGSSetupForWP8() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/setup-for-ios-121");
	}

	//--------------------------------------
	//  IMPLEMENTATION
	//--------------------------------------

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/Implementation/Initialization", false, 106)]
	public static void GMAIInitialization() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/initialization-182");
	}

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/Implementation/Banners", false, 106)]
	public static void GMAIBanners() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/banners-183");
	}

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/Implementation/Interstitial", false, 106)]
	public static void GMAIInterstitial() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/interstitial-181");
	}

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/Implementation/In App Purchase Listener", false, 106)]
	public static void GMAIInAppPurchaseListener() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/inapppurchaselistener-122");
	}

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/Implementation/Prefab Solution", false, 106)]
	public static void GMAIPrefabSolution() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/prefab-solution-185");
	}

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/Implementation/Video Tutorials", false, 106)]
	public static void GMAIVideoTutorials() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/video-tutorials-184");
	}

	//--------------------------------------
	//  MORE
	//--------------------------------------

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/More/Released Apps with the plugin", false, 106)]
	public static void GMAMReleasedAppsWithThePlugin() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/released-apps-with-the-plugin-128");
	}

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/More/Playmaker Support", false, 106)]
	public static void GMAMPlaymakerSupport() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/playmaker-actions-124");
	}

	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/More/Frequently Asked Questions", false, 106)]
	public static void GMAMFrequentlyAskedQuestions() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/frequently-asked-questions-126");
	}
	
	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/More/Using Plugins with Java Script", false, 106)]
	public static void GMAMUsingPluginsWithJavaScript() {
		Application.OpenURL("https://unionassets.com/android-native-plugin/using-plugins-with-java-script-201");
	}
		
	[MenuItem("Window/Stan's Assets/GoogleMobileAd/Documentation/More/Migrating to Unity5", false, 106)]
	public static void GMAMMigratingToUnity5() {
		Application.OpenURL("https://unionassets.com/google-mobile-ads-sdk/migrating-to-unity5-365");
	}
	
	#endif

}
#endif
