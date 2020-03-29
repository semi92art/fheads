using UnityEditorInternal;
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
        Image image = _Item.gameObject.AddComponent<Image>();
        UIStyleObject style = Resources.Load<UIStyleObject>($"Styles/{_StyleName}");
        if (style == null)
            return image;
        
        image.sprite = style.sprite;
        image.color = style.imageColor;
        image.raycastTarget = style.raycastImageTarget;
        image.useSpriteMesh = style.useSpriteMesh;
        image.preserveAspect = style.preserveAspect;
        image.pixelsPerUnitMultiplier = style.pixelsPerUnityMultyply;
        image.type = style.imageType;
        image.fillMethod = style.fillMethod;
        image.fillOrigin = style.fillOrigin;
        image.fillClockwise = style.fillClockwise;
        return image;
    }
    
    public static Text UIText(
        RectTransform _Item,
        string _StyleName
        )
    {
        Text text = _Item.gameObject.AddComponent<Text>();
        UIStyleObject style = Resources.Load<UIStyleObject>($"Styles/{_StyleName}");
        
        text.text = style.textID;
        text.font = style.font;
        text.fontStyle = style.fontStyle;
        text.fontSize = style.fontSize;
        text.lineSpacing = style.lineSpacing;
        text.supportRichText = style.richText;
        text.alignment = style.alignment;
        text.alignByGeometry = style.alignByGeometry;
        text.horizontalOverflow = style.horizontalOverflow;
        text.verticalOverflow = style.verticalOverflow;
        text.resizeTextForBestFit = style.bestFit;
        text.color = style.textColor;
        text.raycastTarget = style.raycastTextTarget;

        if (style.shadowOn)
        {
            Shadow shadow = _Item.gameObject.AddComponent<Shadow>();
            shadow.effectColor = style.shadowEffectColor;
            shadow.effectDistance = style.shadowEffectDistance;
            shadow.useGraphicAlpha = style.shadowUseGraphicsAlpha;
        }

        return text;
    }

    public static Button UIButton(
        RectTransform _Item,
        string _StyleName,
        UnityAction _OnClick,
        Image targetGraphic
    )
    {
        Image image = _Item.gameObject.AddComponent<Image>();
        image.sprite = null;
        image.color = new Color(0f, 0f, 0f, 0f);
        image.raycastTarget = true;
        
        Button button = _Item.gameObject.AddComponent<Button>();
        UIStyleObject style = Resources.Load<UIStyleObject>($"Styles/{_StyleName}");
        
        button.transition = style.transition;
        button.interactable = style.interactable;
        button.targetGraphic = targetGraphic;
        switch (style.transition)
        {
            case Selectable.Transition.ColorTint:
                ColorBlock colorBlock = new ColorBlock();
                colorBlock.normalColor = style.normalState.Color;
                colorBlock.highlightedColor = style.highlightedState.Color;
                colorBlock.pressedColor = style.pressedState.Color;
                colorBlock.selectedColor = style.selectedState.Color;
                colorBlock.disabledColor = style.disabledState.Color;
                colorBlock.colorMultiplier = style.colorMultiplyer;
                colorBlock.fadeDuration = style.fadeDuration;
                button.colors = colorBlock;
                break;
            case Selectable.Transition.SpriteSwap:
                SpriteState state = new SpriteState();
                state.highlightedSprite = style.highlightedState.Sprite;
                state.pressedSprite = style.pressedState.Sprite;
                state.selectedSprite = style.selectedState.Sprite;
                state.disabledSprite = style.disabledState.Sprite;
                button.spriteState = state;
                break;
            case Selectable.Transition.Animation:
                break;
            case Selectable.Transition.None:
                break;
        }
        
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
