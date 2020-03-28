using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Toggle = UnityEngine.UI.Toggle;

public static class UIExtentions
{
    public static RectTransform rectTransform(this Canvas _Canvas)
    {
        return _Canvas.GetComponent<RectTransform>();
    }

    public static RectTransform rectTransform(this Button _Button)
    {
        return _Button.GetComponent<RectTransform>();
    }
    
    public static RectTransform rectTransform(this ScrollRect _ScrollRect)
    {
        return _ScrollRect.GetComponent<RectTransform>();
    }

    public static RectTransform rectTransform(this Toggle _Toggle)
    {
        return _Toggle.GetComponent<RectTransform>();
    }

    public static RectTransform rectTransform(this Mask _Mask)
    {
        return _Mask.GetComponent<RectTransform>();
    }

    public static RectTransform rectTransform(this Shadow _Shadow)
    {
        return _Shadow.GetComponent<RectTransform>();
    }
    
    public static RectTransform rectTransform(this RectTranshormHelper _RectTransformHelper)
    {
        return _RectTransformHelper.GetComponent<RectTransform>();
    }

}