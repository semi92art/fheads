using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class UIFactory
{
    #region public methods
    
    public static Canvas UICanvas(
        string _ID,
        RenderMode _RenderMode,
        bool _PixelPerfect,
        int _SortOrder,
        AdditionalCanvasShaderChannels _AdditionalCanvasShaderChannels,
        CanvasScaler.ScaleMode _ScaleMode,
        Vector2Int _ReferenceResolution,
        CanvasScaler.ScreenMatchMode _ScreenMatchMode,
        float _Match,
        float _ReferencePixelsPerUnit,
        bool _IgnoreReversedGraphics,
        GraphicRaycaster.BlockingObjects _BlockingObjects 
    )
    {
        GameObject go = new GameObject();
        go.name = _ID;
        Canvas canvas = go.AddComponent<Canvas>();
        canvas.renderMode = _RenderMode;
        canvas.pixelPerfect = _PixelPerfect;
        canvas.sortingOrder = _SortOrder;
        canvas.additionalShaderChannels = _AdditionalCanvasShaderChannels;

        CanvasScaler canvasScaler = go.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = _ScaleMode;
        canvasScaler.referenceResolution = _ReferenceResolution;
        canvasScaler.screenMatchMode = _ScreenMatchMode;
        canvasScaler.matchWidthOrHeight = _Match;
        canvasScaler.referencePixelsPerUnit = _ReferencePixelsPerUnit;

        GraphicRaycaster graphicRaycaster = go.AddComponent<GraphicRaycaster>();
        graphicRaycaster.ignoreReversedGraphics = _IgnoreReversedGraphics;
        graphicRaycaster.blockingObjects = _BlockingObjects;

        return canvas;
    }

    public static Image UIImage(
        RectTransform _Item,
        string _StyleName)
    {
        UIStyleObject style = Resources.Load<UIStyleObject>($"Styles/{_StyleName}");
        
        Image image = _Item.gameObject.AddComponent<Image>();
        image.sprite = style.Sprite;
        image.color = style.Color;
        image.raycastTarget = style.RaycastTarget;

        return image;
    }
    
    public static Text UIText(
        RectTransform _Item,
        string _Text,
        string _FontName,
        FontStyle _FontStyle,
        Color _Color,
        int _FontSize,
        TextAnchor _Alignment,
        HorizontalWrapMode _HorizontalOverflow = HorizontalWrapMode.Wrap,
        VerticalWrapMode _VerticalOverflow = VerticalWrapMode.Truncate,
        bool _RichText = false,
        bool _AlignByGeometry = false,
        float _LineSpacing = 1f,
        bool _BestFit = false,
        bool _RaycastTarget = false,
        Color? _ShadowEffectColor = null,
        Vector2? _ShadowEffectDistance = null,
        bool? _ShadowUseGraphicsAlpha = null
        )
    {
        Text text = _Item.gameObject.AddComponent<Text>();
        text.text = _Text;
        Font font = Resources.Load<Font>($"Fonts/{_FontName}");
        if (font != null)
            text.font = font;
        text.fontStyle = _FontStyle;
        text.fontSize = _FontSize;
        text.lineSpacing = _LineSpacing;
        text.supportRichText = _RichText;
        text.alignment = _Alignment;
        text.alignByGeometry = _AlignByGeometry;
        text.horizontalOverflow = _HorizontalOverflow;
        text.verticalOverflow = _VerticalOverflow;
        text.resizeTextForBestFit = _BestFit;
        text.color = _Color;
        text.raycastTarget = _RaycastTarget;

        if (_ShadowEffectColor != null)
        {
            Shadow shadow = _Item.gameObject.AddComponent<Shadow>();
            shadow.effectColor = (Color)_ShadowEffectColor;
            if (_ShadowEffectDistance != null)
                shadow.effectDistance = (Vector2)_ShadowEffectDistance;
            if (_ShadowUseGraphicsAlpha != null)
                shadow.useGraphicAlpha = (bool)_ShadowUseGraphicsAlpha;
        }

        return text;
    }

    public static Button UIButton(
        RectTransform _Item,
        UnityAction _OnClick,
        Navigation _Navigation,
        Selectable.Transition _Transition = Selectable.Transition.None,
        bool _Interactable = true
    )
    {
        Image image = _Item.gameObject.AddComponent<Image>();
        image.color = Utility.Transparent;
        image.raycastTarget = true;
        image.sprite = null;
        Button button = _Item.gameObject.AddComponent<Button>();
        button.interactable = _Interactable;
        button.transition = _Transition;
        button.navigation = _Navigation;
        
        Button.ButtonClickedEvent @event = new Button.ButtonClickedEvent();
        @event.AddListener(_OnClick);
        button.onClick = @event;

        return button;
    }

    public static RectTransform UIRectTransform(
        RectTransform _Group,
        string _ID,
        UIAnchor _Anchor,
        Vector2 _AnchoredPosition,
        Vector2 _Pivot,
        Vector2 _SizeDelta
    )
    {
        GameObject gameObject = new GameObject(_ID);
        RectTransform item = gameObject.AddComponent<RectTransform>();
        item.SetParent(_Group);
        item.anchorMin = _Anchor.Min;
        item.anchorMax = _Anchor.Max;
        item.anchoredPosition = _AnchoredPosition;
        item.pivot = _Pivot;
        item.sizeDelta = _SizeDelta;
        item.localScale = Vector3.one;
        
#if UNITY_EDITOR
        if (gameObject.GetComponent<RectTranshormHelper>() == null)
            gameObject.AddComponent<RectTranshormHelper>();
#endif
        return item;
    }

    

    #endregion

}
