using UnityEngine;

public static class Utility
{
    public static void CopyToClipboard(this string _Text)
    {
        TextEditor te = new TextEditor();
        te.text = _Text;
        te.SelectAll();
        te.Copy();
    }

    public static string ToStringAlt(this Vector2 _Value)
    {
        return $"({_Value.x.ToString("F2").Replace(',','.')}f, " +
               $"{_Value.y.ToString("F2").Replace(',','.')}f)";
    }
    
    public static string ToStringAlt2(this Vector2 _Value)
    {
        switch (_Value.x)
        {
            case 0:
                switch (_Value.y)
                {
                    case 0:
                        return "Vector2.zero";
                    case 1:
                        return "Vector2.up";
                    case -1:
                        return "Vector2.down";
                }
                break;
            case 1:
                switch (_Value.y)
                {
                    case 0:
                        return "Vector2.right";
                    case 1:
                        return "Vector2.one";
                }
                break;
            case -1:
                switch (_Value.y)
                {
                    case 0:
                        return "Vector2.left";
                    case -1:
                        return "Vector2.one * -1f";
                }
                break;
        }

        return _Value.ToStringAlt();
    }
    
    public static Vector2 HalfOne => Vector2.one * .5f;
    public static Color Transparent => new Color(0f, 0f, 0f, 0f);
}
