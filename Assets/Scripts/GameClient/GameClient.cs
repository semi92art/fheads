using System.Collections;
using UnityEngine;

public class GameClient : MonoBehaviour
{
    #region public properties
    
    public static GameClient Instance { get; private set; }
    public bool IsServerOnline { get; private set; }
    public bool IsPingingNow { get; private set; }
    
    #endregion

    #region private fields

    private string m_SeverIP;
    private bool m_IsPrintLog;
    private float m_PingTime;
    private bool m_StopPing;

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

        m_SeverIP = "91.192.174.192";
        m_IsPrintLog = true;
        m_PingTime = 5f;
    }

    #endregion
    
    #region public methods
    
    public void PermanentPingStart()
    {
        IsPingingNow = true;
        StartCoroutine(Coroutines.Action(() => PingServer(new Ping(m_SeverIP), false)));
    }

    public void PermanentPingStop()
    {
        IsPingingNow = false;
    }

    public void SinglePing(System.Action<bool> _Action)
    {
        StartCoroutine(Coroutines.Action(() => PingServer(new Ping(m_SeverIP), true, _Action)));
    }
    
    #endregion

    #region private methods
    
    private void PingServer(Ping _Ping, bool _Single, System.Action<bool> _Action = null)
    {
        bool isDone = _Ping.isDone;
        IsServerOnline = isDone;
        _Action?.Invoke(isDone);
        if (m_IsPrintLog)
            PrintLog(_Ping);

        if (_Single || !IsPingingNow)
            return;
        
        _Ping = new Ping(m_SeverIP);
        StartCoroutine(Coroutines.Delay(() => PingServer(_Ping, false), m_PingTime));
    }

    private void PrintLog(Ping _Ping)
    {
        Debug.Log($"{_Ping.ip} : {IsServerOnline.ToString()}");
        Debug.Log($"{_Ping.time.ToString()} msec");
    }
    
    #endregion
}
