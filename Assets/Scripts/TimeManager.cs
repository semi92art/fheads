using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region static properties
    
    public static TimeManager Instance { get; private set; }
    
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
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public bool GamePaused { get { return Time.timeScale <= float.Epsilon; } }

    public float TimeScale
    {
        get { return Time.timeScale; }
        set { Time.timeScale = value; }
    }
    
    #endregion
    
    
}
