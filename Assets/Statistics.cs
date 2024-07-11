using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Statistics : MonoBehaviour
{

    public GameManager gameManager;

    public TMP_Text sessionEarningsText;
    public TMP_Text lifetimeEarningsText;
    public TMP_Text sessionBeansText;
    public TMP_Text lifetimeBeansText;
    public TMP_Text totalInvestorsText;
    public TMP_Text totalPrestigesText;
    public TMP_Text cashPerMinText;

    void Start()
    {
        
    }

    void Update()
    {

        gameManager.CalculateOfflineGains(true);
        double cashEarnings = gameManager.offlineMoneyGains / gameManager.gainsReduction;

        sessionEarningsText.text = "" + GlobalFunctions.FormatNumber(gameManager.totalMoneyEarned, true);
        lifetimeEarningsText.text = "" + GlobalFunctions.FormatNumber(gameManager.lifetimeEarnings, true);
        sessionBeansText.text = "" + GlobalFunctions.FormatNumber(gameManager.totalEarnedBeans);
        lifetimeBeansText.text = "" + GlobalFunctions.FormatNumber(gameManager.lifetimeEarnedBeans);
        totalInvestorsText.text = "" + GlobalFunctions.FormatNumber(gameManager.lifetimeInvestors);
        totalPrestigesText.text = "" + GlobalFunctions.FormatNumber(gameManager.prestigeLevel);
        cashPerMinText.text = "" + GlobalFunctions.FormatNumber(cashEarnings, true);
    }
}
