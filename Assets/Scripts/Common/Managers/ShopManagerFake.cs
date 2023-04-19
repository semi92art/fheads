using System;
using System.Collections.Generic;
using Common.Constants;
using mazing.common.Runtime;
using mazing.common.Runtime.Extensions;
using mazing.common.Runtime.Managers.IAP;
using mazing.common.Runtime.Utils;
using UnityEngine.Events;

namespace Common.Managers
{
    // ReSharper disable once InconsistentNaming
    public static class IAP_ProductInfoExtensions
    {
        public static IAP_ProductInfo Create(
            this IAP_ProductInfo        _Args,
            decimal                  _LocalizedPrice,
            string                   _LocalizedPriceString,
            string                   _Currency,
            bool                     _HasReceipt,
            Func<EShopProductResult> _Result)
        {
            _Args.LocalizedPrice       = _LocalizedPrice;
            _Args.LocalizedPriceString = _LocalizedPriceString;
            _Args.Currency             = _Currency;
            _Args.HasReceipt           = _HasReceipt;
            _Args.Result               = _Result;
            return _Args;
        }
    }

    public class ShopManagerFake : ShopManagerBase
    {
        private const EShopProductResult Result = EShopProductResult.Success;
        
        private readonly Dictionary<int, IAP_ProductInfo> m_ShopItems = new Dictionary<int, IAP_ProductInfo>
        {
            {PurchaseKeys.Money1,       new IAP_ProductInfo().Create(100m, "RUB 100", "RUB", false, () => Result)},
            {PurchaseKeys.Money2,       new IAP_ProductInfo().Create(200m, "RUB 200", "RUB", false, () => Result)},
            {PurchaseKeys.Money3,       new IAP_ProductInfo().Create(300m, "RUB 300", "RUB", false, () => Result)},
            {PurchaseKeys.NoAds,        new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.DarkTheme,    new IAP_ProductInfo().Create(400m, "RUB 500", "RUB", false,  () => Result)},
            {PurchaseKeys.SpecialOffer, new IAP_ProductInfo().Create(400m, "RUB 600", "RUB", false,  () => Result)},
            {PurchaseKeys.X2NewCoins,   new IAP_ProductInfo().Create(400m, "RUB 700", "RUB", false,  () => Result)},
            
            {PurchaseKeys.Character01, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.Character02, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.Character03, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.Character04, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.Character05, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.Character06, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.Character07, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.Character08, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.Character09, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.Character10, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            
            {PurchaseKeys.CharacterColorSet01, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.CharacterColorSet02, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.CharacterColorSet03, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.CharacterColorSet04, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.CharacterColorSet05, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.CharacterColorSet06, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.CharacterColorSet07, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.CharacterColorSet08, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.CharacterColorSet09, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.CharacterColorSet10, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.CharacterColorSet11, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
            {PurchaseKeys.CharacterColorSet12, new IAP_ProductInfo().Create(400m, "RUB 400", "RUB", false, () => Result)},
        };
        
        private readonly Dictionary<int, UnityAction> m_PurchaseActions = new Dictionary<int, UnityAction>();

        public override void RestorePurchases()
        {
            SaveUtils.PutValue(SaveKeysMazor.DisableAds, null);
            Dbg.Log("Purchases restored.");
        }

        public override void Purchase(int _Key)
        {
            m_PurchaseActions[_Key]?.Invoke();
        }

        public override bool RateGame()
        {
            return false;
        }

        public override IAP_ProductInfo GetItemInfo(int _Key)
        {
            return m_ShopItems.GetSafe(_Key, out _);
        }

        public override void AddPurchaseAction(int _ProductKey, UnityAction _Action)
        {
            m_PurchaseActions.SetSafe(_ProductKey, _Action);
        }

        public override void AddDeferredAction(int _Key, UnityAction _Action)
        {
            // do nothing
        }
    }
}