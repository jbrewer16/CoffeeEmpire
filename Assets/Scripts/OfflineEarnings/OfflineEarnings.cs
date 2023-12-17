using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineEarnings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	//public void CalculateOfflineGains()
	//{
	//	DateTime currentTime = DateTime.Now;
	//	offlineDuration = currentTime - lastOnlineTime;

	//	double totalBeansPerMinute = 0;

	//	int unlockedBeans = 0;

	//	foreach (Bean bean in beans)
	//	{
	//		if (bean.unlocked)
	//		{
	//			double beansPerMinute = bean.CalculateProductionRate();
	//			Debug.Log("beansPerMinute: " + beansPerMinute);
	//			totalBeansPerMinute += beansPerMinute;
	//			unlockedBeans++;
	//		}
	//	}

	//	int totalMinutes = (int)Math.Floor(offlineDuration.TotalMinutes);

	//	double offlineBeanGains = totalBeansPerMinute * totalMinutes;

	//	double offlineCoffeeGains = Mathf.Floor((float)(offlineBeanGains / coffeeManager.GetBeansPerCup()));

	//	offlineMoneyGains = offlineCoffeeGains * customerManager.GetSellPrice();

	//	Debug.Log("DateTime.Now: " + currentTime);
	//	Debug.Log("Unlocked Beans: " + unlockedBeans);
	//	Debug.Log("lastOnlineTime: " + lastOnlineTime);
	//	Debug.Log("Offline Duration: " + offlineDuration);
	//	Debug.Log("TotalBeansPerMinute: " + totalBeansPerMinute);
	//	Debug.Log("TotalMinutes: " + totalMinutes);
	//	Debug.Log("Offline Gains: " + offlineBeanGains);
	//	Debug.Log("offlineCoffeeGains: " + offlineCoffeeGains);
	//	Debug.Log("offlineMoneyGains: " + offlineMoneyGains);

	//	offlineCounterTxt.text = "You were offline for \n" + offlineDuration;
	//	earningsTxt.text = "You earned \n$" + offlineMoneyGains + " \nWhile you were away!";
	//	watchAdTxt.text = "Watch an ad for double earnings! \n($" + (offlineMoneyGains * 2) + ")";

	//	offlineEarningsPanel.SetActive(true);

	//}

	//public void OfflineContinueBtn()
	//{
	//	AddMoney(offlineMoneyGains);
	//	offlineEarningsPanel.SetActive(false);
	//}

	//public void OfflineAdButton()
	//{
	//	AddMoney(offlineMoneyGains * 2);
	//	offlineEarningsPanel.SetActive(false);
	//}

}
