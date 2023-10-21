using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    public int initialCost;
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
    public UpgType upgTypes = new UpgType();

    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private int currentUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        //switch (upgTypes)
        //{
        //    case UpgType.cash:
        //        btnTxt.text = "Upgrade \n $" + cost;
        //        break;
        //    case UpgType.investor:
        //        btnTxt.text = "Upgrade \n " + cost;
        //        break;
        //}

        //btnTxt.text = "Upgrade \n " + GlobalFunctions.FormatNumber(initialCost, upgTypes == UpgType.cash);
        gameManager = gameManagerObj.GetComponent<GameManager>();
        SetUpgradeCount();
        btnTxt.text = "Upgrade \n " + GlobalFunctions.FormatNumber(CalculateUpgradeCost(), upgTypes == UpgType.cash);
        //currentUpgrade = 1;
        updateText();

    }

    public void Update()
    {

        updateText();
        btnTxt.text = "Upgrade \n " + GlobalFunctions.FormatNumber(CalculateUpgradeCost(), upgTypes == UpgType.cash);
        //btnTxt.text = "Upgrade \n " + GlobalFunctions.FormatNumber(initialCost, upgTypes == UpgType.cash);
        //Debug.Log(CalculateUpgradeCost());
        //switch (upgTypes)
        //{
        //    case UpgType.cash:
        //        btnTxt.text = "Upgrade \n $" + CalculateUpgradeCost();
        //        break;
        //    case UpgType.investor:
        //        btnTxt.text = "Upgrade \n " + CalculateUpgradeCost();
        //        break;
        //}

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
            case UpgOptions.inv_startUpgCount:
                upgDescTxt.text = "+" + gameManager.inv_upgStartCnt + "\n" + desc;
                break;
            case UpgOptions.inv_profitBonus:
                upgDescTxt.text = "+" + gameManager.inv_profitBonus + "\n" + desc;
                break;  
            case UpgOptions.inv_upgPriceReducer:
                upgDescTxt.text = gameManager.inv_upgPriceReducer + "%\n" + desc;
                break;
            case UpgOptions.inv_speedIncreasePerTap:
                upgDescTxt.text = gameManager.inv_speedPerTap + "s\n" + desc;
                break;
            case UpgOptions.inv_freeUpgChance:
                upgDescTxt.text = gameManager.inv_freeUpgChance + "%\n" + desc;
                break;
            case UpgOptions.inv_freeBrewChance:
                upgDescTxt.text = gameManager.inv_freeBrewChance + "%\n" + desc;
                break;
            default:
                break;
        }
    }

    private void SetUpgradeCount()
    {
        switch (upgOptions)
        {
            case UpgOptions.growthMult:
                currentUpgrade = gameManager.growthRateMultiplierCount;
                break;
            case UpgOptions.growthCost:
                currentUpgrade = gameManager.growthCostReducerCount;
                break;
            case UpgOptions.beanDensity:
                currentUpgrade = gameManager.growthCostReducerCount;
                break;
            case UpgOptions.brewTime:
                currentUpgrade = gameManager.brewTimeCostReducerCount;
                break;
            case UpgOptions.sellTime:
                currentUpgrade = gameManager.sellTimeCostReducerCount;
                break;
            case UpgOptions.coffeeSellPrice:
                currentUpgrade = gameManager.coffeeSellPriceIncCount;
                break;
            case UpgOptions.inv_startUpgCount:
                currentUpgrade = gameManager.inv_upgStartCntCount;
                break;
            case UpgOptions.inv_profitBonus:
                currentUpgrade = gameManager.inv_profitBonusCount;
                break;
            case UpgOptions.inv_upgPriceReducer:
                currentUpgrade = gameManager.inv_upgPriceReducerCount;
                break;
            case UpgOptions.inv_speedIncreasePerTap:
                currentUpgrade = gameManager.inv_speedPerTapCount;
                break;
            case UpgOptions.inv_freeUpgChance:
                currentUpgrade = gameManager.inv_freeUpgChanceCount;
                break;
            case UpgOptions.inv_freeBrewChance:
                currentUpgrade = gameManager.inv_freeBrewChanceCount;
                break;
            default:
                break;
        }
    }

    public void buyUpgrade()
    {
        float upgCost = CalculateUpgradeCost(); // (int)(initialCost + (initialCost * Mathf.Pow(upgradeCostMultiplier, (currentUpgrade - 1))));
        //if (gameManager.money >= upgCost)
        switch (upgTypes)
        {
            case UpgType.cash:
                int freeChance = Random.Range(0, 100);
                bool freeUpg = freeChance < gameManager.inv_freeUpgChance;
                if (!freeUpg)
                {
                    gameManager.SpendMoney(upgCost);
                }
                else
                {
                    Debug.Log("Congrats! Free Upgrade!");
                }
                break;
            case UpgType.investor:
                gameManager.SpendInvestors((int)upgCost);
                break;
        }
        
        {
            currentUpgrade++; 
            switch (upgOptions)
            {
                case UpgOptions.growthMult:
                    gameManager.AddGrowthRateMultiplier();
                    break;
                case UpgOptions.growthCost:
                    gameManager.AddGrowthCostReducer();
                    break;
                case UpgOptions.beanDensity:
                    gameManager.AddBeanDensity();
                    break;
                case UpgOptions.brewTime:
                    gameManager.ReduceBrewTimeCost();
                    break;
                case UpgOptions.sellTime:
                    gameManager.ReduceSellTimeCost();
                    break;
                case UpgOptions.coffeeSellPrice:
                    gameManager.AddCoffeeSellPrice();
                    break;

                case UpgOptions.inv_startUpgCount:
                    gameManager.AddInvUpgStartCount();
                    break;
                case UpgOptions.inv_profitBonus:
                    gameManager.AddInvProfitBonus();
                    break;
                case UpgOptions.inv_upgPriceReducer:
                    gameManager.AddInvUpgPriceReducer();
                    break;
                case UpgOptions.inv_speedIncreasePerTap:
                    gameManager.AddInvSpeedPerTap();
                    break;
                case UpgOptions.inv_freeUpgChance:
                    gameManager.AddInvFreeUpgChance();
                    break;
                case UpgOptions.inv_freeBrewChance:
                    gameManager.AddInvFreeBrewChance();
                    break;
                default:
                    break;
            }

            btnTxt.text = "Upgrade \n $" + initialCost;
        }
    }

    private float CalculateUpgradeCost()
    {
        float upgCost = initialCost * Mathf.Pow(upgradeCostMultiplier, (currentUpgrade - 1));
        if(upgTypes == UpgType.cash)
        {
            return upgCost - (upgCost * (gameManager.growthCostReducer + gameManager.inv_upgPriceReducer));
        } else
        {
            return upgCost;
        }
    }

}

public enum UpgOptions
{
    growthMult,
    growthCost,
    beanDensity,
    brewTime,
    sellTime,
    coffeeSellPrice,
    inv_startUpgCount,
    inv_profitBonus,
    inv_upgPriceReducer,
    inv_speedIncreasePerTap,
    inv_freeUpgChance,
    inv_freeBrewChance
}

public enum UpgType
{
    cash,
    investor
}
