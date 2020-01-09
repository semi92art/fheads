using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class InAppPurchase_0 : MonoBehaviour 
{
    private const string SKU_NoAds = "no_ads";
    private const string SKU_UnlimFreeze = "unlim_freeze";
    private const string SKU_MoneyPack_1 = "money_pack_1_";
    private const string SKU_MoneyPack_2 = "money_pack_2_";
    private const string SKU_MoneyPack_3 = "money_pack_3";

    private static string price_NoAds;
    private static string price_UnlimFreeze;
    private static string price_MoneyPack_1;
    private static string price_MoneyPack_2;
    private static string price_MoneyPack_3;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        /*string base64_id = 
            "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCg" +
            "KCAQEAkH+eGdntzW9aN8ByHuOsIwhW7Ox9mCnjqd" +
            "bY81RyOMHxT1EQiv+Vt+Co5F+lZFSHhTTsTF4EG7aK" +
            "14SR1im0l7PAyTKODE1AMffs26CkgRPCxJ8W2/j1ndn" +
            "mldo9qeptvICdw2kY0aCrt0M9d8lWfE1UDeCQZg6nn5" +
            "IHLZ7el9w0PWWgZj2RxywSoL5CIBpCCR5uxoEzZm9V0" +
            "nL9sUerC9plPQP9u7aVkkseDQL5jWts1W0ij4oPPezTV" +
            "j+vzqCNdNp0wzOSFagyrAJlEuypQzgE3g/DEAjkOmvIBT" +
            "yoXIGMQ3zRMgWr+L44fJjB5DpQfRKCabFmzOj1a20H1XiERQIDAQAB";
        AndroidNativeSettings.Instance.base64EncodedPublicKey = base64_id;*/
    }

    void Start()
    {
        AndroidInAppPurchaseManager.ActionProductPurchased += OnProductPurchased;
        AndroidInAppPurchaseManager.ActionProductConsumed += OnProductConsumed;
        AndroidInAppPurchaseManager.ActionBillingSetupFinished += OnBillingConnected;

        AndroidInAppPurchaseManager.Client.Connect();
        //AndroidInAppPurchaseManager.Client.RetrieveProducDetails();
    }

    private static void OnBillingConnected(BillingResult result)
    {
        AndroidInAppPurchaseManager.ActionBillingSetupFinished -= OnBillingConnected;

        if (result.IsSuccess)
        {
            Debug.Log("Connected");
            AndroidInAppPurchaseManager.ActionRetrieveProducsFinished += OnRetrieveProductsFinished;
            AndroidInAppPurchaseManager.Client.RetrieveProducDetails();
        }
        else
            Debug.Log("Connection failed");
    }

    private static void OnRetrieveProductsFinished(BillingResult result)
    {
        AndroidInAppPurchaseManager.ActionRetrieveProducsFinished -= OnRetrieveProductsFinished;

        if (result.IsSuccess)
        {
            if (AndroidInAppPurchaseManager.Client.Inventory.IsProductPurchased(SKU_MoneyPack_1))
                AndroidInAppPurchaseManager.Client.Consume(SKU_MoneyPack_1);
            else if (AndroidInAppPurchaseManager.Client.Inventory.IsProductPurchased(SKU_MoneyPack_2))
                AndroidInAppPurchaseManager.Client.Consume(SKU_MoneyPack_2);
            else if (AndroidInAppPurchaseManager.Client.Inventory.IsProductPurchased(SKU_MoneyPack_3))
                AndroidInAppPurchaseManager.Client.Consume(SKU_MoneyPack_3);
            else if (AndroidInAppPurchaseManager.Client.Inventory.IsProductPurchased(SKU_NoAds))
                PlayerPrefs.SetInt("NoAds", 1);
            else if (AndroidInAppPurchaseManager.Client.Inventory.IsProductPurchased(SKU_UnlimFreeze))
                PlayerPrefs.SetInt("UnlimFreeze", 1);
        }
    }

    private static void OnProcessingPurchasedProduct (GooglePurchaseTemplate purchase)
    {
        if (purchase.SKU == SKU_NoAds)
        {
            PlayerPrefs.SetInt("NoAds", 1);
            Debug.Log("NoAds = 1");   
        }
        else if (purchase.SKU == SKU_UnlimFreeze)
        {
            PlayerPrefs.SetInt("UnlimFreeze", 1);

            Debug.Log("UnlimFreeze = 1");    
        }
        else if (purchase.SKU == SKU_MoneyPack_1)
            AndroidInAppPurchaseManager.Client.Consume(SKU_MoneyPack_1);
        else if (purchase.SKU == SKU_MoneyPack_2)
            AndroidInAppPurchaseManager.Client.Consume(SKU_MoneyPack_2);
        else if (purchase.SKU == SKU_MoneyPack_3)
            AndroidInAppPurchaseManager.Client.Consume(SKU_MoneyPack_3);
    }

    private static void OnProcessingConsumeProduct (GooglePurchaseTemplate purchase)
    {
        if (purchase.SKU == SKU_MoneyPack_1)
        {
            FindObjectOfType<AllPrefsScript>().moneyCount += 500000;
            FindObjectOfType<AllPrefsScript>().setMoney = true;
        }
        else if (purchase.SKU == SKU_MoneyPack_2)
        {
            FindObjectOfType<AllPrefsScript>().moneyCount += 2000000;
            FindObjectOfType<AllPrefsScript>().setMoney = true;
        }
        else if (purchase.SKU == SKU_MoneyPack_3)
        {
            FindObjectOfType<AllPrefsScript>().moneyCount += 10000000;
            FindObjectOfType<AllPrefsScript>().setMoney = true;
        }
    }

    public void Purchase_NoAds()
    {
        AndroidInAppPurchaseManager.Client.Purchase(SKU_NoAds);
    }

    public void Purchase_UnlimFreeze()
    {
        AndroidInAppPurchaseManager.Client.Purchase(SKU_UnlimFreeze);
    }
        
    public void Purchase_MoneyPack_1()
    {
        AndroidInAppPurchaseManager.Client.Purchase("money_pack_1_");
    }

    public void Purchase_MoneyPack_2()
    {
        AndroidInAppPurchaseManager.Client.Purchase("money_pack_2_");
    }

    public void Purchase_MoneyPack_3()
    {
        AndroidInAppPurchaseManager.Client.Purchase("money_pack_3");
    }

    private static void OnProductPurchased(BillingResult result)
    {
        if (result.IsSuccess)
        {
            OnProcessingPurchasedProduct(result.Purchase);

            AndroidMessage.Create(
                "Purchase", 
                "Successfull purchase!", 
                AndroidDialogTheme.ThemeHoloLight);   
        }
    }

    private static void OnProductConsumed(BillingResult result)
    {
        if (result.IsSuccess)
        {
            OnProcessingConsumeProduct(result.Purchase);
            Debug.Log("Successfull consume!");
        }
    }
}
