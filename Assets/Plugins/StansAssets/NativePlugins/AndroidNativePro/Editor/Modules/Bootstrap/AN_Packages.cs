using System.Collections.Generic;
using System.IO;
using StansAssets.Foundation.Editor;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditor.PackageManager;
using UnityEngine;

namespace SA.Android
{
    public static class AN_Packages
    {
        public const string FirebaseAnalyticsPackage = "com.google.firebase.analytics";
        public const string FirebaseMessagingPackage = "com.google.firebase.messaging";

        const string k_Edm4UVersion = "1.2.165";
        static readonly string k_Edm4UPackageDownloadUrl = $"https://github.com/googlesamples/unity-jar-resolver/blob/v{k_Edm4UVersion}/external-dependency-manager-{k_Edm4UVersion}.unitypackage?raw=true";

        static bool IsEdm4UInstalled() {
            var precompiledAssemblies = CompilationPipeline.GetPrecompiledAssemblyNames();
            foreach (var assemblyName in precompiledAssemblies) {
                if (assemblyName.StartsWith("Google.JarResolver")) {
                    return true;
                }
            }

            return false;
        }

        public static void InstallEdm4U() {
            // If Edm4U is already installed we would do nothing.
            if (IsEdm4UInstalled()) {
                return;
            }

            var request = EditorWebRequest.Get(k_Edm4UPackageDownloadUrl);
            request.AddEditorProgressDialog("Downloading Google External Dependency Manager");
            request.Send(unityRequest => {
                if (unityRequest.error != null) {
                    EditorUtility.DisplayDialog("Package Download failed.", unityRequest.error, "Ok");
                    return;
                }

                //Asset folder name remove
                var projectPath = Application.dataPath.Substring(0, Application.dataPath.Length - 6);
                var tmpPackageFile = projectPath + FileUtil.GetUniqueTempPathInProject() + ".unityPackage";

                File.WriteAllBytes(tmpPackageFile, unityRequest.downloadHandler.data);

                AssetDatabase.ImportPackage(tmpPackageFile, false);
            });
        }

        public static void InstallFirebasePackage(string packageName) {
            Debug.LogError("Google Scope Registry is not supported anymore!");
        }

        public static bool IsMessagingSdkInstalled {
            get {
#if AN_FIREBASE_MESSAGING
                return true;
#else
                return false;
#endif
            }
        }

        public static bool IsAnalyticsSdkInstalled {
            get {
#if AN_FIREBASE_ANALYTICS
                return true;
#else
                return false;
#endif
            }
        }

        static void AddPackage(ScopeRegistry scopeRegistry, string packageName, string packageVersion) {
            var manifest = new StansAssets.Foundation.Editor.Manifest();
            manifest.Fetch();

            var manifestUpdated = false;
            if (!manifest.TryGetScopeRegistry(scopeRegistry.Url, out _)) {
                manifest.SetScopeRegistry(scopeRegistry.Url, scopeRegistry);
                manifestUpdated = true;
            }

            if (!manifest.IsDependencyExists(packageName)) {
                manifest.SetDependency(packageName, packageVersion);
                manifestUpdated = true;
            }

            if (manifestUpdated)
                manifest.ApplyChanges();
        }
    }
}
