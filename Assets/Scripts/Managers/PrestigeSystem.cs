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
    public long moneyThreshold;
    public long claimableInvestors;
    public float beanMultiplier;
    public bool canAccumulateInvestors;
    private GameManager gameManager;

    public void Start()
    {
        moneyThreshold = 1000000000000;
        claimableInvestors = 0;
        beanMultiplier = 0;
        canAccumulateInvestors = false;

        gameManager = gameManagerObject.GetComponent<GameManager>();

        updateText();
    }

    public void Update()
    {
        if(!canAccumulateInvestors && gameManager.lifetimeEarnings >= moneyThreshold)
        {
            canAccumulateInvestors = true;
        } else if(canAccumulateInvestors && gameManager.lifetimeEarnings >= moneyThreshold){
            claimableInvestors = CalculateInvestorsAwarded(gameManager.lifetimeEarnings);
        }

        updateText();

    }

    public void Prestige()
    {
        long investorsAwarded = CalculateInvestorsAwarded(gameManager.totalMoneyEarned);
        beanMultiplier = CalculateBeanMultiplier(claimableInvestors);

        //claimableInvestors += investorsAwarded;
        awardInvestors();
        gameManager.PrestigeReset();

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
