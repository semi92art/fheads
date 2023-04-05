using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButton : MonoBehaviour 
{
    [HideInInspector]
    public Button but_Play;
    public Image im_showcase, im_showcase2;
    public Image im_Head, im_Boot;
    public Text text_numOfGame;
    public Text text_prize;

    void Awake()
    {
        but_Play = GetComponent<Button>();
    }

    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
