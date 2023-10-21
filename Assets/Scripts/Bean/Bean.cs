using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bean : MonoBehaviour
{
    public GameObject gameManagerObj;
    public GameObject unlockPanel;
    public Slider timerSlider;
    public Slider beanUpgSlider;
    public TMP_Text growCountText;
    public TMP_Text upgradeCostText;
    public TMP_Text timeToGrowText;
    public TMP_Text upgradeCounterText;
    public TMP_Text unlockPriceText;
    public GameManager gameManager;
    public int baseGrowCount;
    public int upgradeCount;
    public int unlockPrice;
    public string beanName;
    public float initialUpgradeCost;
    public float timeToGrow;
    public float upgradeCoefficient;
    public bool growClicked;
    public bool unlocked;

    private int growCount;
    private int upgradeMultiplier = 0;
    private int currentStageUpgradeCount = 1;
    public int timeReductionInterval = 25;
    private int multiBuyNum = 1;
    private const float TimeReductionFactor = 0.5f;
    private float timer;
    private bool hasManager;

    private void Start()
    {
        //unlocked = false;
        timer = timeToGrow;
        growCount = baseGrowCount;
        growClicked = false;
        gameManager = gameManagerObj.GetComponent<GameManager>();
        unlockPriceText.text = "Unlock " + GlobalFunctions.FormatNumber(unlockPrice, true);
        timerSlider.maxValue = timer;
        timerSlider.value = timer;
        beanUpgSlider.maxValue = timeReductionInterval;
        beanUpgSlider.value = 1;
        unlockPanel.SetActive(!unlocked);
        growCount = baseGrowCount * upgradeCount;
        UpdateText();
        //currentUpgrade = gameManager.inv_upgStartCnt + 1;
    }

    private void Update()
    {
        if (unlocked && (hasManager || growClicked))
        {
            timer -= Time.deltaTime;
            timerSlider.value = timerSlider.maxValue - timer;

            if (timer <= 0f)
            {
                GrowBean();
                ResetTimer();
            }
        }

        switch (gameManager.currentMultiBuyIndex)
        {
            case 0: // "1"
                multiBuyNum = 1;
                break;
            case 1: // "10"
                multiBuyNum = 10;
                break;
            case 2: // "Next"
                multiBuyNum = timeReductionInterval - upgradeCount;
                break;
            case 3: // "Max"
                multiBuyNum = CalculateMaxGenerators();
                if(beanName == "Arabica")
                {
                    Debug.Log(multiBuyNum);
                }
                break;
        }

        unlockPanel.SetActive(!unlocked);
        UpdateText();
    }

    private void UpdateText()
    {
        growCountText.text = "" + GlobalFunctions.FormatNumber(
            growCount * (upgradeMultiplier > 0 ? upgradeMultiplier : 1) * 
            (gameManager.growthRateMultiplier + (gameManager.investors * gameManager.investorEffectiveness)));
        upgradeCostText.text = GlobalFunctions.FormatNumber(CalculateUpgradeCost(), true);//"$" + CalculateUpgradeCost();
        timeToGrowText.text = timer.ToString("0") + "s";
        upgradeCounterText.text = upgradeCount + "/" + timeReductionInterval;
        beanUpgSlider.value = upgradeCount;
    }

    public void UpgradeBean()
    {
        //float upgCost = (int)(initialUpgradeCost + (initialUpgradeCost * Mathf.Pow(1.09f, (upgradeCount - 1))));
        double upgCost = CalculateUpgradeCost();

        //upgCost = upgCost - (upgCost * gameManager.growthCostReducer);

        if (gameManager.money >= upgCost)
        {
            //upgradeCount++;
            upgradeCount += multiBuyNum;
            //currentStageUpgradeCount++;
            currentStageUpgradeCount += multiBuyNum;

            gameManager.SpendMoney(upgCost);

            // Update sell price based on prestige level and current upgrade
            growCount = baseGrowCount * upgradeCount;

            // Reduce time to grow the bean every TimeReductionInterval upgrades
            if (upgradeCount >= timeReductionInterval)
            {
                int thresholdsCrossed = (int)Mathf.Floor(upgradeCount / 25);

                timeToGrow = timeToGrow * Mathf.Pow(0.5f, thresholdsCrossed);

                //timeToGrow *= (TimeReductionFactor * thresholdsCrossed);
                beanUpgSlider.minValue = timeReductionInterval;
                timeReductionInterval += (25 * thresholdsCrossed);
                beanUpgSlider.maxValue = timeReductionInterval;
                timerSlider.maxValue = timeToGrow;
                ResetTimer();
                //currentStageUpgradeCount = 0;
            }
        }
        
    }

    // Gives production rate per minute
    public double CalculateProductionRate()
    {
        Debug.Log("Grow Count: " + growCount);
        Debug.Log("TimeToGrow: " + timeToGrow);
        return growCount / timeToGrow * 60; 
    }
    public void SetManager(bool hasManager)
    {
        this.hasManager = hasManager;
    }

    public int GetUpgradeMultiplier()
    {
        return upgradeMultiplier;
    }
    public void SetUpgradeMultiplier(int upgM)
    {
        this.upgradeMultiplier = upgM;
    }

    private void GrowBean()
    {
        gameManager.AddBeans((int)((growCount * (upgradeMultiplier > 0 ? upgradeMultiplier : 1)) *
            (gameManager.growthRateMultiplier + (gameManager.investors * gameManager.investorEffectiveness))));
    }

    private void ResetTimer()
    {
        timer = timeToGrow;
        growClicked = false;
    }

    public void Grow()
    {
        growClicked = true;
    }

    public void unlockBean()
    {
        if(gameManager.money >= unlockPrice)
        {
            gameManager.SpendMoney(unlockPrice);
            //unlockPanel.SetActive(false);
            unlocked = true;
        }
    }

    public void lockBean()
    {
    }

    public void setUnlock(bool inp)
    {
        unlocked = inp;
    }

    public void resetBean()
    {
        upgradeCount = gameManager.inv_upgStartCnt + 1;
        growCount = baseGrowCount * upgradeCount;
        UpdateText();
    }

    private double CalculateUpgradeCost()
    {
        double upgCost = 0;
        if(multiBuyNum == 1)
        {
            upgCost = initialUpgradeCost * Mathf.Pow(upgradeCoefficient, (upgradeCount - 1));
        } else
        {
            double rk = Mathf.Pow(upgradeCoefficient, (upgradeCount - 1));
            double rn = Mathf.Pow(upgradeCoefficient, multiBuyNum);
            upgCost = initialUpgradeCost * ((rk * (rn - 1)) / (upgradeCoefficient - 1));
            //upgCost = initialUpgradeCost * ((Mathf.Pow;
        }

        //float upgCost = initialUpgradeCost + (initialUpgradeCost * Mathf.Pow(1.09f, (upgradeCount - 1)));
        return upgCost;// - (upgCost * (gameManager.growthCostReducer));
    }

    private int CalculateMaxGenerators()
    {
        double cr1 = gameManager.money * (upgradeCoefficient - 1);
        double brk = initialUpgradeCost * Mathf.Pow(upgradeCoefficient, (upgradeCount - 1));
        double x = (cr1 / brk) + 1;
        double log = Mathf.Log((float)x, upgradeCoefficient);
        int maxGens = (int)Mathf.Floor((float)log);
        return maxGens;
    }
}
