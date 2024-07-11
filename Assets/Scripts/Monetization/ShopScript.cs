using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class ShopScript : MonoBehaviour, IDetailedStoreListener
{

    public GameObject gameManagerObj;
    public GameObject x2MultBtn;
    public GameObject x12MultBtn;
    GameManager gameManager;

    public ConsumableItem cItem;
    public ConsumableItem gems_10;
    public ConsumableItem gems_55;
    public ConsumableItem gems_115;
    public ConsumableItem gems_250;
    public ConsumableItem gems_650;
    public ConsumableItem gems_1500;

    public TMP_Text debugText;
    public TMP_Text warpTxt1Day;
    public TMP_Text warpTxt7Day;
    public TMP_Text warpTxt14Day;

    //public int warpPrice1Day;
    //public int warpPrice7Day;
    //public int warpPrice14Day;
    public int warpPrice6Hours;
    public int warpPrice12Hours;
    public int warpPrice24Hours;
    public int x2MultPrice;
    public int x12MultPrice;

    private double warpReward1Day;
    private double warpReward7Day;
    private double warpReward14Day;

    private double warpReward6Hours;
    private double warpReward12Hours;
    private double warpReward24Hours;

    IStoreController m_StoreController;

    // Start is called before the first frame update
    void Start()
    {
        addDebugLine("Start! 1");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        addDebugLine("Start! 2");
        FillItemData();
        addDebugLine("Start! 3");
        SetupBuilder();
        addDebugLine("Start! 4");
        CalculateWarpRewards();

        

    }

    void Update()
    {
        if (gameManager.x2MultUnlocked)
        {
            x2MultBtn.SetActive(false);
        }
        else
        {
            x2MultBtn.SetActive(true);
        }

        if (gameManager.x12MultUnlocked)
        {
            x12MultBtn.SetActive(false);
        }
        else
        {
            x12MultBtn.SetActive(true);
        }
    }

    public void CalculateWarpRewards()
    {
        gameManager.CalculateOfflineGains(true);
        double offlineGains = gameManager.offlineMoneyGains;
        Debug.Log("offlineGains: " + offlineGains);
        //warpReward1Day = offlineGains;
        //warpReward7Day = offlineGains * 7;
        //warpReward14Day = offlineGains * 14;
        warpReward6Hours = offlineGains / 4;
        warpReward12Hours = warpReward6Hours * 2;
        warpReward24Hours = warpReward12Hours * 2;
        warpTxt1Day.text = $"Get 6 hours worth of profit instantly! \n ({GlobalFunctions.FormatNumber(warpReward6Hours, true)})";
        warpTxt7Day.text = $"Get 12 hours worth of profit instantly! \n ({GlobalFunctions.FormatNumber(warpReward12Hours, true)})";
        warpTxt14Day.text = $"Get 24 hours worth of profit instantly! \n ({GlobalFunctions.FormatNumber(warpReward24Hours, true)})";
    }

    public void SetupBuilder()
    {
        addDebugLine("SetupBuilder! 1");
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(cItem.id, ProductType.Consumable);
        builder.AddProduct(gems_10.id, ProductType.Consumable);
        builder.AddProduct(gems_55.id, ProductType.Consumable);
        builder.AddProduct(gems_115.id, ProductType.Consumable);
        builder.AddProduct(gems_250.id, ProductType.Consumable);
        builder.AddProduct(gems_650.id, ProductType.Consumable);
        builder.AddProduct(gems_1500.id, ProductType.Consumable);

        addDebugLine("SetupBuilder! 2");
        UnityPurchasing.Initialize(this, builder);
        addDebugLine("SetupBuilder! 3");
    }

    public void Consumable_Btn_Pressed()
    {
        addDebugLine("Consumable_Btn_Pressed! 1");
        m_StoreController.InitiatePurchase(cItem.id);
        addDebugLine("Consumable_Btn_Pressed! 2");
        //gameManager.AddGems(10);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        addDebugLine("ProcessPurchase! 1");
        var product = purchaseEvent.purchasedProduct;
        print("Purchase Complete: " + product.definition.id);
        addDebugLine("Purchase Complete: " + product.definition.id);

        if (product.definition.id == gems_10.id)
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

        addDebugLine("ProcessPurchase! 2");
        return PurchaseProcessingResult.Complete;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        addDebugLine("Initialize Failed: " + error);
        print("Initialize Failed: " + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        addDebugLine("Initialize Failed: " + error + message);
        print("Initialize Failed: " + error + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        addDebugLine("Purchase Failed" + failureReason);
        print("Purchase Failed" + failureReason);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        addDebugLine("Success");
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
        gems_1500 = new ConsumableItem("1500 Gems", "1500_gems", "Get 1500 Gems", 99.99f);
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

    public void Buy6HourWarp()
    {
        if(gameManager.gems >= warpPrice6Hours)
        {
            gameManager.AddMoney(warpReward6Hours);
            gameManager.SpendGems(warpPrice6Hours);
        }
    }
    public void Buy12HourWarp()
    {
        if (gameManager.gems >= warpPrice12Hours)
        {
            gameManager.AddMoney(warpReward12Hours);
            gameManager.SpendGems(warpPrice12Hours);
        }
    }
    public void Buy24HourWarp()
    {
        if (gameManager.gems >= warpPrice24Hours)
        {
            gameManager.AddMoney(warpReward24Hours);
            gameManager.SpendGems(warpPrice24Hours);
        }
    }

    public void BuyX2Multiplier()
    {
        if(gameManager.gems >= x2MultPrice)
        {
            gameManager.SpendGems(x2MultPrice);
            gameManager.ActivateX2Mult();
            x2MultBtn.SetActive(false);
        }
    }

    public void BuyX12Multiplier()
    {
        if (gameManager.gems >= x12MultPrice)
        {
            gameManager.SpendGems(x12MultPrice);
            gameManager.ActivateX12Mult();
            x12MultBtn.SetActive(false);
        }
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

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        addDebugLine("Purchase Failed" + failureDescription);
        throw new NotImplementedException();
    }

    public void addDebugLine(string line)
    {
        debugText.text = debugText.text + line + '\n';
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
