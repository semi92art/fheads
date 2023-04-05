using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageScript : MonoBehaviour 
{
    [SerializeField]
	private Scripts scr;

     /*
     * English - 0
     * Portugalian - 1
     * Spanish - 2
     * Italian - 3
     * Russian - 11
     */

    public Font font_Main;
    public Font font_Second;

    [System.Serializable]
    public class StaticTextList_1
    {
        public string english;
        public string portugal;
        public string spanish;
        public string italian;
        public string russian;
        public Text text;
    }
        
    public List<StaticTextList_1> staticTextL_1;


	void Awake()
	{
        Set_Language(true);
	}

    void Start()
    {
        
    }

    public void Set_Language(bool onAwake)
    {
        if (onAwake)
        {
            int lang = PlayerPrefs.GetInt("Language");

            switch (lang)
            {
                case 0:
                    for (int i = 0; i < staticTextL_1.Count; i++)
                    {
                        if (staticTextL_1[i].text != null && staticTextL_1[i].english != "")
                            staticTextL_1[i].text.text = staticTextL_1[i].english;
                    }
                    break;
                case 1:
                    for (int i = 0; i < staticTextL_1.Count; i++)
                    {
                        if (staticTextL_1[i].text != null && staticTextL_1[i].portugal != "")
                        {
                            staticTextL_1[i].text.text = staticTextL_1[i].portugal;
                            staticTextL_1[i].text.font = font_Second;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < staticTextL_1.Count; i++)
                    {
                        if (staticTextL_1[i].text != null && staticTextL_1[i].spanish != "")
                        {
                            staticTextL_1[i].text.text = staticTextL_1[i].spanish;
                            staticTextL_1[i].text.font = font_Second;
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < staticTextL_1.Count; i++)
                    {
                        if (staticTextL_1[i].text != null && staticTextL_1[i].italian != "")
                        {
                            staticTextL_1[i].text.text = staticTextL_1[i].italian;
                            staticTextL_1[i].text.font = font_Second;
                        }
                    }
                    break;
                case 11:
                    for (int i = 0; i < staticTextL_1.Count; i++)
                    {
                        if (staticTextL_1[i].text != null && staticTextL_1[i].russian != "")
                            staticTextL_1[i].text.text = staticTextL_1[i].russian;
                    }
                    break;
            }
        }
        else
            SceneManager.LoadScene(1);
    }
}
