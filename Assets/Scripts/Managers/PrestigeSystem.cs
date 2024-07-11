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
    public double moneyThreshold;
    public long claimableInvestors;
    public float beanMultiplier;
    public bool canAccumulateInvestors;
    private GameManager gameManager;

    public void Start()
    {
        moneyThreshold = 1e9;
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
        if(canAccumulateInvestors && gameManager.totalMoneyEarned < moneyThreshold)
        {
            canAccumulateInvestors = false;
            claimableInvestors = 0;
        }

        gameManager.CalculateInvestorBoost();
        updateText();

    }

    public void Prestige()
    {
        if(claimableInvestors > 0)
        {
            long investorsAwarded = CalculateInvestorsAwarded(gameManager.totalMoneyEarned);
            beanMultiplier = CalculateBeanMultiplier(claimableInvestors);

            //claimableInvestors += investorsAwarded;
            awardInvestors();
            gameManager.PrestigeReset();
        } else
        {
            Debug.Log("There are no investors to claim!");
        }

    }

    private void awardInvestors()
    {
        gameManager.AddInvestors(claimableInvestors);
        claimableInvestors = 0;
    }

    private void updateText()
    {
        totalInvestorsTxt.text = "" + GlobalFunctions.FormatNumber(gameManager.investors);
        beanMultiplierTxt.text = "" + (GlobalFunctions.FormatNumber((gameManager.investorIncomeBoost * 100)) + "%");
        claimableInvestorsTxt.text = "" + GlobalFunctions.FormatNumber(claimableInvestors);
    }

    private long CalculateInvestorsAwarded(double lifetimeEarnings)
    {
        long investorsToAward = (long)(150 * Mathf.Sqrt((float)(lifetimeEarnings / Mathf.Pow(10, 12))));
        return investorsToAward;
    }

    private float CalculateBeanMultiplier(long totalInvestorsEarned)
    {
        return 1 + (totalInvestorsEarned * 0.02f); // 2% increase per investor earned
    }
}
