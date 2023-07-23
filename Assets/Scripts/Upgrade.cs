using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{

    public int cost;
    public string desc;
    public float upgradeCostMultiplier;
    //public string beanName;
    //public TMP_Text beanToUpgTxt;
    public TMP_Text upgNameTxt;
    public TMP_Text upgDescTxt;
    public TMP_Text btnTxt;
    //public Bean bean;
    public GameObject gameManagerObj;

    public UpgOptions upgOptions = new UpgOptions();

    private GameManager gameManager;
    private int currentUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        btnTxt.text = "Upgrade \n $" + cost;
        gameManager = gameManagerObj.GetComponent<GameManager>();
        currentUpgrade = 1;
        updateText();

    }

    public void Update()
    {

        updateText();
        btnTxt.text = "Upgrade \n $" + CalculateUpgradeCost();

    }

    public void updateText()
    {
        switch (upgOptions)
        {
            case UpgOptions.growthMult:
                upgDescTxt.text = "x" + gameManager.growthRateMultiplier + "\n" + desc;
                break;
            case UpgOptions.growthCost:
                upgDescTxt.text = "- " + (gameManager.growthCostReducer * 100) + "%\n" + desc;
                break;
            case UpgOptions.beanDensity:
                upgDescTxt.text = "- " + gameManager.beanDensity + "\n" + desc;
                break;
            case UpgOptions.brewTime:
                upgDescTxt.text = "- " + (gameManager.brewTimeCostReducer * 100) + "%\n" + desc;
                break;
            case UpgOptions.sellTime:
                upgDescTxt.text = "- " + (gameManager.sellTimeCostReducer * 100) + "%\n" + desc;
                break;
            case UpgOptions.coffeeSellPrice:
                upgDescTxt.text = "$" + (5 + gameManager.coffeeSellPriceInc) + "\n" + desc;
                break;
            default:
                break;
        }
    }

    public void buyUpgrade()
    {
        float upgCost = (int)(cost + (cost * Mathf.Pow(upgradeCostMultiplier, (currentUpgrade - 1))));
        //if (gameManager.money >= upgCost)
        {
            gameManager.SpendMoney(upgCost);
            currentUpgrade++;
            switch (upgOptions)
            {
                case UpgOptions.growthMult:
                    gameManager.AddGrowthRateMultiplier(0.05f);
                    break;
                case UpgOptions.growthCost:
                    gameManager.AddGrowthCostReducer(0.01f);
                    break;
                case UpgOptions.beanDensity:
                    gameManager.AddBeanDensity(1);
                    break;
                case UpgOptions.brewTime:
                    gameManager.ReduceBrewTimeCost(0.01f);
                    break;
                case UpgOptions.sellTime:
                    gameManager.ReduceSellTimeCost(0.01f);
                    break;
                case UpgOptions.coffeeSellPrice:
                    gameManager.AddCoffeeSellPrice(1);
                    break;
                default:
                    break;
            }

            btnTxt.text = "Upgrade \n $" + cost;
        }
    }

    private float CalculateUpgradeCost()
    {
        // Implement your upgrade cost calculation logic here
        // Example: Upgrade cost doubles for each upgrade
        return cost + (cost * Mathf.Pow(upgradeCostMultiplier, (currentUpgrade - 1)));
    }

}

public enum UpgOptions
{
    growthMult,
    growthCost,
    beanDensity,
    brewTime,
    sellTime,
    coffeeSellPrice
}
