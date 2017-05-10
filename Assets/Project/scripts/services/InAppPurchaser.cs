using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class InAppPurchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    public static string IDNonConsumableNoAds = "noads";
    public static string IDConsumableCoins1000 = "coins1000";
    public static string IDConsumableCoins2000 = "coins2000";
    public static string IDConsumableCoins4000noads = "coins4000noads";
    //public static string IDConsumableCoins1000 = "coins1000";
    //public static string IDConsumableCoins10000 = "coins10000";


    void Start()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {

        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //builder.AddProduct(kProductIDConsumable, ProductType.Consumable);
        builder.AddProduct(IDNonConsumableNoAds, ProductType.NonConsumable);
        builder.AddProduct(IDConsumableCoins1000, ProductType.Consumable);
        builder.AddProduct(IDConsumableCoins2000, ProductType.Consumable);
        builder.AddProduct(IDConsumableCoins4000noads, ProductType.NonConsumable);
        //builder.AddProduct(IDConsumableCoins1000, ProductType.Consumable);
        //builder.AddProduct(IDConsumableCoins10000, ProductType.Consumable);

        //builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
        //    { kProductNameAppleSubscription, AppleAppStore.Name },
        //    { kProductNameGooglePlaySubscription, GooglePlay.Name },
        //});

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    //public void BuyConsumable()
    //{
    //    BuyProductID(kProductIDConsumable);
    //}


    public void BuyNoAds()
    {
        if (PlayerPrefs.GetInt("NoAds") != 1)
        {
            BuyProductID(IDNonConsumableNoAds);
        }
    }

    public void Buy1000Coins()
    {
        BuyProductID(IDConsumableCoins1000);
    }

    public void Buy2000Coins()
    {
        BuyProductID(IDConsumableCoins2000);
    }

    public void Buy4000Coinsnoads()
    {
        BuyProductID(IDConsumableCoins4000noads);
    }


    //public void BuySubscription()
    //{
    //    BuyProductID(kProductIDSubscription);
    //}


    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));

                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<UnityEngine.Purchasing.IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) =>
            {
                if (result)
                {
                    InApp.NoAds();
                }
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }
    
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, IDNonConsumableNoAds, StringComparison.Ordinal))
        {
            InApp.NoAds();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, IDConsumableCoins1000, StringComparison.Ordinal))
        {
            InApp.BuyCoins(1000);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, IDConsumableCoins2000, StringComparison.Ordinal))
        {
            InApp.BuyCoins(2000);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, IDConsumableCoins4000noads, StringComparison.Ordinal))
        {
            InApp.BuyCoins(4000);
            InApp.NoAds();
        }
        //else if (String.Equals(args.purchasedProduct.definition.id, IDConsumableCoins1000, StringComparison.Ordinal))
        //{
        //    InApp.BuyCoins(1000);
        //}
        //else if (String.Equals(args.purchasedProduct.definition.id, IDConsumableCoins10000, StringComparison.Ordinal))
        //{
        //    InApp.BuyCoins(10000);
        //}
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}