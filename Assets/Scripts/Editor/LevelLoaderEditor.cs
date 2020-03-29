using UnityEditor.SceneManagement;
using UnityEditor;

public static class LevelLoaderEditor
{
    [MenuItem("Tools/Load Scene/____Preload")]
    public static void LoadPreloadScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/____Preload.unity");
    }
    
    [MenuItem("Tools/Load Scene/____Menu")]
    public static void LoadMenuScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/____Menu.unity");
    }
    
    [MenuItem("Tools/Load Scene/____Level")]
    public static void LoadLevelScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/____Level.unity");
    }
}