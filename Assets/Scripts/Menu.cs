using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private Canvas m_Canvas;
    public void CreateCanvas()
    {
        m_Canvas = UIFactory.CreateCanvas(
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

    public void CreateBackground()
    {
        UIFactory.CreateImage(
            "menu_background_2",
            UIFactory.CreateRectTransform(
                m_Canvas.GetComponent<RectTransform>(),
                "background",
                Vector2.zero,
                Vector2.one,
                Vector2.zero,
                Vector2.one * .5f,
                Vector2.zero
                ),
            new Color(0.1f, 0.8f, 0.1f));
    }
    
    
}
