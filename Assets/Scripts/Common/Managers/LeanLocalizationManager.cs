using System;
using System.Collections.Generic;
using System.Linq;
using Lean.Localization;
using mazing.common.Runtime.Entities;
using mazing.common.Runtime.Enums;
using mazing.common.Runtime.Exceptions;
using mazing.common.Runtime.Extensions;
using mazing.common.Runtime.Helpers;
using mazing.common.Runtime.Managers;
using mazing.common.Runtime.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Common.Managers
{
    public class LeanLocalizationManager : InitBase, ILocalizationManager
    {
        #region constants

        private const string KeyEmpty = "empty_key";

        #endregion
    
        #region nonpublic members

        private LeanLocalization m_Localization;
        private readonly Dictionary<string, List<LocTextInfo>> m_LocInfosDict =
            new Dictionary<string, List<LocTextInfo>>(); 

        #endregion

        #region inject

        private ILanguageSetting LanguageSetting { get; }
        private IFontProvider    FontProvider    { get; }

        private LeanLocalizationManager(
            ILanguageSetting _LanguageSetting,
            IFontProvider    _FontProvider)
        {
            LanguageSetting = _LanguageSetting;
            FontProvider    = _FontProvider;
        }

        #endregion

        #region api
        
        public event UnityAction<ELanguage> LanguageChanged;
        
        public override void Init()
        {
            if (Initialized)
                return;
            LanguageSetting.ValueSet += SetLanguage;
            LanguageSetting.GetValue = GetCurrentLanguage;
            if (m_Localization != null)
                return;
            var go = new GameObject("Localization");
            Object.DontDestroyOnLoad(go);
            m_Localization = go.AddComponent<LeanLocalization>();
            var culturesDict = GetCultures();
            foreach (var (key, value) in culturesDict)
                m_Localization.AddLanguage(key.ToString(), value);
            foreach (var (key, _) in culturesDict)
            {
                var goCsv = new GameObject(key + "CSV");
                goCsv.SetParent(m_Localization.transform);
                var leanCsv = goCsv.AddComponent<LeanLanguageCSV>();
                leanCsv.Source = Resources.Load<TextAsset>($"Texts/{key}");
                leanCsv.Language = key.ToString();    
            }
            m_Localization.DefaultLanguage = "English";
            LeanLocalization.OnLocalizationChanged += InvokeLanguageChangedEvent;
            base.Init();
        }
        
        public string GetTranslation(string _Key)
        {
            return LeanLocalization.GetTranslationText(_Key);
        }

        public void SetLanguage(ELanguage _Language)
        {
            m_Localization.SetCurrentLanguage(_Language.ToString());
            foreach (var locInfos in m_LocInfosDict.Values)
            {
                var destroyed = locInfos.ToArray()
                    .Where(_Args => _Args == null || _Args.TextObject.IsNull());
                locInfos.RemoveRange(destroyed);
            }
            foreach (var args in m_LocInfosDict.Values.SelectMany(_LocInfos => _LocInfos))
            {
                UpdateLocalization(args);
            }
        }

        public ELanguage GetCurrentLanguage()
        {
            Enum.TryParse(LeanLocalization.CurrentLanguage, out ELanguage lang);
            return lang;
        }

        public void AddLocalization(LocTextInfo _Info)
        {
            if (_Info.TextObject.IsNull())
                return;
            if (string.IsNullOrEmpty(_Info.LocalizationKey))
                _Info.LocalizationKey = KeyEmpty;
            foreach (var locInfo in m_LocInfosDict.Values)
            {
                var destroyed = locInfo
                    .Where(_Args => _Args == null || _Args.TextObject.IsNull());
                locInfo.RemoveRange(destroyed);
            }
            if (!m_LocInfosDict.ContainsKey(_Info.LocalizationKey))
                m_LocInfosDict.Add(_Info.LocalizationKey, new List<LocTextInfo>());
            var args = m_LocInfosDict[_Info.LocalizationKey]
                .FirstOrDefault(_Args => _Args.TextObject == _Info.TextObject);
            if (args != null)
            {
                args.TextFormula = _Info.TextFormula;
                args.TextType = _Info.TextType;
                UpdateLocalization(args);
            }
            else
            {
                args = _Info.Clone() as LocTextInfo;
                m_LocInfosDict[_Info.LocalizationKey].Add(args);
            }
            UpdateLocalization(args);
        }

        public TMP_FontAsset GetFont(ETextType _TextType, ELanguage? _Language = null)
        {
            var language = _Language ?? GetCurrentLanguage();
            return FontProvider.GetFont(_TextType, language);
        }

        #endregion

        #region nonpublic methods

        private void UpdateLocalization(LocTextInfo _Info)
        {
            if (!(_Info.TextObject is TMP_Text tmpText))
                return;
            switch (_Info.TextLocalizationType)
            {
                case ETextLocalizationType.TextAndFont:
                {
                    string translation = GetTranslation(_Info.LocalizationKey);
                    tmpText.text = _Info.TextFormula == null ? translation : _Info.TextFormula(translation);
                    tmpText.font = GetFont(_Info.TextType);
                }
                    break;
                case ETextLocalizationType.OnlyText:
                {
                    string translation = GetTranslation(_Info.LocalizationKey);
                    tmpText.text = _Info.TextFormula == null ? translation : _Info.TextFormula(translation);
                }
                    break;
                case ETextLocalizationType.OnlyFont:
                    tmpText.font = GetFont(_Info.TextType);
                    break;
                default:
                    throw new SwitchCaseNotImplementedException(_Info.TextLocalizationType);
            }
        }

        private void InvokeLanguageChangedEvent()
        {
            LanguageChanged?.Invoke(GetCurrentLanguage());
        }

        ~LeanLocalizationManager()
        {
            LeanLocalization.OnLocalizationChanged -= InvokeLanguageChangedEvent;
        }

        private static Dictionary<ELanguage, string[]> GetCultures()
        {
            return new Dictionary<ELanguage, string[]>
            {
                {ELanguage.English,   new[] {"en",  "en-GB"}},
                {ELanguage.German,    new[] {"ger", "ger-GER"}},
                {ELanguage.Spanish,   new[] {"sp",  "sp-SP"}},
                {ELanguage.Portugal,  new[] {"por", "por-POR"}},
                {ELanguage.Russian,   new[] {"ru",  "ru-RUS"}},
                {ELanguage.Japanese,  new[] {"ja",  "ja-JAP"}},
                {ELanguage.Korean,    new[] {"ko",  "ko-KOR"}},
            };
        }

        #endregion
    }
}