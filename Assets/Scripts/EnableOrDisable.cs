using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableOrDisable : MonoBehaviour 
{
    [Header("Enable GameObjects on Awake")]
    public GameObject[] enabOnAwake;
    [Header("Disable GameObjects on Start")]
    public GameObject[] disabOnStart;
    [Header("Make transparent GameObjects on Awake")]
    public GameObject[] transpOnAwake;

    void Awake()
    {
        for (int i = 0; i < enabOnAwake.Length; i++)
        {
            if (enabOnAwake[i] != null)
                enabOnAwake[i].SetActive(true);
        }

        for (int i = 0; i < disabOnStart.Length; i++)
        {
            if (disabOnStart[i] != null)
                disabOnStart[i].SetActive(true);
        }

        for (int i = 0; i < transpOnAwake.Length; i++)
        {
            if (transpOnAwake[i] != null)
            {
                if (transpOnAwake[i].GetComponent<Image>() != null)
                    transpOnAwake[i].GetComponent<Image>().color = Color.clear;

                if (transpOnAwake[i].GetComponent<SpriteRenderer>() != null)
                    transpOnAwake[i].GetComponent<SpriteRenderer>().color = Color.clear;
            }
        }
    }

    void Start()
    {
        for (int i = 0; i < disabOnStart.Length; i++)
        {
            if (disabOnStart[i] != null)
                disabOnStart[i].SetActive(false);
        }
    }
}
