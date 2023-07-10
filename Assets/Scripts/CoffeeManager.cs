using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CoffeeManager : MonoBehaviour
{

    public GameObject gameManagerObj;
    public TMP_Text timerTxt;
    public TMP_Text brewSpeedPriceTxt;
    public TMP_Text brewCapacityPriceTxt;
    public TMP_Text capacityTxt;

    public float brewSpeed;
    public float brewSpeedUpgPrice;
    public int brewCapacity;
    public float brewCapacityUpgPrice;
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
        gameManager = gameManagerObj.GetComponent<GameManager>();
        timer = brewSpeed;
        timerTxt.text = timer + "s";
        capacityTxt.text = brewCapacity + " Cup";
        brewSpeedPriceTxt.text = "$" + brewSpeedUpgPrice;
        brewCapacityPriceTxt.text = "$" + brewCapacityUpgPrice;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.beanCnt >= beanPerCup)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                brewCoffee();
                ResetTimer();
            }

            UpdateText();
        }
    }

    public void UpdateText()
    {
        timerTxt.text = timer + "s";
        capacityTxt.text = brewCapacity + " Cup";
        brewSpeedPriceTxt.text = "$" + brewSpeedUpgPrice;
        brewCapacityPriceTxt.text = "$" + brewCapacityUpgPrice;
    }

    private void ResetTimer()
    {
        timer = brewSpeed;
    }

    private void brewCoffee()
    {
        if(gameManager.beanCnt >= (beanPerCup * brewCapacity))
        {
            gameManager.beanCnt -= (beanPerCup * brewCapacity);
            gameManager.coffee += brewCapacity;
        } else
        {
            int cupsToBrew = gameManager.beanCnt / beanPerCup;
            gameManager.beanCnt -= cupsToBrew * beanPerCup;
            gameManager.coffee += cupsToBrew;
        }
    }

    public void upgradeBrewTime()
    {
        if(gameManager.money >= brewSpeedUpgPrice)
        {
            gameManager.SpendMoney(brewSpeedUpgPrice);
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
        timer -= (float)(timer * 0.15);
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
