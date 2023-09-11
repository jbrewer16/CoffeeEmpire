using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour, IDataPersistence
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
	public long beanCnt;
	public long coffee;
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
		//money = 10;
		//gems = 0;
		//prestigeLevel = 1;
		//investors = 0;
		//investorEffectiveness = 0.02f;
		//investorIncomeBoost = 0;
		////costReducer = 0f;
		//growthRateMultiplier = 1.00f;
		//inv_growthRateMultiplier = 1.00f;
		//inv_profitBonus = 0;
		//inv_upgPriceReducer = 0;
		//growthCostReducer = 0;
		//brewTimeCostReducer = 0;
		//sellTimeCostReducer = 0;
		//coffeeSellPriceInc = 0;
		//beanDensity = 0;
		//beanCnt = 0;
		//coffee = 0;

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
		beanCntText.text = "" + GlobalFunctions.FormatNumber(beanCnt);
		// Update the money UI text with the current money value
		moneyText.text = GlobalFunctions.FormatNumber(money, true);//"$ " + money.ToString();
		// Update the gems UI text with the current gems value
		gemsText.text = "" + GlobalFunctions.FormatNumber(gems);
		// Update the gems UI text with the current gems value
		coffeeCntText.text = GlobalFunctions.FormatNumber(coffee);

	}

	public void LoadData(GameData data)
    {
		this.money								= data.money;
		this.totalMoneyEarned					= data.totalMoneyEarned;
		this.investorEffectiveness				= data.investorEffectiveness;
		this.investorIncomeBoost					= data.investorIncomeBoost;
		this.growthRateMultiplier				= data.growthRateMultiplier;
		this.inv_growthRateMultiplier			= data.inv_growthRateMultiplier;
		this.growthCostReducer					= data.growthCostReducer;
		this.upgPriceReducer						= data.upgPriceReducer;
		this.brewTimeCostReducer					= data.brewTimeCostReducer;
		this.sellTimeCostReducer					= data.sellTimeCostReducer;
		this.coffeeSellPriceInc					= data.coffeeSellPriceInc;
		this.inv_coffeeSellPriceInc				= data.inv_coffeeSellPriceInc;
		this.inv_speedPerTap						= data.inv_speedPerTap;
		this.inv_profitBonus						= data.inv_profitBonus;
		this.inv_freeUpgChance					= data.inv_freeUpgChance;
		this.inv_freeBrewChance					= data.inv_freeBrewChance;
		this.inv_upgPriceReducer					= data.inv_upgPriceReducer;
		this.beanDensity							= data.beanDensity;
		this.beanCnt								= data.beanCnt;
		this.coffee								= data.coffee;
		this.gems								= data.gems;
		this.prestigeLevel						= data.prestigeLevel;
		this.investors							= data.investors;
		this.inv_upgStartCnt						= data.inv_upgStartCnt;

		coffeeManager.brewCapacityUpgrades		= data.brewCapacityUpgrades;
		coffeeManager.timerReduceAmount			= data.coffeeTimerReduceAmount;
		coffeeManager.brewCapacity				= data.brewCapacity;
		coffeeManager.brewCapacityUpgPrice		= data.brewCapacityUpgPrice;
		coffeeManager.brewSpeedUpgPrice			= data.brewSpeedUpgPrice;

		customerManager.custCapacityUpgrades		= data.custCapacityUpgrades;
		customerManager.timerReduceAmount		= data.customerTimerReduceAmount;
		customerManager.custCapacity				= data.custCapacity;
		customerManager.custCapacityUpgPrice		= data.custCapacityUpgPrice;
		customerManager.serveSpeedUpgPrice		= data.serveSpeedUpgPrice;

	}

	public void SaveData(ref GameData data)
    {
		data.money						= this.money;
		data.totalMoneyEarned			= this.totalMoneyEarned;
		data.investorEffectiveness		= this.investorEffectiveness;
		data.investorIncomeBoost			= this.investorIncomeBoost;
		data.growthRateMultiplier		= this.growthRateMultiplier;
		data.inv_growthRateMultiplier	= this.inv_growthRateMultiplier;
		data.growthCostReducer			= this.growthCostReducer;
		data.upgPriceReducer				= this.upgPriceReducer;
		data.brewTimeCostReducer			= this.brewTimeCostReducer;
		data.sellTimeCostReducer			= this.sellTimeCostReducer;
		data.coffeeSellPriceInc			= this.coffeeSellPriceInc;
		data.inv_coffeeSellPriceInc		= this.inv_coffeeSellPriceInc;
		data.inv_speedPerTap				= this.inv_speedPerTap;
		data.inv_profitBonus				= this.inv_profitBonus;
		data.inv_freeUpgChance			= this.inv_freeUpgChance;
		data.inv_freeBrewChance			= this.inv_freeBrewChance;
		data.inv_upgPriceReducer			= this.inv_upgPriceReducer;
		data.beanDensity					= this.beanDensity;
		data.beanCnt						= this.beanCnt;
		data.coffee						= this.coffee;
		data.gems						= this.gems;
		data.prestigeLevel				= this.prestigeLevel;
		data.investors					= this.investors;
		data.inv_upgStartCnt				= this.inv_upgStartCnt;

		data.brewCapacityUpgrades		= coffeeManager.brewCapacityUpgrades;
		data.coffeeTimerReduceAmount		= coffeeManager.timerReduceAmount;
		data.brewCapacity				= coffeeManager.brewCapacity;
		data.brewCapacityUpgPrice		= coffeeManager.brewCapacityUpgPrice;
		data.brewSpeedUpgPrice			= coffeeManager.brewSpeedUpgPrice;	 

		data.custCapacityUpgrades		= customerManager.custCapacityUpgrades;
		data.customerTimerReduceAmount	= customerManager.timerReduceAmount;
		data.custCapacity				= customerManager.custCapacity;
		data.custCapacityUpgPrice		= customerManager.custCapacityUpgPrice;
		data.serveSpeedUpgPrice			= customerManager.serveSpeedUpgPrice;
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
