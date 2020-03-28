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

    [Header("Button:")]
    public bool interactable;
    public Selectable.Transition transition;
    
    public UIState normalState;
    public UIState highlightedState;
    public UIState pressedState;
    public UIState selectedState;
    public UIState disabledState;
    [Range(1f, 5f)] public float colorMultiplyer;
    public float fadeDuration;
    
    [Header("Image & Text")]
    public Color color;
    public bool raycastTarget;
    public string text;
    public Sprite sprite;
    
    #endregion
}
