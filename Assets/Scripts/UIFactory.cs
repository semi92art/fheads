using System.Text.RegularExpressions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public static class UIFactory
{
    #region public methods
    
    public static Canvas CreateCanvas(
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
    
    
    public static Image CreateImage(
        string _Image,
        RectTransform _Item,
        Color _Color,
        bool _RaycastTarget = false)
    {
        Sprite sprite = Resources.Load<Sprite>($"Sprites/{_Image}");
        Image image = _Item.gameObject.AddComponent<Image>();
        image.sprite = sprite;
        image.color = _Color;
        image.raycastTarget = _RaycastTarget;

        return image;
    }

    public static Text CreateText(
        RectTransform _Item,
        string _Text,
        Font _Font,
        FontStyle _FontStyle,
        int _FontSize,
        float _LineSpacing,
        bool _RichText,
        TextAnchor _Alignment,
        bool _AlignByGeometry,
        HorizontalWrapMode _HorizontalOverflow,
        VerticalWrapMode _VerticalOverflow,
        bool _BestFit,
        Color _Color,
        bool _RaycastTarget,
        Color? _ShadowEffectColor = null,
        Vector2? _ShadowEffectDistance = null,
        bool? _ShadowUseGraphicsAlpha = null
        )
    {
        Text text = _Item.gameObject.AddComponent<Text>();
        text.text = _Text;
        text.font = _Font;
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

    public static Button CreateButton(
        RectTransform _Item,
        string _Image,
        Color _Color,
        bool _Interactable,
        Selectable.Transition _Transition,
        Navigation _Navigation,
        Button.ButtonClickedEvent _OnClick,
        bool _RaycastTarget = false
        )
    {
        Image image = CreateImage(_Image, _Item, _Color, _RaycastTarget);
        Button button = image.gameObject.AddComponent<Button>();
        button.interactable = _Interactable;
        button.transition = _Transition;
        button.navigation = _Navigation;
        button.onClick = _OnClick;

        return button;
    }
    
    public static RectTransform CreateRectTransform(
        RectTransform _Group,
        string _ID,
        Vector2 _AnchorMin,
        Vector2 _AnchorMax,
        Vector2 _AnchoredPosition,
        Vector2 _Pivot,
        Vector2 _SizeDelta
    )
    {
        GameObject gameObj = new GameObject(_ID);
        RectTransform item = gameObj.AddComponent<RectTransform>();
        item.SetParent(_Group);
        item.anchorMin = _AnchorMin;
        item.anchorMax = _AnchorMax;
        item.anchoredPosition = _AnchoredPosition;
        item.pivot = _Pivot;
        item.sizeDelta = _SizeDelta;
        item.localScale = Vector3.one;
        return item;
    }

    #endregion
    
    #region private methods

    

    #endregion
}
