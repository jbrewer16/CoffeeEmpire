using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class CoffeeManager : MonoBehaviour
{

    public GameObject gameManagerObj;
    public Slider timerSlider;
    public TMP_Text timerTxt;
    public TMP_Text brewSpeedPriceTxt;
    public TMP_Text brewCapacityPriceTxt;
    public TMP_Text capacityTxt;

    public float brewSpeed;
    public float brewSpeedUpgPrice;
    public float brewCapacityUpgPrice;
    public float timerReduceAmount;
    public int brewCapacity;
    public int brewCapacityUpgrades;
    public int sellPrice;

    private GameManager gameManager;

    private float timer;
    private int beanPerCup = 16;
    private int tempCap = 1;

    // Start is called before the first frame update
    void Start()
    {
        brewSpeed = 10;
        brewSpeedUpgPrice = 1000;
        brewCapacityUpgPrice = 50;
        brewCapacity = 1;
        brewCapacityUpgrades = 1;
        timerReduceAmount = 0.25f;
        gameManager = gameManagerObj.GetComponent<GameManager>();
        timer = brewSpeed;
        timerTxt.text = timer + "s";
        capacityTxt.text = brewCapacity + " Cup";
        brewSpeedPriceTxt.text = "$" + brewSpeedUpgPrice;
        brewCapacityPriceTxt.text = "$" + brewCapacityUpgPrice;
        timerSlider.maxValue = timer;
        timerSlider.value = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.beanCnt >= (beanPerCup - gameManager.beanDensity))
        {
            timer -= Time.deltaTime;
            timerSlider.value = timer;
            if (timer <= 0f)
            {
                brewCoffee();
                ResetTimer();
            }
        }
        UpdateText();
    }

    public void UpdateText()
    {
        timerTxt.text = timer + "s";
        capacityTxt.text = brewCapacity + " Cup";
        brewSpeedPriceTxt.text = "$" + (brewSpeedUpgPrice - (brewSpeedUpgPrice * gameManager.brewTimeCostReducer));
        brewCapacityPriceTxt.text = "$" + brewCapacityUpgPrice;
    }

    private void ResetTimer()
    {
        timer = brewSpeed;
    }

    private void brewCoffee()
    {
        if(gameManager.beanCnt >= ((beanPerCup - gameManager.beanDensity) * brewCapacity))
        {
            gameManager.beanCnt -= ((beanPerCup - gameManager.beanDensity) * brewCapacity);
            gameManager.coffee += brewCapacity;
        } else
        {
            int cupsToBrew = gameManager.beanCnt / (beanPerCup - gameManager.beanDensity);
            gameManager.beanCnt -= cupsToBrew * (beanPerCup - gameManager.beanDensity);
            gameManager.coffee += cupsToBrew;
        }
    }

    public void upgradeBrewTime()
    {
        if(gameManager.money >= (brewSpeedUpgPrice - (brewSpeedUpgPrice * gameManager.brewTimeCostReducer)))
        {
            gameManager.SpendMoney((brewSpeedUpgPrice - (brewSpeedUpgPrice * gameManager.brewTimeCostReducer)));
            brewSpeed -= 0.15f;
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
        //timer -= (float)(timer * 0.15);
        timer -= timerReduceAmount;
    }

    private float CalculateBrewCapacity()
    {
        float percentageIncrease = 0.1f; // 3% increase
        int newCapacity = brewCapacity + (int)Math.Ceiling(brewCapacity * percentageIncrease);
        int increment = newCapacity - brewCapacity;
        brewCapacity += increment < 1 ? 1 : increment;
        return brewCapacity;
    }

    private float CalculateCapacityUpgPrice()
    {
        float percentageIncrease = 0.1f;
        float newPrice = brewCapacityUpgPrice + (brewCapacityUpgPrice * percentageIncrease);
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
