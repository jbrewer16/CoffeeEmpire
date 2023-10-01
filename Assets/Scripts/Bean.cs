using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bean : MonoBehaviour
{

    public int baseGrowCount;
    public int upgradeCount;
    public int unlockPrice;
    public string beanName;
    public float initialUpgradeCost;
    public float timeToGrow;
    public float upgradeCoefficient;
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
    public int timeReductionInterval = 25;
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
        float upgCost = CalculateUpgradeCost();

        //upgCost = upgCost - (upgCost * gameManager.growthCostReducer);

        if (gameManager.money >= upgCost)
        {
            upgradeCount++;
            currentStageUpgradeCount++;

            gameManager.SpendMoney(upgCost);

            // Update sell price based on prestige level and current upgrade
            growCount = baseGrowCount * upgradeCount;

            // Reduce time to grow the bean every TimeReductionInterval upgrades
            if (upgradeCount == timeReductionInterval)
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
            unlockPanel.SetActive(false);
            unlocked = true;
        }
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

    private float CalculateUpgradeCost()
    {
        float upgCost = initialUpgradeCost * Mathf.Pow(upgradeCoefficient, (upgradeCount - 1));
        //float upgCost = initialUpgradeCost + (initialUpgradeCost * Mathf.Pow(1.09f, (upgradeCount - 1)));
        return upgCost - (upgCost * (gameManager.growthCostReducer + gameManager.inv_upgPriceReducer));
    }
}
