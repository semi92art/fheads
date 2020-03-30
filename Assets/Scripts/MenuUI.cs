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
        CreateTournaments();
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
    
    public void CreateTournaments()
    {
        RectTransform playNow = UIFactory.UIImage(
            UIFactory.UIRectTransform(
                m_Canvas.rectTransform(),
                "tournaments",
                UIAnchor.Create(Vector2.one, Vector2.one),
                new Vector2(-180f, -383f),
                Utility.HalfOne,
                new Vector2(340f, 230f)
            ),
            "tournaments_container").rectTransform;
        
        UIFactory.UIText(
            UIFactory.UIRectTransform(
                playNow,
                "text",
                UIAnchor.Create(Vector2.zero, Vector2.right),
                new Vector2(0, 26.3f),
                Utility.HalfOne,
                new Vector2(0, 52.6f)),
            "tournaments");
        
        UIFactory.UIImage(
            UIFactory.UIRectTransform(
                playNow,
                "icon",
                UIAnchor.Create(Vector2.up, Vector2.one),
                new Vector2(0, -88.7f), 
                Utility.HalfOne,
                new Vector2(0, 177.4f)),
            "tournaments");
        
        UIFactory.UIButton(
            UIFactory.UIRectTransform(
                playNow,
                "button",
                UIAnchor.Create(Vector2.zero, Vector2.one),
                Vector2.zero,
                Utility.HalfOne,
                Vector2.zero),
            "tournaments",
            () =>
            {
                Debug.Log("Tournaments Button Pushed");
                //Button functionality
            },
            playNow.GetComponent<Image>());
    }

}