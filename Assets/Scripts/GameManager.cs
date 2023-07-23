using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<Bean> beans = new List<Bean>();
    public float money;
    public float investorEffectiveness;
    //public float costReducer;
    public float growthRateMultiplier;
    public float growthCostReducer;
    public float brewTimeCostReducer;
    public float sellTimeCostReducer;
    public float coffeeSellPriceInc;
    public int beanDensity;
    public int beanCnt;
    public int coffee;
    public int gems;
    public int prestigeLevel;
    public int investors;

    // Track unlocked coffee beans
    private bool[] unlockedBeans;
    // Reference to UI text for displaying money and gems
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text beanCntText;
    [SerializeField] private TMP_Text coffeeCntText;
    [SerializeField] private TMP_Text gemsText;

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize base stats
        money = 10;
        gems = 0;
        prestigeLevel = 1;
        investors = 0;
        investorEffectiveness = 0.001f;
        //costReducer = 0f;
        growthRateMultiplier = 1.00f;
        growthCostReducer = 0;
        brewTimeCostReducer = 0;
        sellTimeCostReducer = 0;
        coffeeSellPriceInc = 0;
        beanDensity = 0;
        beanCnt = 0;
        coffee = 0;

        // Initialize unlocked beans array (assuming there are 10 beans in total)
        unlockedBeans = new bool[10];

        // Unlock the first bean at the start of the game
        UnlockBean(0);

        // Update the money and gems UI display
        updateText();
    }

    public void Update()
    {
        // Update the money and gems UI display
        updateText();
    }

    public void updateText()
    {
        // Update the gems UI text with the current gems value
        beanCntText.text = "" + beanCnt.ToString();
        // Update the money UI text with the current money value
        moneyText.text = "$ " + money.ToString();
        // Update the gems UI text with the current gems value
        gemsText.text = "" + gems.ToString();
        // Update the gems UI text with the current gems value
        coffeeCntText.text = coffee.ToString() + "c";

    }

    public void AddToBeanScriptList(Bean b)
    {
        beans.Add(b);
    }

    public void AddMoney(float amount)
    {
        money += amount;

        // Update the money UI display
        //UpdateMoneyText();
    }

    public void AddBeans(int amount)
    {
        beanCnt += amount;

        // Update the money UI display
        //UpdateMoneyText();
    }

    public void SpendMoney(float amount)
    {
        money -= amount;

        // Update the money UI display
        //UpdateMoneyText();
    }

    public void AddGems(int amount)
    {
        gems += amount;

        // Update the gems UI display
        //UpdateGemsText();
    }

    public void UnlockBean(int beanIndex)
    {
        // Set the corresponding bean index to true in the unlocked beans array
        unlockedBeans[beanIndex] = true;
    }

    public bool IsBeanUnlocked(int beanIndex)
    {
        // Check if the bean at the given index is unlocked
        return unlockedBeans[beanIndex];
    }

    public void Prestige()
    {
        // Reset game progress, increase prestige level, and provide any bonuses
        money = 0;
        gems = 0;

        // Reset unlocked beans array
        for (int i = 0; i < unlockedBeans.Length; i++)
        {
            unlockedBeans[i] = false;
        }

        // Increase prestige level
        prestigeLevel++;

        // Update the money and gems UI display
        //UpdateMoneyText();
        //UpdateGemsText();
    }

    public void AddGrowthRateMultiplier(float g)
    {
        growthRateMultiplier += g;
    }

    public void AddGrowthCostReducer(float g)
    {
        growthCostReducer += g;
    }

    public void AddBeanDensity(int b)
    {
        beanDensity += b;
    }

    public void ReduceBrewTimeCost(float b)
    {
        brewTimeCostReducer += b;
    }

    public void ReduceSellTimeCost(float s)
    {
        sellTimeCostReducer += s;
    }

    public void AddCoffeeSellPrice(int c)
    {
        coffeeSellPriceInc += c;
    }

}
