using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClient : MonoBehaviour
{
    private static GameClient m_Instance;
    private Ping m_Ping;
    
    public bool IsServerOnline { get; private set; }

    public string serverIP;
    public bool isPrintLog = true;
    public float pingTime;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        this.CheckPing(serverIP);
    }

    protected GameClient()
    {
        
    }

    public static GameClient getInstance()
    {
        if (m_Instance == null)
        {
            m_Instance= new GameClient();
        }

        return m_Instance;
    }

    public void CheckPing(string _IP)
    {
        m_Ping = new Ping(_IP);
        StartCoroutine(PingServer(_IP));
    }

    IEnumerator PingServer(string _IP)
    {
        WaitForSeconds sec = new WaitForSeconds(pingTime);
        yield return sec;
        
        if (m_Ping.isDone)
        {
            IsServerOnline = true;
            PrintLog(m_Ping);
            m_Ping = new Ping(_IP);
        }
        else
        {
            IsServerOnline = false;
            PrintLog(m_Ping);
        }
        StartCoroutine(PingServer(_IP));
    }

    private void PrintLog(Ping _Ping)
    {
        if (isPrintLog)
        {
            Debug.Log(_Ping.ip + " : " + IsServerOnline);
            Debug.Log(_Ping.time + " msec");
        }
    }
}
