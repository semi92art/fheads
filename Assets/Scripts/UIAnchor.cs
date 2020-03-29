using UnityEngine;

public struct UIAnchor
{
    #region factory

    public static UIAnchor Create(Vector2 _AnchorMin, Vector2 _AnchorMax)
    {
        return Create(_AnchorMin.x, _AnchorMin.y, _AnchorMax.x, _AnchorMax.y);
    }
    
    public static UIAnchor Create(float _MinX, float _MinY, float _MaxX, float _MaxY)
    {
        return new UIAnchor(_MinX, _MinY, _MaxX, _MaxY);
    }

    #endregion
    
    #region properties
    
    public Vector2 Min { get; private set; }
    public Vector2 Max { get; private set; }
    
    #endregion

    #region constructors
    
    public UIAnchor(float _MinX, float _MinY, float _MaxX, float _MaxY)
    {
        Min = new Vector2(_MinX, _MinY);
        Max = new Vector2(_MaxX, _MaxY);
    }

    #endregion
}
