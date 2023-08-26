using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class ShopScript : MonoBehaviour, IStoreListener
{

    public GameObject gameManagerObj;
    GameManager gameManager;

    public ConsumableItem cItem;

    IStoreController m_StoreController;

    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        gameManager = gameManagerObj.GetComponent<GameManager>();
        SetupBuilder();
    }

    [Obsolete]
    public void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(cItem.id, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void Consumable_Btn_Pressed()
    {
        m_StoreController.InitiatePurchase(cItem.id);
        //gameManager.AddGems(10);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;
        print("Purchase Complete: " + product.definition.id);
        
        if(product.definition.id == cItem.id)
        {
            gameManager.AddGems(10);
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        print("Initialize Faild: " + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        print("Initialize Faild: " + error + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        print("Purchase Failed" + failureReason);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        print("Success");
        m_StoreController = controller;
    }
}

[Serializable]
public class ConsumableItem
{
    public string Name;
    public string id;
    public string desc;
    public float price;
}
