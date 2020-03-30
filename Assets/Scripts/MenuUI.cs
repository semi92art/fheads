using System.Collections.Generic;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    #region attributes
    
    private Canvas m_Canvas;
    
    #endregion
    
    #region engine methods

    private void Start()
    {
        CreateCanvas();
        
        UIFactory.UIImage(
            UIFactory.UIRectTransform(
                m_Canvas.GetComponent<RectTransform>(),
                "background",
                UIAnchor.Create(Vector2.zero, Vector2.one),
                Vector2.zero,
                Utility.HalfOne,
                Vector2.zero
            ),
            "menu_background");

        CreatePlayOneOnOne();
        CreateLocalization();
    }
    
    #endregion

    public void CreateCanvas()
    {
        m_Canvas = UIFactory.UICanvas(
            "canvas",
            RenderMode.ScreenSpaceOverlay,
            false,
            3,
            AdditionalCanvasShaderChannels.Normal,
            CanvasScaler.ScaleMode.ScaleWithScreenSize,
            new Vector2Int(1280, 720),
            CanvasScaler.ScreenMatchMode.MatchWidthOrHeight,
            0f,
            100f,
            true,
            GraphicRaycaster.BlockingObjects.All);
    }

    public void CreateLocalization()
    {
        //Create main localization object
        GameObject localizationObject = new GameObject("localizationObject");
        LeanLocalization localization = localizationObject.AddComponent<LeanLocalization>();
        //add languages in list
        string[] cultres= new string[2]{"en", "en-GB"};
        localization.AddLanguage("English", cultres);
        cultres= new string[2]{"ru", "ru-RUS"};
        localization.AddLanguage("Russian", cultres);
        cultres= new string[2]{"ger", "ger-GER"};
        localization.AddLanguage("German", cultres);
        
        //Create readers from localization files
        GameObject englishCSV = new GameObject("englishCSV");
        englishCSV.transform.SetParent(localizationObject.transform);
        LeanLanguageCSV engCSV = englishCSV.AddComponent<LeanLanguageCSV>();
        engCSV.Source = Resources.Load<TextAsset>("Texts/English");
        engCSV.Language = "English";
        
        GameObject russianCSV = new GameObject("russianCSV");
        russianCSV.transform.SetParent(localizationObject.transform);
        LeanLanguageCSV rusCSV = russianCSV.AddComponent<LeanLanguageCSV>();
        rusCSV.Source = Resources.Load<TextAsset>("Texts/Russian");
        rusCSV.Language = "Russian";
        
        GameObject germanCSV = new GameObject("germanCSV");
        germanCSV.transform.SetParent(localizationObject.transform);
        LeanLanguageCSV gerCSV=germanCSV.AddComponent<LeanLanguageCSV>();
        gerCSV.Source = Resources.Load<TextAsset>("Texts/German");
        gerCSV.Language = "German";
        
        localization.SetCurrentLanguage("English");
        
        //ATTENTION! This component need to be added to existed button text (in factory)
        GameObject buttonText = GameObject.Find("play_one_on_one").transform.Find("text").gameObject;
        LeanLocalizedText locComponent = buttonText.AddComponent<LeanLocalizedText>();
        locComponent.TranslationName = "Play1on1";

    }
    public void CreatePlayOneOnOne()
    {
        RectTransform playNow = UIFactory.UIImage(
            UIFactory.UIRectTransform(
                m_Canvas.rectTransform(),
                "play_one_on_one",
                UIAnchor.Create(Vector2.one, Vector2.one),
                new Vector2(-180f, -120f),
                Utility.HalfOne,
                new Vector2(340f, 230f)
            ),
            "play1on1_container").rectTransform;
        
        UIFactory.UIText(
            UIFactory.UIRectTransform(
                playNow,
                "text",
                UIAnchor.Create(Vector2.zero, Vector2.right),
                new Vector2(0, 26.3f),
                Utility.HalfOne,
                new Vector2(0, 52.6f)),
            "play1on1");
        
       UIFactory.UIImage(
            UIFactory.UIRectTransform(
                playNow,
                "icon",
                UIAnchor.Create(Vector2.up, Vector2.one),
                new Vector2(0, -88.7f), 
                Utility.HalfOne,
                new Vector2(0, 177.4f)),
            "play1on1");
        
        UIFactory.UIButton(
            UIFactory.UIRectTransform(
                playNow,
                "button",
                UIAnchor.Create(Vector2.zero, Vector2.one),
                Vector2.zero,
                Utility.HalfOne,
                Vector2.zero),
            "play1on1",
            () =>
            {
                Debug.Log("Play1on1 Button");
                GameManager.Instance.LoadRandomLevel();
            },
            playNow.GetComponent<Image>());
    }
}