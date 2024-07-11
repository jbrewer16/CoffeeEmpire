using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CustomerManager : MonoBehaviour
{
    public GameObject gameManagerObj;
    public Animator customerAnimator;
    public Slider sellTimer;
    public TMP_Text timerTxt;
    public TMP_Text serveSpeedPriceTxt;
    public TMP_Text custCapacityPriceTxt;
    public TMP_Text custCapacityTxt;

    public float originalServeSpeed;
    public float serveSpeed;
    public float serveSpeedUpgPrice;
    public float custCapacityUpgPrice;
    public float timerReduceAmount;
    public int custCapacity;
    public int custCapacityUpgrades;
    public int sellPrice;

    public bool maxSellSpeedUnlocked;

    private GameManager gameManager;

    private float timer;
    private int beanPerCup = 16;
    private int tempCap = 1;

    private bool isSpeedBoosted = false;
    private float speedBoostMultiplier = 2.0f;
    private float boostDuration = 0.25f;
    private float currentBoostTimer;

    // Start is called before the first frame update
    void Start()
    {
        sellPrice = 5;
        originalServeSpeed = 10;
        //serveSpeed = 10;
        //serveSpeedUpgPrice = 1000;
        //custCapacityUpgPrice = 50;
        //custCapacity = 1;
        //CalculateCustCapacity();
        //custCapacityUpgrades = 1;
        //timerReduceAmount = 0.25f;
        gameManager = gameManagerObj.GetComponent<GameManager>();
        timer = serveSpeed;
        timerTxt.text = timer + "s";
        custCapacityTxt.text = custCapacity + " Cup";
        serveSpeedPriceTxt.text = GlobalFunctions.FormatNumber(serveSpeedUpgPrice - (serveSpeedUpgPrice * gameManager.sellTimeCostReducer), true);//"$" + (serveSpeedUpgPrice - (serveSpeedUpgPrice * gameManager.sellTimeCostReducer));
        custCapacityPriceTxt.text = GlobalFunctions.FormatNumber(custCapacityUpgPrice, true);//"$" + custCapacityUpgPrice;
        sellTimer.maxValue = timer;
        sellTimer.value = timer;
    }

    // Update is called once per frame
    void Update()
    {
        sellTimer.maxValue = serveSpeed;
        //float currentSpeedMultiplier = isSpeedBoosted ? (speedBoostMultiplier + gameManager.inv_speedPerTap) : 1;

        if (serveSpeed <= 0.25f)
        {
            maxSellSpeedUnlocked = true;
        }
        else
        {
            maxSellSpeedUnlocked = false;
        }

        if (gameManager.coffee >= 1)
        {
            timer -= Time.deltaTime * GetCurrentSpeedMultiplier();
            sellTimer.value = timer;

            if (timer <= 0f)
            {
                sellCoffee();
                ResetTimer();
            }
            customerAnimator.speed = CalculateAnimationSpeed();
        }
        else
        {
            customerAnimator.Play("CustomerAnimation", 0, 0);
            customerAnimator.speed = 0;
        }
        UpdateText();
        UpdateBoostTimer();
    }

    public void UpdateText()
    {
        double finalCustCapacity = 0;
        if (gameManager.doubleCustCapacityActive)
        {
            finalCustCapacity = custCapacity * 2;
        }
        else
        {
            finalCustCapacity = custCapacity;
        }

        timerTxt.text = ""; // timer.ToString("0.00") + "s";
        //custCapacityTxt.text = finalCustCapacity + " Cup";
        custCapacityTxt.text = GlobalFunctions.FormatNumber(finalCustCapacity) + (finalCustCapacity > 1 ? " Cups" : " Cup");
        if (maxSellSpeedUnlocked)
        {
            serveSpeedPriceTxt.text = "MAX";
        }
        else
        {
            serveSpeedPriceTxt.text = GlobalFunctions.FormatNumber(serveSpeedUpgPrice - (serveSpeedUpgPrice * gameManager.sellTimeCostReducer), true);
        }
        //serveSpeedPriceTxt.text = GlobalFunctions.FormatNumber(serveSpeedUpgPrice - (serveSpeedUpgPrice * gameManager.sellTimeCostReducer), true);//"$" + (serveSpeedUpgPrice - (serveSpeedUpgPrice * gameManager.sellTimeCostReducer));
        custCapacityPriceTxt.text = GlobalFunctions.FormatNumber(custCapacityUpgPrice, true);//"$" + custCapacityUpgPrice;
    }
    
    void UpdateBoostTimer()
    {
        if (isSpeedBoosted)
        {
            currentBoostTimer -= Time.deltaTime;
            if (currentBoostTimer <= 0)
            {
                isSpeedBoosted = false;
            }
        }
    }

    float GetCurrentSpeedMultiplier()
    {
        return isSpeedBoosted ? (speedBoostMultiplier + gameManager.inv_speedPerTap) : 1;
    }

    float CalculateAnimationSpeed()
    {
        // Calculate normal animation speed to match the base timer duration
        float baseSpeed = originalServeSpeed / serveSpeed;
        return baseSpeed * GetCurrentSpeedMultiplier();
    }

    private void ResetTimer()
    {
        timer = serveSpeed;
        sellTimer.value = timer;
    }

    private void sellCoffee()
    {

        double finalCustCapacity = 0;
        if (gameManager.doubleCustCapacityActive)
        {
            finalCustCapacity = custCapacity * 2;
        }
        else
        {
            finalCustCapacity = custCapacity;
        }

        int freeChance = UnityEngine.Random.Range(0, 100);
        bool freeUpg = freeChance < gameManager.inv_freeSellChance;

        int mult = 1;
        if (gameManager.x2MultUnlocked && gameManager.x12MultUnlocked)
        {
            mult = 24;
        }
        else if (gameManager.x12MultUnlocked)
        {
            mult = 12;
        }
        else if (gameManager.x2MultUnlocked)
        {
            mult = 2;
        }

        if (gameManager.coffee >= finalCustCapacity)
        {
            //gameManager.money += (sellPrice + gameManager.coffeeSellPriceInc) * custCapacity;
            double profit = (GetSellPrice() * finalCustCapacity) * mult;
            gameManager.AddMoney(profit);
            if(!freeUpg) gameManager.coffee -= finalCustCapacity;
        } else if(gameManager.coffee > 0)
        {
            //gameManager.money += (sellPrice + gameManager.coffeeSellPriceInc) * gameManager.coffee;
            double profit = (GetSellPrice() * gameManager.coffee) * mult;
            gameManager.AddMoney(profit);
            if (!freeUpg) gameManager.coffee -= gameManager.coffee;
        }

        //if (gameManager.coffee >= custCapacity)
        //{
        //    gameManager.coffee -= custCapacity;
        //    if(gameManager.investors != 0)
        //    {
        //        gameManager.money += ((sellPrice + gameManager.coffeeSellPriceInc) + ((sellPrice + gameManager.coffeeSellPriceInc) * (gameManager.investors * gameManager.investorEffectiveness))) * custCapacity;
        //    } else
        //    {
        //        gameManager.money += (sellPrice + gameManager.coffeeSellPriceInc) * custCapacity;
        //    }
            
        //}
    }

    public int GetSellPrice()
    {
        return (sellPrice + gameManager.coffeeSellPriceInc + gameManager.inv_profitBonus);
    }

    public void upgradeServeSpeed()
    {
         if(serveSpeed - 0.15f <= 0.25)
        {
            serveSpeed = 0.25f;
            maxSellSpeedUnlocked = true;
        } else
        {
            if (gameManager.money >= (serveSpeedUpgPrice - (serveSpeedUpgPrice * gameManager.sellTimeCostReducer)))
            {
                gameManager.SpendMoney((serveSpeedUpgPrice - (serveSpeedUpgPrice * gameManager.sellTimeCostReducer)));
                serveSpeed -= 0.15f;
                sellTimer.maxValue = serveSpeed;
                CalculateSpeedUpgPrice();
                UpdateText();
            }
        }
    }

    public void upgradeCustCapacity()
    {
        if (gameManager.money >= custCapacityUpgPrice)
        {
            gameManager.SpendMoney(custCapacityUpgPrice);
            custCapacityUpgrades++;
            CalculateCustCapacity();
            CalculateCapacityUpgPrice();
            UpdateText();
        }
    }

    public void reduceSellTimer()
    {
        if (gameManager.coffee >= 1)
        {
            if (!isSpeedBoosted)
            {
                currentBoostTimer = boostDuration;
                isSpeedBoosted = true;
            }
            //timer -= (timerReduceAmount + gameManager.inv_speedPerTap);
        }
        //timer -= (float)(timer * 0.15);
    }

    public void resetCustomer()
    {
        sellPrice = 5;
        serveSpeed = 10;
        serveSpeedUpgPrice = 1000;
        custCapacityUpgPrice = 50;
        custCapacity = 1;
        custCapacityUpgrades = 1;
        timerReduceAmount = 0.25f;
    }

    private float CalculateCustCapacity()
    {
        float percentageIncrease = 0.1f;
        int newCapacity = custCapacity + (int)Math.Ceiling(custCapacity * percentageIncrease);
        int increment = newCapacity - custCapacity;
        custCapacity += increment < 1 ? 1 : increment;
        return custCapacity;
    }

    private float CalculateCapacityUpgPrice()
    {
        float percentageIncrease = 0.15f;
        float newPrice = custCapacityUpgPrice + (custCapacityUpgPrice * percentageIncrease);
        custCapacityUpgPrice = newPrice;
        return custCapacityUpgPrice;
    }
    private float CalculateSpeedUpgPrice()
    {
        float percentageIncrease = 1.5f;
        float newPrice = serveSpeedUpgPrice + (serveSpeedUpgPrice * percentageIncrease);
        serveSpeedUpgPrice = newPrice;
        return serveSpeedUpgPrice;
    }
}
