using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region static properties
    public static UIManager Instance { get; private set; }
    
    #endregion
    
    #region private fields

    private MenuUI m_Menu;
    private LevelUI m_Level;
    
    #endregion

    #region engine methods
    
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += (_Arg0, _Scene) =>
        {
            if (_Scene.name == "____Menu")
            {
                m_Menu = gameObject.AddComponent<MenuUI>();
                Destroy(m_Level);
            }
            else if (_Scene.name == "____Level")
            {
                m_Level = gameObject.AddComponent<LevelUI>();
                Destroy(m_Menu);
            }
        };
    }

    #endregion
}
