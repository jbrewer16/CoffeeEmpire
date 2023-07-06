using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeSystem : MonoBehaviour
{
    private int currentMoney;
    private int totalMoneyEarned;
    private int totalInvestorsEarned;
    private float incomeMultiplier;

    public void Prestige()
    {
        int investorsAwarded = CalculateInvestorsAwarded(totalMoneyEarned);
        incomeMultiplier = CalculateIncomeMultiplier(totalInvestorsEarned);

        totalInvestorsEarned += investorsAwarded;
        totalMoneyEarned = 0;
        currentMoney = 0;

        Debug.Log("Prestige Successful!");
        Debug.Log("Investors Awarded: " + investorsAwarded);
        Debug.Log("Income Multiplier: " + incomeMultiplier);
        Debug.Log("Current Money: " + currentMoney);
    }

    private int CalculateInvestorsAwarded(int totalMoneyEarned)
    {
        return (int)(totalMoneyEarned * 0.1f); // 10% of total money earned
    }

    private float CalculateIncomeMultiplier(int totalInvestorsEarned)
    {
        return 1 + (totalInvestorsEarned * 0.05f); // 5% increase per investor earned
    }
}
