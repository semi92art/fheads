using UnityEditor;

[InitializeOnLoad]
public static class OnEditorLauncher
{
    static OnEditorLauncher()
    {
        SetAndroidKeyPassword();
    }

    private static void SetAndroidKeyPassword()
    {
        PlayerSettings.keyaliasPass = "Anthony_1980";
        PlayerSettings.keystorePass = "Anthony_1980";
    }
}
