using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class CoffeeManager : MonoBehaviour
{

    public GameObject gameManagerObj;
    public Animator coffeeAnimator;
    public Slider brewTimer;
    public TMP_Text timerTxt;
    public TMP_Text brewSpeedPriceTxt;
    public TMP_Text brewCapacityPriceTxt;
    public TMP_Text capacityTxt;

    public float originalBrewSpeed;
    public float brewSpeed;
    public float brewSpeedUpgPrice;
    public double brewCapacityUpgPrice;
    public float timerReduceAmount;
    public double brewCapacity;
    public int brewCapacityUpgrades;
    public int sellPrice;

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
        originalBrewSpeed = 10;
        //brewSpeed = 10;
        //brewSpeedUpgPrice = 1000;
        //brewCapacityUpgPrice = 50;
        //CalculateBrewCapacity();
        //brewCapacity = 1;
        //brewCapacityUpgrades = 1;
        //timerReduceAmount = 0.25f;
        gameManager = gameManagerObj.GetComponent<GameManager>();
        timer = brewSpeed;
        timerTxt.text = timer + "s";
        capacityTxt.text = GlobalFunctions.FormatNumber(brewCapacity, false) + " Cup";
        brewSpeedPriceTxt.text = GlobalFunctions.FormatNumber(brewSpeedUpgPrice, true);//"$" + brewSpeedUpgPrice;
        brewCapacityPriceTxt.text = GlobalFunctions.FormatNumber(brewCapacityUpgPrice, true);//"$" + brewCapacityUpgPrice;
        brewTimer.maxValue = timer;
        brewTimer.value = timer;
    }

    // Update is called once per frame
    void Update()
    {
        brewTimer.maxValue = brewSpeed;
        //float currentSpeedMultiplier = isSpeedBoosted ? (speedBoostMultiplier + gameManager.inv_speedPerTap) : 1;
        //if (isSpeedBoosted)
        //{
        //    Debug.Log(currentSpeedMultiplier);
        //}

        if (gameManager.beanCnt >= (beanPerCup - gameManager.beanDensity))
        {
            timer -= Time.deltaTime * GetCurrentSpeedMultiplier();
            brewTimer.value = timer;
            if (timer <= 0f)
            {
                brewCoffee();
                ResetTimer();
            }
            //coffeeAnimator.speed = 1;
            coffeeAnimator.speed = CalculateAnimationSpeed();
        } else
        {
            coffeeAnimator.Play("CoffeeAnimation", 0, 0);
            coffeeAnimator.speed = 0;
        }
        UpdateText();
        UpdateBoostTimer();
    }

    //public double CalculateProductionRate()
    //{
    //    double coffeeBrewRatePerMinute
    //    return 0;
    //}
    public void UpdateText()
    {
        timerTxt.text = ""; //timer.ToString("0.00") + "s";
        capacityTxt.text = GlobalFunctions.FormatNumber(brewCapacity) + (brewCapacity > 1 ? " Cups" : " Cup");
        brewSpeedPriceTxt.text = GlobalFunctions.FormatNumber(brewSpeedUpgPrice - (brewSpeedUpgPrice * gameManager.brewTimeCostReducer), true);//"$" + (brewSpeedUpgPrice - (brewSpeedUpgPrice * gameManager.brewTimeCostReducer));
        brewCapacityPriceTxt.text = GlobalFunctions.FormatNumber(brewCapacityUpgPrice, true);//"$" + brewCapacityUpgPrice;
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
        float baseSpeed = originalBrewSpeed / brewSpeed;
        return baseSpeed * GetCurrentSpeedMultiplier();
    }

    private void ResetTimer()
    {
        timer = brewSpeed;
        brewTimer.value = timer;
    }

    private void brewCoffee()
    {
        int freeChance = UnityEngine.Random.Range(0, 100);
        bool freeUpg = freeChance < gameManager.inv_freeBrewChance;
        if (gameManager.beanCnt >= ((beanPerCup - gameManager.beanDensity) * brewCapacity))
        {
            if(!freeUpg) gameManager.beanCnt -= ((beanPerCup - gameManager.beanDensity) * brewCapacity);
            gameManager.coffee += brewCapacity;
        } else
        {
            double cupsToBrew = gameManager.beanCnt / (beanPerCup - gameManager.beanDensity);
            if(!freeUpg) gameManager.beanCnt -= cupsToBrew * (beanPerCup - gameManager.beanDensity);
            gameManager.coffee += cupsToBrew;
        }
    }

    public int GetBeansPerCup()
    {
        return beanPerCup;
    }
    public void upgradeBrewTime()
    {
        if(gameManager.money >= (brewSpeedUpgPrice - (brewSpeedUpgPrice * gameManager.brewTimeCostReducer)))
        {
            gameManager.SpendMoney((brewSpeedUpgPrice - (brewSpeedUpgPrice * gameManager.brewTimeCostReducer)));
            brewSpeed -= 0.15f;
            brewTimer.maxValue = brewSpeed;
            CalculateSpeedUpgPrice();
            UpdateText();
        }
    }

    public void upgradeBrewCapacity()
    {
        if (gameManager.money >= brewCapacityUpgPrice)
        {
            gameManager.SpendMoney(brewCapacityUpgPrice);
            brewCapacityUpgrades++;
            CalculateBrewCapacity();
            CalculateCapacityUpgPrice();
            UpdateText();
        }
    }

    public void reduceBrewTimer()
    {
        if (gameManager.beanCnt >= (beanPerCup - gameManager.beanDensity))
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

    public void resetCoffee()
    {
        brewSpeed = 10;
        brewSpeedUpgPrice = 1000;
        brewCapacityUpgPrice = 50;
        brewCapacity = 1;
        brewCapacityUpgrades = 1;
        timerReduceAmount = 0.25f;
    }

    //private float CalculateBrewCapacity()
    //{
    //    float percentageIncrease = 0.1f;
    //    int newCapacity = brewCapacity + (int)Math.Ceiling(brewCapacity * percentageIncrease);
    //    int increment = newCapacity - brewCapacity;
    //    brewCapacity += increment < 1 ? 1 : increment;
    //    return brewCapacity;
    //}

    private double CalculateBrewCapacity()
    {
        float percentageIncrease = 0.1f;
        double increment = Math.Ceiling(brewCapacity * percentageIncrease);
        brewCapacity += Math.Max(1, increment);
        return brewCapacity;
    }

    private double CalculateCapacityUpgPrice()
    {
        float percentageIncrease = 0.15f;
        double newPrice = brewCapacityUpgPrice + (brewCapacityUpgPrice * percentageIncrease);
        brewCapacityUpgPrice = newPrice;
        return brewCapacityUpgPrice;
    }
    private float CalculateSpeedUpgPrice()
    {
        float percentageIncrease = 1.1f;
        float newPrice = brewSpeedUpgPrice + (brewSpeedUpgPrice * percentageIncrease);
        brewSpeedUpgPrice = newPrice;
        return brewSpeedUpgPrice;
    }

}
    