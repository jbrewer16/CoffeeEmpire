using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{

	// General Player Data
	public double beanCnt;
	public double coffee;
	public double lifetimeEarnings;
    public double money;
	public double totalMoneyEarned;
	public int beanDensity;
	public int gems;
	public int prestigeLevel;
	public long investors;
	public string lastOnlineTime;
	public List<BeanData> beans;
	public List<int> baristas;

	// Upgrade Page Data
	// // Upgrades
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
    public float inv_speedPerTap;
    public float inv_upgPriceReducer;
	public int inv_freeBrewChance;
	public int inv_freeUpgChance;
	public int inv_profitBonus;
	public int inv_upgStartCnt;
	//public float inv_coffeeSellPriceInc;
	// // Counts
	public int inv_freeBrewChanceCount;
	public int inv_freeUpgChanceCount;
	public int inv_growthRateMultiplierCount;
	public int inv_profitBonusCount;
    public int inv_speedPerTapCount;
    public int inv_upgPriceReducerCount;
	public int inv_upgStartCntCount;
	//public int inv_coffeeSellPriceIncCount;

	// Coffee Brew Page Data
	public int brewCapacity;
	public int brewCapacityUpgrades;
	public float brewCapacityUpgPrice;
	public float brewSpeedUpgPrice;
	public float coffeeTimerReduceAmount;

	// Customer Page Data
	public int custCapacity;
	public int custCapacityUpgrades;
	public float custCapacityUpgPrice;
	public float customerTimerReduceAmount;
	public float serveSpeedUpgPrice;


	public GameData()
    {
		InitializeData();
	}

	/// <summary>
	/// Fills the initial data for the Beans
	/// </summary>
	void fillBeans()
	{
		beans.Add(new BeanData(1, "Arabica",		1,		4,			1,		0,			1.07f));
		beans.Add(new BeanData(2, "Espresso",		16,		20,			3,		250,		1.15f));
		beans.Add(new BeanData(3, "Mocha",			32,		40,			5,		500,		1.14f));
		beans.Add(new BeanData(4, "Chocolate",		64,		75,			10,		2000,		1.13f));
		beans.Add(new BeanData(5, "Irish Cream",	128,	200,		25,		15000,		1.12f));
		beans.Add(new BeanData(6, "Cinnamon",		256,	500,		45,		5e4,		1.11f));
		beans.Add(new BeanData(7, "Maple",			512,	1000,		90,		2.5e6,		1.10f));
		beans.Add(new BeanData(8, "Pumpkin Spice",	1024,	2500,		120,	5e8,		1.09f));
		beans.Add(new BeanData(9, "Lava",			2048,	5000,		180,	1.5e10,		1.08f));
		beans.Add(new BeanData(10,"Radioactive",	4096,	15000,		300,	5e12,		1.07f));
		beans.Add(new BeanData(11,"Magical",		8192,	30000,		420,	7e14,		1.06f));
		beans.Add(new BeanData(12,"Quantum",		16384,	75000,		600,	1.5e16,		1.05f));
		beans.Add(new BeanData(13,"Galactic",		32768,	250000,		900,	3e18,		1.04f));
		beans.Add(new BeanData(14,"Cosmic",			65536,	500000,		1500,	1e20,		1.03f));
		beans.Add(new BeanData(15,"Time Warp",		131072, 1000000,	1800,	2e22,		1.02f));
	}

	/// <summary>
	/// Resets all prestige items back to the initial state
	/// </summary>
	public void ResetGame()
	{
		InitializeData();
	}

	private void InitializeData()
	{
		//Debug.Log("Initializing Data!!!!!");
		// General Player Data
		this.beanCnt = 0;
		this.coffee = 0;
		this.lifetimeEarnings = 10;
		this.money = 10;
		this.totalMoneyEarned = 10;
		this.beanDensity = 0;
		this.gems = 0;
		this.prestigeLevel = 0;
		this.investors = 0;
		this.lastOnlineTime = DateTime.Now.ToString("MM/dd/yyyyTHH:mm:ss");
		this.beans = new List<BeanData>();
		this.baristas = new List<int>();

		// Upgrade Page Data
		// // Upgrades
		this.brewTimeCostReducer = 0;
		this.coffeeSellPriceInc = 0;
		this.growthCostReducer = 0;
		this.growthRateMultiplier = 1.00f;
		this.investorEffectiveness = 0.02f;
		this.investorIncomeBoost = 0;
		this.sellTimeCostReducer = 0;
		this.upgPriceReducer = 0;
		// // Counts
		this.brewTimeCostReducerCount = 1;
		this.coffeeSellPriceIncCount = 1;
		this.growthCostReducerCount = 1;
		this.growthRateMultiplierCount = 1;
		this.investorEffectivenessCount = 1;
		this.investorIncomeBoostCount = 1;
		this.sellTimeCostReducerCount = 1;
		this.upgPriceReducerCount = 1;

		// Investor Page Data
		// // Upgrades
		this.inv_growthRateMultiplier = 1.00f;
		this.inv_speedPerTap = 0;
		this.inv_upgPriceReducer = 0;
		this.inv_freeBrewChance = 0;
		this.inv_freeUpgChance = 0;
		this.inv_profitBonus = 0;
		this.inv_upgStartCnt = 0;
		//public float inv_coffeeSellPriceInc;
		// // Counts
		this.inv_freeBrewChanceCount = 1;
		this.inv_freeUpgChanceCount = 1;
		this.inv_growthRateMultiplierCount = 1;
		this.inv_profitBonusCount = 1;
		this.inv_speedPerTapCount = 1;
		this.inv_upgPriceReducerCount = 1;
		this.inv_upgStartCntCount = 1;
		//public int inv_coffeeSellPriceIncCount;

		// Coffee Brew Page Data
		this.brewCapacity = 1;
		this.brewCapacityUpgrades = 1;
		this.brewCapacityUpgPrice = 50;
		this.brewSpeedUpgPrice = 1000;
		this.coffeeTimerReduceAmount = 0.25f;

		// Customer Page Data
		this.custCapacity = 1;
		this.custCapacityUpgrades = 1;
		this.custCapacityUpgPrice = 50;
		this.customerTimerReduceAmount = 0.25f;
		this.serveSpeedUpgPrice = 1000;

		fillBeans();
	}

}

[System.Serializable]
public class BeanSaveData<T>
{
	public List<T> list;

	public BeanSaveData(List<T> data)
    {
		this.list = data;
    }
}