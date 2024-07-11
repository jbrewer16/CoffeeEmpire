using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bean : MonoBehaviour
{
    public int          id;
    public int          baseGrowCount;
    public int          growCount;
    public int          timeReductionInterval = 25;
    public int          upgradeCount;
    public double       unlockPrice;
    private int         currentStageUpgradeCount = 1;
    public string       beanName;
    public Slider       beanUpgSlider;
    public Slider       timerSlider;
    public GameManager  gameManager;
    public GameObject   gameManagerObj;
    public GameObject   unlockPanel;
    public bool         growClicked;
    public bool         unlocked;
    public TMP_Text     buyCountTxt;
    public TMP_Text     growCountText;
    public TMP_Text     timeToGrowText;
    public TMP_Text     upgradeCostText;
    public TMP_Text     upgradeCounterText;
    public TMP_Text     unlockPriceText;
    public double        initialUpgradeCost;
    public float        timeToGrow;
    public float        upgradeCoefficient;
    public AudioClip tapSound;
    public AudioSource audioSource;

    private bool        hasManager;
    private int         multiBuyNum = 1;
    private int         upgradeMultiplier = 0;
    private float       timer;
    private const float TimeReductionFactor = 0.5f;

    public Image beanIcon;
    public string beanIconPath;

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
        beanIconPath = "BeanIcons/" + beanName;
        setBeanImage(beanIconPath);
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
                break;
        }

        unlockPanel.SetActive(!unlocked);
        UpdateText();
    }

    private void UpdateText()
    {
        buyCountTxt.text = "x" + multiBuyNum;
        growCountText.text = "" + GlobalFunctions.FormatNumber(
            growCount * (upgradeMultiplier > 0 ? upgradeMultiplier : 1) * 
            (gameManager.growthRateMultiplier + (gameManager.investors * gameManager.investorEffectiveness)));
        upgradeCostText.text = GlobalFunctions.FormatNumber(CalculateUpgradeCost(), true);//"$" + CalculateUpgradeCost();
        timeToGrowText.text = timer.ToString("0") + "s";
        upgradeCounterText.text = upgradeCount + "/" + timeReductionInterval;
        beanUpgSlider.value = upgradeCount;
    }

    public void setBeanImage(string path)
    {
        Sprite icon = Resources.Load<Sprite>(path);
        beanIcon.sprite = icon;
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
        //Debug.Log("Grow Count: " + growCount);
        //Debug.Log("TimeToGrow: " + timeToGrow);
        return (growCount / timeToGrow) * 60;
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
            upgCost -= (upgCost * gameManager.growthCostReducer);
        } else
        {
            double rk = Mathf.Pow(upgradeCoefficient, (upgradeCount - 1));
            double rn = Mathf.Pow(upgradeCoefficient, multiBuyNum);
            upgCost = initialUpgradeCost * ((rk * (rn - 1)) / (upgradeCoefficient - 1));
            upgCost -= (upgCost * gameManager.growthCostReducer);
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

    //public void PlayTap()
    //{
    //    audioSource.PlayOneShot(tapSound);
    //}
}
