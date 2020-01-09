#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(GoogleMobileAdSettings))]
public class GoogleMobileAdSettingsEditor : Editor {


	
	
	GUIContent IOS_UnitAdId  	 = new GUIContent("Banners Ad Unit Id [?]:",  "IOS Banners Ad Unit Id ");
	GUIContent IOS_InterstAdId   = new GUIContent("Interstitials Ad Unit Id [?]:", "IOS Interstitials Ad Unit Id");
	GUIContent IOS_VideoAdId   = new GUIContent("Rewarded Video Ad Unit Id [?]:", "IOS Rewarded Video Ad Unit Id");

	GUIContent Android_UnitAdId  	 = new GUIContent("Banners Ad Unit Id [?]:",  "Android Banners Ad Unit Id ");
	GUIContent Android_InterstAdId   = new GUIContent("Interstitials Ad Unit Id [?]:", "Android Interstitials Ad Unit Id");
	GUIContent Android_RewardedVideoAdId   = new GUIContent("Rewarded Video Ad Unit Id [?]:", "Android Rewarded Video Ad Unit Id");

	GUIContent WP8_UnitAdId  	 = new GUIContent("Banners Ad Unit Id [?]:",  "WP8 Banners Ad Unit Id ");
	GUIContent WP8_InterstAdId   = new GUIContent("Interstitials Ad Unit Id [?]:", "WP8 Interstitials Ad Unit Id");

	
	GUIContent SdkVersion   = new GUIContent("Plugin Version [?]", "This is Plugin version.  If you have problems or compliments please include this so we know exactly what version to look out for.");
	GUIContent SupportEmail = new GUIContent("Support [?]", "If you have any technical quastion, feel free to drop an e-mail");

	GUIContent deviceNameLabel = new GUIContent("Device Name [?]:", "Name of your device. Just for you");
	GUIContent deviceIdLabel = new GUIContent("Device ID [?]:", "ID of your device. You can get ot from console log");

	private GoogleMobileAdSettings settings;

	void Awake() {
		if (IsInstalled && IsUpToDate) {
			UpdateManifest();
		}
	}

	public override void OnInspectorGUI() {
		#if UNITY_WEBPLAYER
		EditorGUILayout.HelpBox("Editing Google Mobile Ad Settings not avaliable with web player platfrom. Please swith to any other platfrom under Build Seting menu", MessageType.Warning);
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.Space();

		if(GUILayout.Button("Switch To WP8",  GUILayout.Width(120))) {
			EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.WP8Player);
		}

		if(GUILayout.Button("Switch To Android",  GUILayout.Width(120))) {
			EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
		}

