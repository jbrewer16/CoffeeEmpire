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
    public ConsumableItem gems_10;
    public ConsumableItem gems_55;
    public ConsumableItem gems_25;
    public ConsumableItem gems_115;
    public ConsumableItem gems_250;
    public ConsumableItem gems_650;
    public ConsumableItem gems_1500;

    IStoreController m_StoreController;

    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        gameManager = gameManagerObj.GetComponent<GameManager>();
        FillItemData();
        SetupBuilder();
    }

    [Obsolete]
    public void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(cItem.id, ProductType.Consumable);
        builder.AddProduct(gems_10.id, ProductType.Consumable);
        builder.AddProduct(gems_55.id, ProductType.Consumable);
        builder.AddProduct(gems_115.id, ProductType.Consumable);
        builder.AddProduct(gems_250.id, ProductType.Consumable);
        builder.AddProduct(gems_650.id, ProductType.Consumable);
        builder.AddProduct(gems_1500.id, ProductType.Consumable);


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
        
        if(product.definition.id == gems_10.id)
        {
            gameManager.AddGems(10);
        } else if(product.definition.id == gems_55.id)
        {
            gameManager.AddGems(55);
        } else if (product.definition.id == gems_115.id)
        {
            gameManager.AddGems(115);
        } else if (product.definition.id == gems_250.id)
        {
            gameManager.AddGems(250);
        } else if (product.definition.id == gems_650.id)
        {
            gameManager.AddGems(650);
        } else if (product.definition.id == gems_1500.id)
        {
            gameManager.AddGems(1500);
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

    public void FillItemData()
    {
        gems_10 = new ConsumableItem("10 Gems", "10_gems", "Get 10 Gems", 0.99f);
        gems_55 = new ConsumableItem("55 Gems", "55_gems", "Get 55 Gems", 4.99f);
        gems_115 = new ConsumableItem("115 Gems", "115_gems", "Get 115 Gems", 9.99f);
        gems_250 = new ConsumableItem("250 Gems", "250_gems", "Get 250 Gems", 19.99f);
        gems_650 = new ConsumableItem("650 Gems", "650_gems", "Get 650 Gems", 49.99f);
        gems_1500 = new ConsumableItem("1500 Gems", "1500_gems", "Get 1500 Gems", 0.99f);
    }

    public void Buy_10_Gems()
    {
        m_StoreController.InitiatePurchase(gems_10.id);
    }
    public void Buy_55_Gems()
    {
        m_StoreController.InitiatePurchase(gems_55.id);
    }
    public void Buy_115_Gems()
    {
        m_StoreController.InitiatePurchase(gems_115.id);
    }
    public void Buy_250_Gems()
    {
        m_StoreController.InitiatePurchase(gems_250.id);
    }
    public void Buy_650_Gems()
    {
        m_StoreController.InitiatePurchase(gems_650.id);
    }
    public void Buy_1500_Gems()
    {
        m_StoreController.InitiatePurchase(gems_1500.id);
    }

    // TODO - Remove when IAP is fully developed
    public void add10Gems()
    {
        gameManager.AddGems(10);
    }
    public void add55Gems()
    {
        gameManager.AddGems(55);
    }
    public void add115Gems()
    {
        gameManager.AddGems(115);
    }
    public void add250Gems()
    {
        gameManager.AddGems(250);
    }
    public void add650Gems()
    {
        gameManager.AddGems(650);
    }
    public void add1500Gems()
    {
        gameManager.AddGems(1500);
    }

}

[Serializable]
public class ConsumableItem
{
    public string Name;
    public string id;
    public string desc;
    public float price;

    public ConsumableItem(string n, string i, string d, float p)
    {
        Name = n;
        id = i;
        desc = d;
        price = p;
    }
}
