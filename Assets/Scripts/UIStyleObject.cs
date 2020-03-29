using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new_style", menuName = "Style", order = 1)]
public class UIStyleObject : ScriptableObject
{
    #region types

    [System.Serializable]
    public struct UIState
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private Color color;

        public Sprite Sprite => sprite;
        public Color Color => color;
    }
    
    #endregion
    
    #region public fields

    [Header("BUTTON PROPERTIES:")]
    public bool interactable;
    public Selectable.Transition transition;
    public UIState normalState;
    public UIState highlightedState;
    public UIState pressedState;
    public UIState selectedState;
    public UIState disabledState;
    [Range(1f, 5f)] public float colorMultiplyer = 1f;
    public float fadeDuration;
    
    [Header("IMAGE PROPERTIES:")]
    public Sprite sprite;
    public Color imageColor = Color.white;
    public bool raycastImageTarget = true;
    public Image.Type imageType = Image.Type.Simple;
    public bool useSpriteMesh;
    public bool preserveAspect = true;
    public float pixelsPerUnityMultyply;
    public Image.FillMethod fillMethod;
    [Range(0, 3)] public int fillOrigin;
    public bool fillClockwise = true;
    
    
    
    [Header("TEXT PROPERTIES:")]
    public Color textColor = Color.white;
    public bool raycastTextTarget;
    public string textID;
    public Font font;
    public FontStyle fontStyle = FontStyle.Normal;
    public int fontSize = 10;
    public TextAnchor alignment = TextAnchor.MiddleCenter;
    public HorizontalWrapMode horizontalOverflow = HorizontalWrapMode.Wrap;
    public VerticalWrapMode verticalOverflow = VerticalWrapMode.Truncate;
    public bool richText;
    public bool alignByGeometry;
    public float lineSpacing = 1f;
    public bool bestFit;
    public bool shadowOn;
    public Color shadowEffectColor = Color.black;
    public Vector2 shadowEffectDistance;
    public bool shadowUseGraphicsAlpha;

    #endregion
}