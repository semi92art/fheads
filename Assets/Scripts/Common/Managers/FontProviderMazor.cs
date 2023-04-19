using System.Runtime.CompilerServices;
using mazing.common.Runtime.Enums;
using mazing.common.Runtime.Managers;
using TMPro;

namespace Common.Managers
{
    public class FontProviderMazor : IFontProvider
    {
        #region constants

        private const string PrefabSetName = "fonts";

        #endregion
        
        private IPrefabSetManager PrefabSetManager { get; }

        public FontProviderMazor(IPrefabSetManager _PrefabSetManager)
        {
            PrefabSetManager = _PrefabSetManager;
        }

        public TMP_FontAsset GetFont(ETextType _TextType, ELanguage _Language)
        {
            const string lilitaRegular         = "lilita_one_regular";
            const string lilitaRegularExt120   = "lilita_one_120_regular_extended_ascii";
            const string lilitaRegularExt54    = "lilita_one_54_regular_extended_ascii";
            const string montserratAceRegualar = "montserrat_ace_regular";
            const string multiroundPro         = "multiround_pro";
            
            string prefabName = _TextType switch
            {
                ETextType.GameUI => _Language switch
                {
                    ELanguage.English   => montserratAceRegualar,
                    ELanguage.Russian   => montserratAceRegualar,
                    ELanguage.German    => montserratAceRegualar,
                    ELanguage.Spanish   => montserratAceRegualar,
                    ELanguage.Portugal  => montserratAceRegualar,
                    ELanguage.Japanese  => "japanese_game",
                    ELanguage.Korean    => "korean_game",
                    _                   => throw new SwitchExpressionException(_Language)
                },
                ETextType.MenuUI_H1 => _Language switch
                {
                    ELanguage.English  => lilitaRegularExt54,
                    ELanguage.Russian  => multiroundPro,
                    ELanguage.German   => lilitaRegularExt54,
                    ELanguage.Spanish  => lilitaRegularExt54,
                    ELanguage.Portugal => lilitaRegularExt54,
                    ELanguage.Japanese => "japanese_menu",
                    ELanguage.Korean   => "korean_menu",
                    _                  => throw new SwitchExpressionException(_Language)
                },
                ETextType.MenuUI_H2 => _Language switch
                {
                    ELanguage.English  => lilitaRegularExt54,
                    ELanguage.Russian  => multiroundPro,
                    ELanguage.German   => lilitaRegularExt54,
                    ELanguage.Spanish  => lilitaRegularExt54,
                    ELanguage.Portugal => lilitaRegularExt54,
                    ELanguage.Japanese => "japanese_menu",
                    ELanguage.Korean   => "korean_menu",
                    _                  => throw new SwitchExpressionException(_Language)
                },
                ETextType.MenuUI_H3 => _Language switch
                {
                    ELanguage.English  => lilitaRegular,
                    ELanguage.Russian  => multiroundPro,
                    ELanguage.German   => lilitaRegularExt120,
                    ELanguage.Spanish  => lilitaRegularExt120,
                    ELanguage.Portugal => lilitaRegularExt120,
                    ELanguage.Japanese => "japanese_menu",
                    ELanguage.Korean   => "korean_menu",
                    _                  => throw new SwitchExpressionException(_Language)
                },
                _  => throw new SwitchExpressionException(_TextType)
            };
            return PrefabSetManager.GetObject<TMP_FontAsset>(PrefabSetName, prefabName);
        }
    }
}