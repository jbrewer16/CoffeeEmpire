using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CustomerManager : MonoBehaviour
{
    public GameObject gameManagerObj;
    public TMP_Text timerTxt;
    public TMP_Text serveSpeedPriceTxt;
    public TMP_Text custCapacityPriceTxt;
    public TMP_Text custCapacityTxt;

    public float serveSpeed;
    public float serveSpeedUpgPrice;
    public int custCapacity;
    public float custCapacityUpgPrice;
    public int custCapacityUpgrades;
    public int sellPrice;

    private GameManager gameManager;

    private float timer;
    private int beanPerCup = 16;
    private int tempCap = 1;

    // Start is called before the first frame update
    void Start()
    {
        sellPrice = 5;
        serveSpeed = 10;
        serveSpeedUpgPrice = 1000;
        custCapacityUpgPrice = 50;
        custCapacity = 1;
        custCapacityUpgrades = 1;
        gameManager = gameManagerObj.GetComponent<GameManager>();
        timer = serveSpeed;
        timerTxt.text = timer + "s";
        custCapacityTxt.text = custCapacity + " Cup";
        serveSpeedPriceTxt.text = "$" + serveSpeedUpgPrice;
        custCapacityPriceTxt.text = "$" + custCapacityUpgPrice;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.coffee >= 1)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                sellCoffee();
                ResetTimer();
            }

            UpdateText();
        }
    }

    public void UpdateText()
    {
        timerTxt.text = timer + "s";
        custCapacityTxt.text = custCapacity + " Cup";
        serveSpeedPriceTxt.text = "$" + serveSpeedUpgPrice;
        custCapacityPriceTxt.text = "$" + custCapacityUpgPrice;
    }

    private void ResetTimer()
    {
        timer = serveSpeed;
    }

    private void sellCoffee()
    {
        if (gameManager.coffee >= custCapacity)
        {
            gameManager.coffee -= custCapacity;
            if(gameManager.investors != 0)
            {
                gameManager.money += (sellPrice + (sellPrice * (gameManager.investors * gameManager.investorEffectiveness))) * custCapacity;
            } else
            {
                gameManager.money += sellPrice * custCapacity;
            }
            
        }
        else
        {
            if (gameManager.investors != 0)
            {
                gameManager.money += (sellPrice + (sellPrice * (gameManager.investors * gameManager.investorEffectiveness))) * custCapacity;
            }
            else
            {
                gameManager.money += sellPrice * custCapacity;
            }
            gameManager.coffee -= gameManager.coffee;
        }
    }

    public void upgradeServeSpeed()
    {
        if (gameManager.money >= serveSpeedUpgPrice)
        {
            gameManager.SpendMoney(serveSpeedUpgPrice);
            serveSpeed -= 0.15f;
            CalculateSpeedUpgPrice();
            UpdateText();
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
        timer -= (float)(timer * 0.15);
    }

    private float CalculateCustCapacity()
    {
        float percentageIncrease = 0.1f; // 3% increase
        int newCapacity = custCapacity + (int)Math.Ceiling(custCapacity * percentageIncrease);
        int increment = newCapacity - custCapacity;
        custCapacity += increment < 1 ? 1 : increment;
        return custCapacity;
    }

    private float CalculateCapacityUpgPrice()
    {
        float percentageIncrease = 0.1f;
        float newPrice = custCapacityUpgPrice + (custCapacityUpgPrice * percentageIncrease);
        custCapacityUpgPrice = newPrice;
        return custCapacityUpgPrice;
    }
    private float CalculateSpeedUpgPrice()
    {
        float percentageIncrease = 1.1f;
        float newPrice = serveSpeedUpgPrice + (serveSpeedUpgPrice * percentageIncrease);
        serveSpeedUpgPrice = newPrice;
        return serveSpeedUpgPrice;
    }
}
