using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bean : MonoBehaviour
{

    public int baseGrowCount;
    public int currentUpgrade;
    public int unlockPrice;
    public string beanName;
    public float upgradeCost;
    public float timeToGrow;
    public bool growClicked;
    public bool unlocked;

    public GameObject gameManagerObj;
    public GameObject unlockPanel;

    public Slider timerSlider;
    public Slider beanUpgSlider;

    public GameManager gameManager;

    public TMP_Text growCountText;
    public TMP_Text upgradeCostText;
    public TMP_Text timeToGrowText;
    public TMP_Text upgradeCounterText;
    public TMP_Text unlockPriceText;

    private int growCount;
    private int upgradeMultiplier = 0;
    private int currentStageUpgradeCount = 1;
    private int timeReductionInterval = 25;
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
        unlockPriceText.text = "Unlock $" + unlockPrice;
        timerSlider.maxValue = timer;
        timerSlider.value = timer;
        beanUpgSlider.maxValue = timeReductionInterval;
        beanUpgSlider.value = 1;
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

        UpdateText();
    }

    private void UpdateText()
    {
        growCountText.text = "" + (int)((growCount * (upgradeMultiplier > 0 ? upgradeMultiplier : 1)) * gameManager.growthRateMultiplier);
        upgradeCostText.text = "$" + CalculateUpgradeCost();
        timeToGrowText.text = timer.ToString("0") + "s";
        upgradeCounterText.text = currentUpgrade + "/" + timeReductionInterval;
        beanUpgSlider.value = currentUpgrade;
    }

    public void UpgradeBean()
    {
        float upgCost = (int)(upgradeCost + (upgradeCost * Mathf.Pow(1.09f, (currentUpgrade - 1))));

        upgCost = upgCost - (upgCost * gameManager.growthCostReducer);

        if (gameManager.money >= upgCost)
        {
            currentUpgrade++;
            currentStageUpgradeCount++;

            gameManager.SpendMoney(upgCost);

            // Update sell price based on prestige level and current upgrade
            growCount = baseGrowCount * currentUpgrade;

            // Reduce time to grow the bean every TimeReductionInterval upgrades
            if (currentUpgrade == timeReductionInterval)
            {
                timeToGrow *= TimeReductionFactor;
                beanUpgSlider.minValue = timeReductionInterval;
                timeReductionInterval += 25;
                beanUpgSlider.maxValue = timeReductionInterval;
                timerSlider.maxValue = timeToGrow;
                ResetTimer();
                //currentStageUpgradeCount = 0;
            }
        }
        
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
        gameManager.AddBeans((int)(growCount * (upgradeMultiplier > 0 ? upgradeMultiplier : 1) * gameManager.growthRateMultiplier));
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
            unlockPanel.SetActive(false);
            unlocked = true;
        }
    }

    public void setUnlock(bool inp)
    {
        unlocked = inp;
    }

    private float CalculateUpgradeCost()
    {
        // Implement your upgrade cost calculation logic here
        // Example: Upgrade cost doubles for each upgrade
        float upgCost = upgradeCost + (upgradeCost * Mathf.Pow(1.09f, (currentUpgrade - 1)));
        return upgCost - (upgCost * gameManager.growthCostReducer);
    }
}
