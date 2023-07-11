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
    private const int TimeReductionInterval = 50;
    private const float TimeReductionFactor = 0.5f;
    private float timer;
    private bool hasManager;

    private void Start()
    {
        unlocked = false;
        timer = timeToGrow;
        growCount = baseGrowCount;
        growClicked = false;
        gameManager = gameManagerObj.GetComponent<GameManager>();
        unlockPriceText.text = "Unlock $" + unlockPrice;
        timerSlider.maxValue = timer;
        timerSlider.value = timer;
        beanUpgSlider.value = 1;
    }

    private void Update()
    {
        if (hasManager || growClicked)
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
        growCountText.text = "" + growCount * (upgradeMultiplier > 0 ? upgradeMultiplier : 1);
        upgradeCostText.text = "$" + CalculateUpgradeCost();
        timeToGrowText.text = timer.ToString("0") + "s";
        upgradeCounterText.text = currentUpgrade + "/50";
        beanUpgSlider.value = currentUpgrade;
    }

    public void UpgradeBean()
    {
        float upgCost = (int)(upgradeCost + (upgradeCost * Mathf.Pow(1.09f, (currentUpgrade - 1))));

        if (gameManager.money >= (upgCost - (upgCost * gameManager.costReducer)))
        {
            currentUpgrade++;

            gameManager.SpendMoney((upgCost - (upgCost * gameManager.costReducer)));

            // Update sell price based on prestige level and current upgrade
            growCount = baseGrowCount * currentUpgrade;

            // Reduce time to grow the bean every TimeReductionInterval upgrades
            if (currentUpgrade % TimeReductionInterval == 0)
            {
                timeToGrow *= TimeReductionFactor;
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
        Debug.Log(upgradeMultiplier);
        Debug.Log(growCount * (upgradeMultiplier > 0 ? upgradeMultiplier : 1));
        gameManager.AddBeans(growCount * (upgradeMultiplier > 0 ? upgradeMultiplier : 1));
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
        }
    }

    private float CalculateUpgradeCost()
    {
        // Implement your upgrade cost calculation logic here
        // Example: Upgrade cost doubles for each upgrade
        return upgradeCost + (upgradeCost * Mathf.Pow(1.09f, (currentUpgrade-1)));
    }
}
