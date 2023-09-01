using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{

    public float money;
	public float totalMoneyEarned;
	public float investorEffectiveness;
	public float investorIncomeBoost;
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

	public List<BeanData> beans;
	//public BeanSaveData<BeanData> beansData = new BeanSaveData<BeanData>(beans);

	public GameData()
    {
        this.money = 10;
		this.totalMoneyEarned = 0;
		this.investorEffectiveness = 0.02f;
		this.investorIncomeBoost = 0;
		this.growthRateMultiplier = 1.00f;
		this.inv_growthRateMultiplier = 1.00f;
		this.growthCostReducer = 0;
		this.upgPriceReducer = 0;
		this.brewTimeCostReducer = 0;
		this.sellTimeCostReducer = 0;
		this.coffeeSellPriceInc = 0;
		this.inv_coffeeSellPriceInc = 0;
		this.inv_speedPerTap = 0;
		this.inv_profitBonus = 0;
		this.inv_freeUpgChance = 0;
		this.inv_freeBrewChance = 0;
		this.inv_upgPriceReducer = 0;
		this.beanDensity = 0;
		this.beanCnt = 0;
		this.coffee = 0;
		this.gems = 0;
		this.prestigeLevel = 0;
		this.investors = 0;
		this.inv_upgStartCnt = 0;


		this.beans = new List<BeanData>();
		fillBeans();
	}

	void fillBeans()
	{
		beans.Add(new BeanData("Arabica", 1, 2, 1, 0));
		beans.Add(new BeanData("Espresso", 16, 20, 3, 250));
		beans.Add(new BeanData("Mocha", 32, 40, 5, 500));
		beans.Add(new BeanData("Chocolate", 64, 75, 10, 2000));
		beans.Add(new BeanData("Irish Cream", 128, 200, 25, 5000));
		beans.Add(new BeanData("Cinnamon", 256, 500, 45, 10000));
		beans.Add(new BeanData("Maple", 512, 1000, 90, 25000));
		beans.Add(new BeanData("Pumpkin Spice", 1024, 2500, 120, 50000));
		beans.Add(new BeanData("Lava", 2048, 5000, 180, 150000));
		beans.Add(new BeanData("Radioactive", 4096, 15000, 300, 500000));
		beans.Add(new BeanData("Magical", 8192, 30000, 420, 750000));
		beans.Add(new BeanData("Quantum", 16384, 75000, 600, 1500000));
		beans.Add(new BeanData("Galactic", 32768, 250000, 900, 3750000));
		beans.Add(new BeanData("Cosmic", 65536, 500000, 1500, 10000000));
		beans.Add(new BeanData("Time Warp", 131072, 1000000, 1800, 25000000));
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