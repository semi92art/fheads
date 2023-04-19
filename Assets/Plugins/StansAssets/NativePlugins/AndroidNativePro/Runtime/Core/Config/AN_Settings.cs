using System.Collections.Generic;
using SA.Foundation.Config;
using SA.Foundation.Patterns;
using SA.Android.Utilities;
using SA.Android.Vending.BillingClient;
using UnityEngine;

namespace SA.Android
{
    class AN_Settings : SA_ScriptableSingleton<AN_Settings>
    {
        public enum StorageType
        {
            Internal,
            External,
            ForceInternal
        }

        public const string PLUGIN_NAME = "Android Native";
        public const string DOCUMENTATION_URL = "https://unionassets.com/android-native-pro/manual";

        public const string AndroidNativeFolder = SA_Config.StansAssetsNativePluginsPath + "AndroidNativePro/";

        public const string EditorFolder = AndroidNativeFolder + "Editor/";
        public const string DependenciesFolder = EditorFolder + "Dependencies/";
        public const string DependenciesFilePath = DependenciesFolder + "AN_Dependencies.xml";

        public const string AndroidFolder = AndroidNativeFolder + "Android/";

        public const string AndroidInternalFolder = AndroidFolder + "Internal/";

        public const string AndroidCoreLibPath = AndroidNativeFolder + "Android/Core/an_library.bundle/";

        public const string AndroidResPath = AndroidCoreLibPath + "res/";
        public const string AndroidValuesPath = AndroidResPath + "values/";
        public const string AndroidDrawablePath = AndroidResPath + "drawable/";
        public const string AndroidRawPath = AndroidResPath + "raw/";

        public const string AndroidManifestFilePath = AndroidCoreLibPath + "AndroidManifest.xml";
        public const string AndroidGamesIdsFilePath = AndroidValuesPath + "games-ids.xml";
        public const string PlayServiceGamesIdsGeneratedFile = AndroidNativeFolder + "Runtime/API/GMS/AN_GamesIds.cs";

        public const string AndroidTestScenePath = AndroidNativeFolder + "Tests/Scene/AN_TestScene.unity";

        //--------------------------------------
        // Editor Settings
        //--------------------------------------

        public bool ManifestManagement = true;
        public bool EnforceEdm4UDependency = true;

        //--------------------------------------
        // Runtime Settings
        //--------------------------------------

        [SerializeField]
        internal AN_LogLevel LogLevel = new AN_LogLevel();

        public bool WtfLogging = false;
        public StorageType PreferredImagesStorage = StorageType.External;

        //--------------------------------------
        // API Settings
        //--------------------------------------

        public bool Vending = false;
        public bool Contacts = false;
        public bool Social = false;
        public bool GooglePlay = false;
        public bool CameraAndGallery = false;

        //--------------------------------------
        // App
        //--------------------------------------

        public bool MediaPlayer = false;
        public bool LocalNotifications = false;
        public bool SkipPermissionsDialog = false;

        //--------------------------------------
        // Billing
        //--------------------------------------

        public string RSAPublicKey = "Base64-encoded RSA public key to include in your binary. Please remove any spaces.";
        public List<AN_SkuDetails> InAppProducts = new List<AN_SkuDetails>();
        public bool Licensing = false;
        public bool GooglePlayBilling = false;

        public void AddInAppProduct(string sku, AN_BillingClient.SkuType productType, bool isConsumable = false)
        {
            var product = new AN_SkuDetails(sku, productType);
            product.Title = sku;
            product.IsConsumable = isConsumable;
            InAppProducts.Add(product);
        }

        //--------------------------------------
        // Google Play
        //--------------------------------------

        public bool GooglePlayGamesAPI = true;

        //--------------------------------------
        // SA_Scriptable Settings
        //--------------------------------------

        protected override string BasePath => AndroidNativeFolder;

        public override string PluginName => PLUGIN_NAME;

        public override string DocumentationURL => DOCUMENTATION_URL;

        public override string SettingsUIMenuItem => SA_Config.EditorMenuRoot + "Android/Services";
    }
}
