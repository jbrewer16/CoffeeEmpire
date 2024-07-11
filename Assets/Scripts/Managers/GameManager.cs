using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class GameManager : MonoBehaviour, IDataPersistence
{
	public static GameManager Instance { get; private set; }
	public GameObject prestigeSystemObj;
	public GameObject offlineWatchAdBtn;
	public PrestigeSystem prestigeSystem;
	public CoffeeManager coffeeManager;
	public CustomerManager customerManager;
	public List<Bean> beans = new List<Bean>();
	public GameObject offlineEarningsPanel;

	public DoubleBrewingCapacityAd doubleBrewingCapacityAd;
	public DoubleCustomerCapacityAd doubleCustomerCapacityAd;

	// Track unlocked coffee beans
	private bool[] unlockedBeans;
	// Reference to UI text for displaying money and gems
	[SerializeField] private TMP_Text moneyText;
	[SerializeField] private TMP_Text beanCntText;
	[SerializeField] private TMP_Text coffeeCntText;
	[SerializeField] private TMP_Text gemsText;
	[SerializeField] private TMP_Text offlineCounterTxt;
	[SerializeField] private TMP_Text earningsTxt;
	[SerializeField] private TMP_Text watchAdTxt;

	public TMP_Text multiBuyButtonTxt;
	public string[] multiBuyOptions;
	public int currentMultiBuyIndex;

	public double offlineMoneyGains;
	public double offlineMoneyGainsWithAd;
	public TimeSpan offlineDuration;
	public float offlineMaxHours;

	public float gainsReduction = 0.15f;
	private Coroutine doubleIncomeCoroutine;

	public bool dataLoaded;

	// General Player Data
	public double beanCnt;
	public double totalEarnedBeans;
	public double lifetimeEarnedBeans;
	public double coffee;
	public double lifetimeEarnings;
	public double money;
	public double totalMoneyEarned;
	public int beanDensity;
	public int gems;
	public int prestigeLevel;
	public long investors;
	public long lifetimeInvestors;
	public DateTime lastOnlineTime;
	public List<int> harvesters;
	public bool x2MultUnlocked;
	public bool x12MultUnlocked;
	public bool finishedTutorial;

	// Ad info
	public int doubleIncomeAdsWatched;
	public bool doubleIncomeActive;
	public bool doubleCustCapacityActive;
	public bool doubleBrewCapacityActive;
	public float remainingDoubleIncomeTime = 0f;
	public float remainingDoubleBrewingTime = 0f;
	public float remainingDoubleCustomerTime = 0f;

	// Upgrade Page Data
	public float brewTimeCostReducer;
	public int coffeeSellPriceInc;
	public float growthCostReducer;
	public float growthRateMultiplier;
	public float investorEffectiveness;
	public float investorIncomeBoost;
	public float sellTimeCostReducer;
	public float upgPriceReducer;
	// // Counts
	public int brewTimeCostReducerCount;
	public int coffeeSellPriceIncCount;
	public int growthCostReducerCount;
	public int growthRateMultiplierCount;
	public int investorEffectivenessCount;
	public int investorIncomeBoostCount;
	public int sellTimeCostReducerCount;
	public int upgPriceReducerCount;

	// Investor Page Data
	// // Upgrades
	public float inv_growthRateMultiplier;
	public float inv_upgPriceReducer;
    public float inv_speedPerTap;
    public int inv_freeBrewChance;
	public int inv_freeSellChance;
	public int inv_freeUpgChance;
	public int inv_profitBonus;
	public int inv_upgStartCnt;
	//public float inv_coffeeSellPriceInc;
	// // Counts
	public int inv_freeBrewChanceCount;
	public int inv_freeSellChanceCount;
	public int inv_freeUpgChanceCount;
	public int inv_growthRateMultiplierCount;
	public int inv_profitBonusCount;
    public int inv_speedPerTapCount;
    public int inv_upgPriceReducerCount;
	public int inv_upgStartCntCount;
	//public int inv_coffeeSellPriceIncCount;

	// Coffee Brew Page Data
	public double brewCapacity;
	public int brewCapacityUpgrades;
	public double brewCapacityUpgPrice;
	public float brewSpeedUpgPrice;
	public float coffeeTimerReduceAmount;

	// Customer Page Data
	public int custCapacity;
	public int custCapacityUpgrades;
	public float custCapacityUpgPrice;
	public float customerTimerReduceAmount;
	public float serveSpeedUpgPrice;

	private bool gainsNotCalculated = false;

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
		dataLoaded = false;
		currentMultiBuyIndex = 0;
		multiBuyOptions = new string[]{"1", "10", "Next", "Max"};

		prestigeSystem = prestigeSystemObj.GetComponent<PrestigeSystem>();

		offlineMaxHours = 2;

		// Initialize unlocked beans array (assuming there are 10 beans in total)
		unlockedBeans = new bool[10];

        // Unlock the first bean at the start of the game
        UnlockBean(0);
        //UnlockFirstBean();

        // Update the money and gems UI display
        updateText();
	}

	public void Update()
	{

		// Update the money and gems UI display
		updateText();

        if(beans != null && beans[0].growCount > 0 && !gainsNotCalculated)
        {
			CalculateOfflineGains();
			gainsNotCalculated = true;
		}

	}

	public void updateText()
	{
		// Update the gems UI text with the current gems value
		beanCntText.text = "" + GlobalFunctions.FormatNumber(beanCnt);
		// Update the money UI text with the current money value
		moneyText.text = GlobalFunctions.FormatNumber(money, true);//"$ " + money.ToString();
		// Update the gems UI text with the current gems value
		gemsText.text = "" + GlobalFunctions.FormatNumber(gems);//GlobalFunctions.FormatNumber(gems);
		// Update the gems UI text with the current gems value
		coffeeCntText.text = GlobalFunctions.FormatNumber(coffee);

	}

	public void UnlockFirstBean()
    {
        // Unlock the first bean at the start of the game
        UnlockBean(0);
        //CalculateOfflineGains();
	}

	public void UpdateMultiBuyButtonText()
    {
		multiBuyButtonTxt.text = "Buy " + ((currentMultiBuyIndex < 2) ? "x" : "") + multiBuyOptions[currentMultiBuyIndex];
    }

	public void ChangeMultiBuyOption()
    {
		currentMultiBuyIndex = (currentMultiBuyIndex + 1) % multiBuyOptions.Length;
		UpdateMultiBuyButtonText();
	}

	public void LoadData(GameData data)
    {
		// General Player Data
		this.beanCnt								= data.beanCnt;
		this.totalEarnedBeans						= data.totalEarnedBeans;
		this.lifetimeEarnedBeans					= data.lifetimeEarnedBeans;
		this.coffee									= data.coffee;
		this.lifetimeEarnings						= data.lifetimeEarnings;
		this.money									= data.money;
		this.totalMoneyEarned						= data.totalMoneyEarned;
		this.beanDensity							= data.beanDensity;
		this.gems									= data.gems;
		this.prestigeLevel							= data.prestigeLevel;
		this.investors								= data.investors;
		this.lifetimeInvestors						= data.lifetimeInvestors;
		this.lastOnlineTime							= DateTime.ParseExact(data.lastOnlineTime, "MM/dd/yyyyTHH:mm:ss", null);
		this.harvesters								= data.harvesters;
		this.x2MultUnlocked							= data.x2MultUnlocked;
		this.x12MultUnlocked						= data.x12MultUnlocked;
		this.finishedTutorial						= data.finishedTutorial;

		// Ad info
		this.doubleIncomeAdsWatched					= data.doubleIncomeAdsWatched;
		this.doubleIncomeActive						= data.doubleIncomeActive;
		this.doubleCustCapacityActive				= data.doubleCustCapacityActive;
		this.doubleBrewCapacityActive				= data.doubleBrewCapacityActive;
		this.remainingDoubleIncomeTime				= data.remainingDoubleIncomeTime;
		this.remainingDoubleBrewingTime				= data.remainingDoubleBrewingTime;
		this.remainingDoubleCustomerTime			= data.remainingDoubleCustomerTime;

		// Upgrade Page Data
		// // Upgrades
		this.brewTimeCostReducer					= data.brewTimeCostReducer;
		this.coffeeSellPriceInc						= data.coffeeSellPriceInc;
		this.growthCostReducer						= data.growthCostReducer;
		this.growthRateMultiplier					= data.growthRateMultiplier;
		this.investorEffectiveness					= data.investorEffectiveness;
		this.investorIncomeBoost					= data.investorIncomeBoost;
		this.sellTimeCostReducer					= data.sellTimeCostReducer;
		this.upgPriceReducer						= data.upgPriceReducer;
		// // Counts
		this.brewTimeCostReducerCount				= data.brewTimeCostReducerCount;
		this.coffeeSellPriceIncCount				= data.coffeeSellPriceIncCount;
		this.growthCostReducerCount					= data.growthCostReducerCount;
		this.growthRateMultiplierCount				= data.growthRateMultiplierCount;
		this.investorEffectivenessCount				= data.investorEffectivenessCount;
		this.investorIncomeBoostCount				= data.investorIncomeBoostCount;
		this.sellTimeCostReducerCount				= data.sellTimeCostReducerCount;
		this.upgPriceReducerCount					= data.upgPriceReducerCount;

		// Investor Page Data
		// // Upgrades
		//this.inv_growthRateMultiplier				= data.inv_growthRateMultiplier;
		this.inv_upgPriceReducer					= data.inv_upgPriceReducer;
		this.inv_freeBrewChance						= data.inv_freeBrewChance;
		this.inv_freeUpgChance						= data.inv_freeUpgChance;
		this.inv_profitBonus						= data.inv_profitBonus;
		this.inv_upgStartCnt						= data.inv_upgStartCnt;
		this.inv_speedPerTap						= data.inv_speedPerTap;
		// // Counts
		//this.inv_growthRateMultiplierCount			= data.inv_growthRateMultiplierCount;
		this.inv_upgPriceReducerCount				= data.inv_upgPriceReducerCount;
		this.inv_freeBrewChanceCount				= data.inv_freeBrewChanceCount;
		this.inv_freeUpgChanceCount					= data.inv_freeUpgChanceCount;
		this.inv_profitBonusCount					= data.inv_profitBonusCount;
		this.inv_upgStartCntCount					= data.inv_upgStartCntCount;
		this.inv_speedPerTapCount					= data.inv_speedPerTapCount;

		//// Coffee Brew Page Data
		coffeeManager.brewCapacity					= data.brewCapacity;
		coffeeManager.brewCapacityUpgPrice			= data.brewCapacityUpgPrice;
		coffeeManager.brewCapacityUpgrades			= data.brewCapacityUpgrades;
		coffeeManager.brewSpeed						= data.brewSpeed;
		coffeeManager.brewSpeedUpgPrice				= data.brewSpeedUpgPrice;
		coffeeManager.timerReduceAmount				= data.coffeeTimerReduceAmount;

		//// Customer Page Data
		customerManager.custCapacity				= data.custCapacity;
		customerManager.custCapacityUpgrades		= data.custCapacityUpgrades;
		customerManager.custCapacityUpgPrice		= data.custCapacityUpgPrice;
		customerManager.timerReduceAmount			= data.customerTimerReduceAmount;
		customerManager.serveSpeed					= data.serveSpeed;
		customerManager.serveSpeedUpgPrice			= data.serveSpeedUpgPrice;

		//CalculateOfflineGains();

	}

	public void SaveData(ref GameData data)
    {

		// General Player Data
		data.beanCnt						= this.beanCnt;
		data.totalEarnedBeans				= this.totalEarnedBeans;
		data.lifetimeEarnedBeans			= this.lifetimeEarnedBeans;
		data.coffee							= this.coffee;
		data.lifetimeEarnings				= this.lifetimeEarnings;
		data.money							= this.money;
		data.totalMoneyEarned				= this.totalMoneyEarned;
		data.beanDensity					= this.beanDensity;
		data.gems							= this.gems;
		data.prestigeLevel					= this.prestigeLevel;
		data.investors						= this.investors;
		data.lifetimeInvestors				= this.lifetimeInvestors;
		data.lastOnlineTime					= DateTime.Now.ToString("MM/dd/yyyyTHH:mm:ss");
		data.harvesters						= this.harvesters;
		data.x2MultUnlocked					= this.x2MultUnlocked;
		data.x12MultUnlocked				= this.x12MultUnlocked;
		data.finishedTutorial				= this.finishedTutorial;

		// Ad info
		data.doubleIncomeAdsWatched			= this.doubleIncomeAdsWatched;
		data.doubleIncomeActive				= this.doubleIncomeActive;
		data.doubleCustCapacityActive		= this.doubleCustCapacityActive;
		data.doubleBrewCapacityActive		= this.doubleBrewCapacityActive;
		data.remainingDoubleIncomeTime		= this.remainingDoubleIncomeTime;
		data.remainingDoubleBrewingTime		= this.remainingDoubleBrewingTime;
		data.remainingDoubleCustomerTime	= this.remainingDoubleCustomerTime;

		// Upgrade Page Data
		// // Upgrades
		data.brewTimeCostReducer			= this.brewTimeCostReducer;
		data.coffeeSellPriceInc				= this.coffeeSellPriceInc;
		data.growthCostReducer				= this.growthCostReducer;
		data.growthRateMultiplier			= this.growthRateMultiplier;
		data.investorEffectiveness			= this.investorEffectiveness;
		data.investorIncomeBoost			= this.investorIncomeBoost;
		data.sellTimeCostReducer			= this.sellTimeCostReducer;
		data.upgPriceReducer				= this.upgPriceReducer;
		// // Counts
		data.brewTimeCostReducerCount		= this.brewTimeCostReducerCount;
		data.coffeeSellPriceIncCount		= this.coffeeSellPriceIncCount;
		data.growthCostReducerCount			= this.growthCostReducerCount;
		data.growthRateMultiplierCount		= this.growthRateMultiplierCount;
		data.investorEffectivenessCount		= this.investorEffectivenessCount;
		data.investorIncomeBoostCount		= this.investorIncomeBoostCount;
		data.sellTimeCostReducerCount		= this.sellTimeCostReducerCount;
		data.upgPriceReducerCount			= this.upgPriceReducerCount;

		// Investor Page Data
		// // Upgrades
		//data.inv_growthRateMultiplier		= this.inv_growthRateMultiplier;
		data.inv_upgPriceReducer			= this.inv_upgPriceReducer;
		data.inv_freeBrewChance				= this.inv_freeBrewChance;
		data.inv_freeUpgChance				= this.inv_freeUpgChance;
		data.inv_profitBonus				= this.inv_profitBonus;
		data.inv_upgStartCnt				= this.inv_upgStartCnt;
		data.inv_speedPerTap				= this.inv_speedPerTap;
		
		// // Counts
		//data.inv_growthRateMultiplierCount	= this.inv_growthRateMultiplierCount;
		data.inv_upgPriceReducerCount		= this.inv_upgPriceReducerCount;
		data.inv_freeBrewChanceCount		= this.inv_freeBrewChanceCount;
		data.inv_freeUpgChanceCount			= this.inv_freeUpgChanceCount;
		data.inv_profitBonusCount			= this.inv_profitBonusCount;
		data.inv_upgStartCntCount			= this.inv_upgStartCntCount;
		data.inv_speedPerTapCount			= this.inv_speedPerTapCount;

		//// Coffee Brew Page Data
		data.brewCapacity					= coffeeManager.brewCapacity;
		data.brewCapacityUpgPrice			= coffeeManager.brewCapacityUpgPrice;
		data.brewCapacityUpgrades			= coffeeManager.brewCapacityUpgrades;
		data.brewSpeed						= coffeeManager.brewSpeed;
		data.brewSpeedUpgPrice				= coffeeManager.brewSpeedUpgPrice;	 
		data.coffeeTimerReduceAmount		= coffeeManager.timerReduceAmount;

		//// Customer Page Data
		data.custCapacity					= customerManager.custCapacity;
		data.custCapacityUpgrades			= customerManager.custCapacityUpgrades;
		data.custCapacityUpgPrice			= customerManager.custCapacityUpgPrice;
		data.customerTimerReduceAmount		= customerManager.timerReduceAmount;
		data.serveSpeed						= customerManager.serveSpeed;
		data.serveSpeedUpgPrice				= customerManager.serveSpeedUpgPrice;

	}

	public void CalculateOfflineGains(bool isWarpCalculation = false)
	{
		DateTime currentTime = DateTime.Now;
		offlineDuration = currentTime - lastOnlineTime;
		// The calculation doesn't accurately count for brew and sell time, just going to reduce it to balance the gains a bit
		//float gainsReduction = 0.15f;

		if(offlineDuration.TotalSeconds < 0)
        {
			Debug.Log("Woah there, something seems strange, did you invent a time machine? Wouldn't travel back anymore or your data may be deleted...");
        } else if(offlineDuration.TotalMinutes > 5 || isWarpCalculation)
        {
			double totalBeansPerMinute = 0;

			int unlockedBeans = 0;

			foreach (Bean bean in beans)
			{
				if (bean.unlocked)
				{
					double beansPerMinute = bean.CalculateProductionRate();
					//Debug.Log("beansPerMinute: " + beansPerMinute);
					totalBeansPerMinute += beansPerMinute;
					unlockedBeans++;
				}
			}

			int offlineMaxMinutes = (int)offlineMaxHours * 60;
			int totalMinutes = (int)Math.Floor(offlineDuration.TotalMinutes);

			if (isWarpCalculation)
            {
				int totalMinutesWarp = (24 * 60);
				double offlineBeanGains = totalBeansPerMinute * totalMinutesWarp;
				double offlineCoffeeGains = Mathf.Floor((float)(offlineBeanGains / coffeeManager.GetBeansPerCup()));
				offlineMoneyGains = Math.Round((offlineCoffeeGains * customerManager.GetSellPrice()) * gainsReduction);
			} else if (totalMinutes > offlineMaxMinutes)
            {
				// Gains without ad
				int totalMinutesWithoutAd = offlineMaxMinutes;
				double offlineBeanGains = totalBeansPerMinute * totalMinutesWithoutAd;
				double offlineCoffeeGains = Mathf.Floor((float)(offlineBeanGains / coffeeManager.GetBeansPerCup()));
				offlineMoneyGains = Math.Round((offlineCoffeeGains * customerManager.GetSellPrice()) * gainsReduction);

				// Gains with ad
				int offlineMaxMinutesWithAd = offlineMaxMinutes + 60;
				int totalMinutesWithAd = Math.Min(totalMinutes, offlineMaxMinutesWithAd);
				double offlineBeanGainsWithAd = totalBeansPerMinute * totalMinutesWithAd;
				double offineCoffeeGainsWithAd = Mathf.Floor((float)(offlineBeanGainsWithAd / coffeeManager.GetBeansPerCup()));
				offlineMoneyGainsWithAd = Math.Round((offineCoffeeGainsWithAd * customerManager.GetSellPrice()) * gainsReduction);

				// Update display
				int timeDifference = totalMinutesWithAd - totalMinutesWithoutAd;
				offlineCounterTxt.text = "You were offline for \n" + offlineDuration.ToString(@"hh\:mm");
				earningsTxt.text = "You earned \n$" + offlineMoneyGains + " \nWhile you were away!";
				watchAdTxt.gameObject.SetActive(true);
				offlineWatchAdBtn.SetActive(true);
				watchAdTxt.text = "Watch an ad for extra earnings! + " + timeDifference + "min \n($" + offlineMoneyGainsWithAd + ")";
			} else
            {
				double offlineBeanGains = totalBeansPerMinute * totalMinutes;
				double offlineCoffeeGains = Mathf.Floor((float)(offlineBeanGains / coffeeManager.GetBeansPerCup()));
				offlineMoneyGains = Math.Round((offlineCoffeeGains * customerManager.GetSellPrice()) * 0.25);
				offlineCounterTxt.text = "You were offline for \n" + offlineDuration.ToString(@"hh\:mm");
				earningsTxt.text = "You earned \n$" + offlineMoneyGains + " \nWhile you were away!";
				watchAdTxt.gameObject.SetActive(false);
				offlineWatchAdBtn.SetActive(false);
			}

			// Set it to the inverse of isWarpCalculation so that it doesn't unnecessarily open the offline panel
			offlineEarningsPanel.SetActive(!isWarpCalculation);


   //         Debug.Log("DateTime.Now: " + currentTime);
   //         Debug.Log("Unlocked Beans: " + unlockedBeans);
   //         Debug.Log("lastOnlineTime: " + lastOnlineTime);
   //         Debug.Log("Offline Duration: " + offlineDuration);
   //         Debug.Log("TotalBeansPerMinute: " + totalBeansPerMinute);
   //         Debug.Log("TotalMinutes: " + totalMinutes);
			//Debug.Log("Offline Gains: " + offlineBeanGains);
   //         Debug.Log("offlineCoffeeGains: " + offlineCoffeeGains);
   //         Debug.Log("offlineMoneyGains: " + offlineMoneyGains);

		}

	}

	public void OfflineContinueBtn()
	{
		AddMoney(offlineMoneyGains);
		offlineEarningsPanel.SetActive(false);
	}

	public void HideOfflinePanel()
	{
		offlineEarningsPanel.SetActive(false);
	}

	public void AddToBeanScriptList(Bean b)
	{
		beans.Add(b);
	}

	public void AddMoney(double amount)
	{
		if (doubleIncomeActive) amount = amount * 2;
		money += amount;
		totalMoneyEarned += amount;
		lifetimeEarnings += amount;
		// Update the money UI display
		//UpdateMoneyText();
	}

	public void AddBeans(double amount)
	{
		beanCnt += amount;
		totalEarnedBeans += amount;
		lifetimeEarnedBeans += amount;

		// Update the money UI display
		//UpdateMoneyText();
	}

	public void AddCoffee(double amount)
    {
		coffee += amount;
    }

	public void SpendMoney(double amount)
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

	public void SpendGems(int amount)
    {
		gems -= amount;
    }

	public void PrestigeReset()
	{
		money = 10;
		beanCnt = 0;
		totalEarnedBeans = 0;
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

	public void ClearBeans()
    {
		beans.Clear();
    }

	public void AddInvUpgStartCount(int amount = 1)
	{
		inv_upgStartCnt += amount;
		inv_upgStartCntCount++;
	}

	public void AddInvProfitBonus(int p = 1)
	{
		inv_profitBonus += p;
		inv_profitBonusCount++;
	}

	public void AddInvestors(long i)
	{
		investors += i;
		lifetimeInvestors += i;
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

	public void ActivateX2Mult()
    {
		x2MultUnlocked = true;
    }

	public void ActivateX12Mult()
	{
		x12MultUnlocked = true;
	}



	public void AddInvUpgPriceReducer(float c = 0.05f)
	{
		inv_upgPriceReducer += c;
		inv_upgPriceReducerCount++;
    }
	public void AddGrowthRateMultiplier(float g = 0.05f)
	{
		growthRateMultiplier += g;
		growthRateMultiplierCount++;
	}
	public void AddInvGrowthRateMultiplier(float g)
	{
		inv_growthRateMultiplier += g;
		inv_growthRateMultiplierCount++;
	}

    public void AddInvSpeedPerTap(float s = 0.05f)
    {
        inv_speedPerTap += s;
		inv_speedPerTapCount++;
    }

    public void AddInvFreeUpgChance(int f = 1)
    {
		inv_freeUpgChance += f;
		inv_freeUpgChanceCount++;
    }

	public void AddInvFreeBrewChance(int b = 1)
    {
		inv_freeBrewChance += b;
		inv_freeBrewChanceCount++;
    }

	public void AddInvFreeSellChance(int s = 1)
    {
		inv_freeSellChance += s;
		inv_freeSellChanceCount++;
    }

	public void AddGrowthCostReducer(float g = 0.01f)
	{
		growthCostReducer += g;
		growthCostReducerCount++;
	}

	public void AddBeanDensity(int b = 1)
	{
		beanDensity += b;
	}

	public void ReduceBrewTimeCost(float b = 0.01f)
	{
		brewTimeCostReducer += b;
		brewTimeCostReducerCount++;
	}

	public void ReduceSellTimeCost(float s = 0.01f)
	{
		sellTimeCostReducer += s;
		sellTimeCostReducerCount++;
	}

	public void AddCoffeeSellPrice(int c = 1)
	{
		coffeeSellPriceInc += c;
		coffeeSellPriceIncCount++;
	}

	public void StartDoubleCustomerCapacity()
    {
		doubleCustCapacityActive = true;
		remainingDoubleCustomerTime = 5 * 60;
		StartCoroutine(DoubleCustCapacityTimer());
	}

	public IEnumerator DoubleCustCapacityTimer()
	{

		while (remainingDoubleCustomerTime > 0)
		{
			yield return new WaitForSeconds(1f);
			remainingDoubleCustomerTime -= 1f;
		}

		doubleCustomerCapacityAd.ShowAdBtn();
		doubleCustCapacityActive = false;
	}

	//public IEnumerator DoubleCustCapacityTimer(int timer)
 //   {
	//	yield return new WaitForSeconds(timer);
	//	doubleCustomerCapacityAd.ShowAdBtn();
	//	doubleCustCapacityActive = false;
	//}

	public void StartDoubleBrewingCapacity()
	{
		doubleBrewCapacityActive = true;
		remainingDoubleBrewingTime = 5 * 60;
		StartCoroutine(DoubleBrewingCapacityTimer());
	}

	public IEnumerator DoubleBrewingCapacityTimer()
	{

		while (remainingDoubleBrewingTime > 0)
		{
			yield return new WaitForSeconds(1f);
			remainingDoubleBrewingTime -= 1f;
		}

		doubleBrewingCapacityAd.ShowAdBtn();
		doubleBrewCapacityActive = false;
	}

	//public IEnumerator DoubleBrewingCapacityTimer(int timer)
	//{
	//	yield return new WaitForSeconds(timer);
	//	doubleBrewingCapacityAd.ShowAdBtn();
	//	doubleBrewCapacityActive = false;
	//}

	public void startDoubleIncome()
    {
		if(doubleIncomeAdsWatched < 4)
        {
			if(!doubleIncomeActive)
			{
				doubleIncomeActive = true;
				remainingDoubleIncomeTime = 30 * 60;
				doubleIncomeCoroutine = StartCoroutine(DoubleIncomeTimer());
			} else
			{
				AddDoubleIncomeTime();
			}

			doubleIncomeAdsWatched++;
        }
    }

	public IEnumerator DoubleIncomeTimer()
    {

		while (remainingDoubleIncomeTime > 0)
		{
			yield return new WaitForSeconds(1f);
			remainingDoubleIncomeTime -= 1f;
			if(remainingDoubleIncomeTime%1800 == 0)
            {
				doubleIncomeAdsWatched--;
            }
		}

		doubleIncomeActive = false;
	}

	public void AddDoubleIncomeTime()
    {

		remainingDoubleIncomeTime += 30 * 60;

        if (doubleIncomeCoroutine == null)
        {
            doubleIncomeCoroutine = StartCoroutine(DoubleIncomeTimer());
        }
    }

}
