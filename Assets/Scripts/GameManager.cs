using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public GameObject prestigeSystemObj;
	public PrestigeSystem prestigeSystem;
	public CoffeeManager coffeeManager;
	public CustomerManager customerManager;
	public List<Bean> beans = new List<Bean>();
	public float money;
	public float totalMoneyEarned; //Money earned this prestige
	public float investorEffectiveness;
	public float investorIncomeBoost;
	//public float costReducer;
	public float growthRateMultiplier;
	public float inv_growthRateMultiplier;
	public float growthCostReducer;
	public float upgPriceReducer;
	public float brewTimeCostReducer;
	public float sellTimeCostReducer;
	public float coffeeSellPriceInc;
	public float inv_coffeeSellPriceInc;
	public float inv_speedPerTap;
	public int inv_profitBonus;
	public int inv_freeUpgChance;
	public int inv_freeBrewChance;
	public float inv_upgPriceReducer;
	public int beanDensity;
	public int beanCnt;
	public int coffee;
	public int gems;
	public int prestigeLevel;
	public int investors;
	public int inv_upgStartCnt;

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
		investorEffectiveness = 0.02f;
		investorIncomeBoost = 0;
		//costReducer = 0f;
		growthRateMultiplier = 1.00f;
		inv_growthRateMultiplier = 1.00f;
		inv_profitBonus = 0;
		inv_upgPriceReducer = 0;
		growthCostReducer = 0;
		brewTimeCostReducer = 0;
		sellTimeCostReducer = 0;
		coffeeSellPriceInc = 0;
		beanDensity = 0;
		beanCnt = 0;
		coffee = 0;

		prestigeSystem = prestigeSystemObj.GetComponent<PrestigeSystem>();

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
		totalMoneyEarned += amount;
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

	public void SpendInvestors(int amount)
	{
		investors -= amount;
	}

	public void AddGems(int amount)
	{
		gems += amount;

		// Update the gems UI display
		//UpdateGemsText();
	}

	public void prestigeReset()
	{
		money = 0;
		beanCnt = 0;
		totalMoneyEarned = 0;
		coffee = 0;
		int beanI = 0;
		foreach(Bean b in beans)
		{
			b.resetBean();
			if(beanI > 0)
            {
				b.unlockPanel.SetActive(true);
            }
			beanI++;
		}
		// Reset unlocked beans array
		for (int i = 1; i < unlockedBeans.Length; i++)
		{
			unlockedBeans[i] = false;
		}
		coffeeManager.resetCoffee();
		customerManager.resetCustomer();

	}

	public void AddInvUpgStartCount(int amount = 1)
	{
		inv_upgStartCnt += amount;
	}

	public void AddInvProfitBonus(int p = 5)
	{
		inv_profitBonus += p;
	}

	public void AddInvestors(int i)
	{
		investors += i;
		CalculateInvestorBoost();
	}

	public void CalculateInvestorBoost()
	{
		investorIncomeBoost = (investors * investorEffectiveness);
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


	public void AddInvUpgPriceReducer(float c = 0.05f)
	{
		inv_upgPriceReducer += c;
    }
	public void AddGrowthRateMultiplier(float g = 0.05f)
	{
		growthRateMultiplier += g;
	}
	public void AddInvGrowthRateMultiplier(float g)
	{
		inv_growthRateMultiplier += g;
	}

	public void AddInvSpeedPerTap(float s = 0.05f)
    {
		inv_speedPerTap += s;
    }

	public void AddInvFreeUpgChance(int f = 1)
    {
		inv_freeUpgChance += f;
    }

	public void AddInvFreeBrewChance(int b = 1)
    {
		inv_freeBrewChance += b;
    }

	public void AddGrowthCostReducer(float g = 0.01f)
	{
		growthCostReducer += g;
	}

	public void AddBeanDensity(int b = 1)
	{
		beanDensity += b;
	}

	public void ReduceBrewTimeCost(float b = 0.01f)
	{
		brewTimeCostReducer += b;
	}

	public void ReduceSellTimeCost(float s = 0.01f)
	{
		sellTimeCostReducer += s;
	}

	public void AddCoffeeSellPrice(int c = 1)
	{
		coffeeSellPriceInc += c;
	}

}