		if(GUILayout.Button("Switch To IOS",  GUILayout.Width(120))) {
			#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7
			EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iPhone);
			#else
			EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iOS);
			#endif
		}
		EditorGUILayout.EndHorizontal();
		
		if(true) {return;}
		
		#endif

		settings = target as GoogleMobileAdSettings;

		GUI.changed = false;



		GeneralOptions();
		EditorGUILayout.Space();
		MainSettings();
		EditorGUILayout.Space();
		EditorTesting();
		EditorGUILayout.Space();
		RequestSettings();
		EditorGUILayout.Space();
		AboutGUI();
	

		if(GUI.changed) {
			DirtyEditor();
		}
	}



	public static bool IsInstalled {
		get {
		return SA.Common.Editor.VersionsManager.Is_GMA_Installed;
		}
	}
	
	public static bool IsUpToDate {
		get {
		if(CurrentVersion == SA.Common.Editor.VersionsManager.GMA_Version) {
				return true;
			} else {
				return false;
			}
		}
	}

	
	public static int CurrentVersion {
		get {
		return SA.Common.Editor.VersionsManager.ParceVersion(GoogleMobileAdSettings.VERSION_NUMBER);
		}
	}
	
	public static int CurrentMagorVersion {
		get {
		return SA.Common.Editor.VersionsManager.ParceMagorVersion(GoogleMobileAdSettings.VERSION_NUMBER);
		}
	}
	

	
	public static void UpdateVersionInfo() {
	SA.Common.Util.Files.Write(SA.Common.Config.GMA_VERSION_INFO_PATH, GoogleMobileAdSettings.VERSION_NUMBER);
		UpdateManifest ();
	}

	private static int[] rates = new int[]{0, 20, 50, 80, 100};
	private string[] FillRateToolbarStrings = new string[] {"0%", "20%", "50%", "80%", "100%"};
	private void EditorTesting() {
		EditorGUILayout.HelpBox("Editor Testing", MessageType.None);
		EditorGUILayout.Space();

	GoogleMobileAdSettings.Instance.IsEditorTestingEnabled = SA.Common.Editor.Tools.ToggleFiled("Editor Testing", GoogleMobileAdSettings.Instance.IsEditorTestingEnabled);
		
		GUI.enabled = GoogleMobileAdSettings.Instance.IsEditorTestingEnabled;
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Fill Rate:");
		GoogleMobileAdSettings.Instance.EditorFillRateIndex = GUILayout.Toolbar(GoogleMobileAdSettings.Instance.EditorFillRateIndex, FillRateToolbarStrings, EditorStyles.radioButton);
		GoogleMobileAdSettings.Instance.EditorFillRate = rates[GoogleMobileAdSettings.Instance.EditorFillRateIndex];
		EditorGUILayout.Space();
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("");
		EditorGUILayout.LabelField("0% - Always Error");
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("");
		EditorGUILayout.LabelField("100% - Always Provide Ad");
		EditorGUILayout.EndHorizontal();
		
		GUI.enabled = true;
	}

	private void GeneralOptions() {
		
		if(!IsInstalled) {
			EditorGUILayout.HelpBox("Install Required ", MessageType.Error);
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Space();
			Color c = GUI.color;
			GUI.color = Color.cyan;
			if(GUILayout.Button("Install Plugin",  GUILayout.Width(120))) {
			SA.Common.Editor.Instalation.Android_InstallPlugin();
			SA.Common.Editor.Instalation.IOS_InstallPlugin();
				UpdateVersionInfo();
			}
			GUI.color = c;
			EditorGUILayout.EndHorizontal();
		}
		
		if(IsInstalled) {
			if(!IsUpToDate) {
			EditorGUILayout.HelpBox("Update Required \nResources version: " + SA.Common.Editor.VersionsManager.GMA_StringVersionId + " Plugin version: " + GoogleMobileAdSettings.VERSION_NUMBER, MessageType.Warning);
				
				
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.Space();
				Color c = GUI.color;
				GUI.color = Color.cyan;


			if(CurrentMagorVersion != SA.Common.Editor.VersionsManager.GMA_MagorVersion) {
					if(GUILayout.Button("How to update",  GUILayout.Width(250))) {
						Application.OpenURL("https://goo.gl/XkgvnG");
					}
				} else {
					if(GUILayout.Button("Upgrade Resources",  GUILayout.Width(250))) {
					SA.Common.Editor.Instalation.Android_InstallPlugin();
					SA.Common.Editor.Instalation.IOS_InstallPlugin();
						UpdateVersionInfo();
					}
				}

				
				GUI.color = c;
				EditorGUILayout.Space();
				EditorGUILayout.EndHorizontal();
				Actions();
			} else {
				EditorGUILayout.HelpBox("Google Mobile Ad Plugin v" + GoogleMobileAdSettings.VERSION_NUMBER + " is installed", MessageType.Info);

				PluginSettings();
				Actions();
			}
		}
		
		
		EditorGUILayout.Space();
		
	}
	

	private void PluginSettings() {
		EditorGUILayout.Space();
		EditorGUILayout.HelpBox("Plugin Settings", MessageType.None);
		


		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Keep Android Manifest Clean");
		
		EditorGUI.BeginChangeCheck();
		GoogleMobileAdSettings.Instance.KeepManifestClean = EditorGUILayout.Toggle(GoogleMobileAdSettings.Instance.KeepManifestClean);
		if(EditorGUI.EndChangeCheck()) {
			UpdateManifest();
		}
		
		if(GUILayout.Button("[?]",  GUILayout.Width(27))) {
			Application.OpenURL("http://goo.gl/5rLoDV");
		}
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		EditorGUILayout.EndHorizontal();
	}

	private void Actions() {
		EditorGUILayout.Space();
		GoogleMobileAdSettings.Instance.ShowActions = EditorGUILayout.Foldout(GoogleMobileAdSettings.Instance.ShowActions, "More Actions");
		if(GoogleMobileAdSettings.Instance.ShowActions) {

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Space();
			
			if(GUILayout.Button("Open Manifest ",  GUILayout.Width(160))) {
			UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal("Assets" + SA.Manifest.Manager.MANIFEST_FILE_PATH, 1);
			}

			if(GUILayout.Button("Reset Settings",  GUILayout.Width(160))) {
				ResetSettings();
			}
			
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Space();

			
			if(GUILayout.Button("Load Example Settings",  GUILayout.Width(160))) {
				LoadExampleSettings();
			}

			if(GUILayout.Button("Reinstall",  GUILayout.Width(160))) {
			SA.Common.Editor.Instalation.Android_InstallPlugin();
			SA.Common.Editor.Instalation.IOS_InstallPlugin();
				UpdateVersionInfo();
			}
			
			
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
			
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Space();
			
			if(GUILayout.Button("Remove",  GUILayout.Width(160))) {
			SA.Common.Editor.RemoveTool.RemovePlugins();
			}
			
			EditorGUILayout.EndHorizontal();	
		}
	}


	public static void LoadExampleSettings() {
		
		GoogleMobileAdSettings.Instance.Android_BannersUnitId = "ca-app-pub-6101605888755494/1824764765";
		GoogleMobileAdSettings.Instance.Android_InterstisialsUnitId = "ca-app-pub-6101605888755494/3301497967";
		GoogleMobileAdSettings.Instance.Android_RewardedVideoAdUnitId = "ca-app-pub-6101605888755494/4996523169";


		
		GoogleMobileAdSettings.Instance.IOS_BannersUnitId = "ca-app-pub-6101605888755494/1852640761";
		GoogleMobileAdSettings.Instance.IOS_InterstisialsUnitId = "ca-app-pub-6101605888755494/3329373962";
		GoogleMobileAdSettings.Instance.IOS_RewardedVideoAdUnitId = "ca-app-pub-6101605888755494/3513401162";


		GoogleMobileAdSettings.Instance.WP8_BannersUnitId = "ca-app-pub-6101605888755494/8658089162";
		GoogleMobileAdSettings.Instance.WP8_InterstisialsUnitId = "ca-app-pub-6101605888755494/1134822367";


		GoogleMobileAdSettings.Instance.testDevices.Clear();
		GADTestDevice dev = new GADTestDevice();
		dev.ID = "6B9FA8031AEFDC4758B7D8987F77A5A6";
		dev.Name = "Nexus 7";
		dev.IsOpen = false;
		GoogleMobileAdSettings.Instance.AddDevice(dev);


		dev = new GADTestDevice();
		dev.ID = "5c2286dd9be7697bddd186fbd3301fc9";
		dev.Name = "iPhone 6s";
		dev.IsOpen = false;
		GoogleMobileAdSettings.Instance.AddDevice(dev);





		
		
	}
	
	public static void ResetSettings() {
	SA.Common.Util.Files.DeleteFile("Extensions/GoogleMobileAd/Resources/GoogleMobileAdSettings.asset");
		GoogleMobileAdSettings.Instance.ShowActions = true;
		Selection.activeObject = GoogleMobileAdSettings.Instance;
	}


	string NewKeyword = "";
	public void RequestSettings() {


		EditorGUILayout.Space();
		EditorGUILayout.HelpBox("Settings", MessageType.None);
		EditorGUILayout.Space();


		settings.IsKeywordsOpened = EditorGUILayout.Foldout(settings.IsKeywordsOpened, "Keywords");
		if(settings.IsKeywordsOpened) {
			if(GoogleMobileAdSettings.Instance.DefaultKeywords.Count == 0) {
				EditorGUILayout.HelpBox("No Default Keywords", MessageType.Info);
			}

			foreach(string keywrod in GoogleMobileAdSettings.Instance.DefaultKeywords) {
				EditorGUILayout.BeginVertical (GUI.skin.box);

				EditorGUILayout.BeginHorizontal();

				EditorGUILayout.LabelField(keywrod);

			bool IsRemoved = SA.Common.Editor.Tools.SrotingButtons(keywrod, GoogleMobileAdSettings.Instance.DefaultKeywords);
				if(IsRemoved) {
					return;
				}
				EditorGUILayout.EndHorizontal();


				EditorGUILayout.EndVertical ();
			}





			EditorGUILayout.BeginVertical (GUI.skin.box);

			EditorGUILayout.BeginHorizontal();
			NewKeyword = EditorGUILayout.TextField(NewKeyword);
			EditorGUILayout.Space();
			if(GUILayout.Button("Add new Keyword", GUILayout.Width(150))) {

				if(!settings.DefaultKeywords.Contains(NewKeyword)) {
					settings.DefaultKeywords.Add(NewKeyword);
				}
				NewKeyword = string.Empty;
			}
			EditorGUILayout.EndHorizontal();


			EditorGUILayout.Space();
			EditorGUILayout.EndVertical ();


		}

		settings.IsTestSettinsOpened = EditorGUILayout.Foldout(settings.IsTestSettinsOpened, "Test Devices");
		if(settings.IsTestSettinsOpened) {

			if(GoogleMobileAdSettings.Instance.testDevices.Count == 0) {
				EditorGUILayout.HelpBox("No Test Devices Registred so far", MessageType.Info);
			}
			foreach(GADTestDevice device in GoogleMobileAdSettings.Instance.testDevices) {
				
				EditorGUI.indentLevel = 1;
				EditorGUILayout.BeginVertical (GUI.skin.box);
				device.IsOpen = EditorGUILayout.Foldout(device.IsOpen, device.Name);
				if(device.IsOpen) {
					EditorGUI.indentLevel = 2;
					
					
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField(deviceNameLabel);
					device.Name	 	= EditorGUILayout.TextField(device.Name);
					EditorGUILayout.EndHorizontal();
					
					
					
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField(deviceIdLabel);
					device.ID	 	= EditorGUILayout.TextField(device.ID);
					if(device.ID.Length > 0) {
						device.ID = device.ID.Trim();
					}
					EditorGUILayout.EndHorizontal();
					
					
					
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.Space();
					

					
					if(GUILayout.Button("Remove",  GUILayout.Width(80))) {
						GoogleMobileAdSettings.Instance.RemoveDevice(device);
						return;
					}
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.Space();
					
				}
				
				EditorGUILayout.EndVertical ();
				
			}

			EditorGUI.indentLevel = 0;
			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.Space();
			if(GUILayout.Button("Register New Device",  GUILayout.Width(135))) {
				GoogleMobileAdSettings.Instance.AddDevice(new GADTestDevice());
			}
			
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

		}


	settings.TagForChildDirectedTreatment = SA.Common.Editor.Tools.ToggleFiled("Tag For Child Directed Treatment", settings.TagForChildDirectedTreatment);
	}

	public void MainSettings() {

		EditorGUILayout.HelpBox("Platforms", MessageType.None);
		EditorGUILayout.Space();

		settings.IsIOSSettinsOpened = EditorGUILayout.Foldout(settings.IsIOSSettinsOpened, "IOS");
		if(settings.IsIOSSettinsOpened) {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(IOS_UnitAdId);
			settings.IOS_BannersUnitId	 	= EditorGUILayout.TextField(settings.IOS_BannersUnitId);
			if(settings.IOS_BannersUnitId.Length > 0) {
				settings.IOS_BannersUnitId		= settings.IOS_BannersUnitId.Trim();
			}

			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(IOS_InterstAdId);
			settings.IOS_InterstisialsUnitId	 	= EditorGUILayout.TextField(settings.IOS_InterstisialsUnitId);
			if(settings.IOS_InterstisialsUnitId.Length > 0) {
				settings.IOS_InterstisialsUnitId		= settings.IOS_InterstisialsUnitId.Trim();
			}

			EditorGUILayout.EndHorizontal();


			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(IOS_VideoAdId);
			settings.IOS_RewardedVideoAdUnitId	 	= EditorGUILayout.TextField(settings.IOS_RewardedVideoAdUnitId);
			if(settings.IOS_RewardedVideoAdUnitId.Length > 0) {
				settings.IOS_RewardedVideoAdUnitId		= settings.IOS_RewardedVideoAdUnitId.Trim();
			}

			EditorGUILayout.EndHorizontal();
		}


		settings.IsAndroidSettinsOpened = EditorGUILayout.Foldout(settings.IsAndroidSettinsOpened, "Android");
		if(settings.IsAndroidSettinsOpened) {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(Android_UnitAdId);
			settings.Android_BannersUnitId	 	= EditorGUILayout.TextField(settings.Android_BannersUnitId);
			if(settings.Android_BannersUnitId.Length > 0) {
				settings.Android_BannersUnitId		= settings.Android_BannersUnitId.Trim();
			}


			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(Android_InterstAdId);
			settings.Android_InterstisialsUnitId	 	= EditorGUILayout.TextField(settings.Android_InterstisialsUnitId);
			if(settings.Android_InterstisialsUnitId.Length > 0) {
				settings.Android_InterstisialsUnitId		= settings.Android_InterstisialsUnitId.Trim();
			}

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(Android_RewardedVideoAdId);
			settings.Android_RewardedVideoAdUnitId	 	= EditorGUILayout.TextField(settings.Android_RewardedVideoAdUnitId);
			if(settings.Android_RewardedVideoAdUnitId.Length > 0) {
				settings.Android_RewardedVideoAdUnitId		= settings.Android_RewardedVideoAdUnitId.Trim();
			}
			
			EditorGUILayout.EndHorizontal();
		}



		settings.IsWP8SettinsOpened = EditorGUILayout.Foldout(settings.IsWP8SettinsOpened, "WP8");
		if(settings.IsWP8SettinsOpened) {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(WP8_UnitAdId);
			settings.WP8_BannersUnitId	 	= EditorGUILayout.TextField(settings.WP8_BannersUnitId);
			if(settings.WP8_BannersUnitId.Length > 0) {
				settings.WP8_BannersUnitId		= settings.WP8_BannersUnitId.Trim();
			}

			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(WP8_InterstAdId);
			settings.WP8_InterstisialsUnitId	 	= EditorGUILayout.TextField(settings.WP8_InterstisialsUnitId);
			if(settings.WP8_InterstisialsUnitId.Length > 0) {
				settings.WP8_InterstisialsUnitId		= settings.WP8_InterstisialsUnitId.Trim();
			}

			EditorGUILayout.EndHorizontal();
		}


	
	}



	private void AboutGUI() {

		EditorGUILayout.HelpBox("Version Info", MessageType.None);
		EditorGUILayout.Space();
		
		SelectableLabelField(SdkVersion, GoogleMobileAdSettings.VERSION_NUMBER);
		SelectableLabelField(SupportEmail, "stans.assets@gmail.com");
		
		
	}
	
	private void SelectableLabelField(GUIContent label, string value) {
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(label, GUILayout.Width(180), GUILayout.Height(16));
		EditorGUILayout.SelectableLabel(value, GUILayout.Height(16));
		EditorGUILayout.EndHorizontal();
	}



	private static void DirtyEditor() {
		#if UNITY_EDITOR
		EditorUtility.SetDirty(GoogleMobileAdSettings.Instance);
		#endif
	}

	public static void UpdateManifest() {
		if(!GoogleMobileAdSettings.Instance.KeepManifestClean) {
			return;
		}
		
		SA.Manifest.Manager.Refresh();
		
		SA.Manifest.Template Manifest =  SA.Manifest.Manager.GetManifest();
		SA.Manifest.ApplicationTemplate application =  Manifest.ApplicationTemplate;
		SA.Manifest.ActivityTemplate launcherActivity = application.GetLauncherActivity();

		SA.Manifest.PropertyTemplate targetSdk = Manifest.GetOrCreatePropertyWithTag ("uses-sdk");
		targetSdk.SetValue ("android:targetSdkVersion", "25");

		SA.Manifest.ActivityTemplate AdActivity = application.GetOrCreateActivityWithName("com.google.android.gms.ads.AdActivity");
		AdActivity.SetValue("android:configChanges", "keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize");
		AdActivity.SetValue("android:theme", "@android:style/Theme.Translucent");

		if(launcherActivity != null) {
			SA.Manifest.PropertyTemplate ForwardNativeEventsToDalvik = launcherActivity.GetOrCreatePropertyWithName("meta-data",  "unityplayer.ForwardNativeEventsToDalvik");
			ForwardNativeEventsToDalvik.SetValue("android:value", "true");
		}

		SA.Manifest.PropertyTemplate games_version = application.GetOrCreatePropertyWithName("meta-data",  "com.google.android.gms.version");
		#if UNITY_4_6 || UNITY_4_7
		games_version.SetValue("android:value", GoogleMobileAdSettings.PLAY_SERVICE_VERSION_NUMBER);
		#else
		games_version.SetValue("android:value", "@integer/google_play_services_version");
		#endif

		SA.Manifest.Manager.SaveManifest();
	}	
	
}
#endif
