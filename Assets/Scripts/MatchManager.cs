using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    #region public properties

    public static MatchManager Instance { get; private set; }
    public bool GameStarted { get; set; }
    public bool Restart { get; set; }
        
    #endregion
    
    #region private fields

    private LevelSounds m_LevelSounds;

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
        //m_LevelSounds
    }

    private void Start()
    {
        GameStarted = true;
    }

    #endregion
    
}