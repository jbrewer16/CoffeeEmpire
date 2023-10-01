using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrestigeSystem : MonoBehaviour
{

    public GameObject gameManagerObject;
    public TMP_Text totalInvestorsTxt;
    public TMP_Text beanMultiplierTxt;
    public TMP_Text claimableInvestorsTxt;

    // Minimum money needed for investors to start accumulating
    public int moneyThreshold;
    public int claimableInvestors;
    public float beanMultiplier;
    public bool canAccumulateInvestors;
    private GameManager gameManager;

    public void Start()
    {
        moneyThreshold = 1000000;
        claimableInvestors = 0;
        beanMultiplier = 0;
        canAccumulateInvestors = false;

        gameManager = gameManagerObject.GetComponent<GameManager>();

        updateText();
    }

    public void Update()
    {
        if(!canAccumulateInvestors && gameManager.totalMoneyEarned >= moneyThreshold)
        {
            canAccumulateInvestors = true;
        } else if(canAccumulateInvestors && gameManager.totalMoneyEarned >= moneyThreshold){
            claimableInvestors = CalculateInvestorsAwarded(gameManager.totalMoneyEarned);
        }

        updateText();

    }

    public void Prestige()
    {
        int investorsAwarded = CalculateInvestorsAwarded(gameManager.totalMoneyEarned);
        beanMultiplier = CalculateBeanMultiplier(claimableInvestors);

        //claimableInvestors += investorsAwarded;
        awardInvestors();
        gameManager.prestigeReset();

    }

    private void awardInvestors()
    {
        gameManager.AddInvestors(claimableInvestors);
        claimableInvestors = 0;
    }

    private void updateText()
    {
        totalInvestorsTxt.text = "" + gameManager.investors;
        beanMultiplierTxt.text = "" + (gameManager.investorIncomeBoost * 100) + "%";
        claimableInvestorsTxt.text = "" + claimableInvestors;
    }

    private int CalculateInvestorsAwarded(double totalMoneyEarned)
    {
        return (int)(Mathf.Round((float)(totalMoneyEarned * 0.00001f))); // 0.001% of total money earned
    }

    private float CalculateBeanMultiplier(int totalInvestorsEarned)
    {
        return 1 + (totalInvestorsEarned * 0.02f); // 2% increase per investor earned
    }
}
