using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance { get; private set; }
    
    private Dictionary<int, string> m_Languages = new Dictionary<int, string>
    {
        { 0, "English" },
        { 1, "Russian" },
        { 2, "German" },
        { 3, "Italian" },
        { 4, "Spanish" },
        { 6, "Portugal" },
        { 5, "Ukrainian" },
        { 7, "Chineese" },
        { 8, "Japaneese" },
        { 9, "Arabic" },
        { 10, "Romanian" },
        { 11, "Denmark" },
        { 12, "Nederlands" },
        { 13, "Czech" }
    };
    
    public string GetValue(string _Key)
    {
        return GetValueFromBase(m_Languages[PrefsManager.Instance.Language], _Key);
    }

    #region service methods
    
    private string GetValueFromBase(string _Language, string _Key)
    {
        //TODO make load form database
        return string.Empty;
    }
    
    #endregion
    
    #region engine methods

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
    }
    
    #endregion
}