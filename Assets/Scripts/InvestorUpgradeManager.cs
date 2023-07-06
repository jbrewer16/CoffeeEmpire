using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestorUpgradeManager : MonoBehaviour
{
    public List<InvestorUpgData> investorUpgrades = new List<InvestorUpgData>();
    public GameObject gameManager;
    public GameObject investorUpgIUPrefab;
    public Transform investorUpgUIContainer;

    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {

        gameManagerScript = gameManager.GetComponent<GameManager>();

        fillInvestorUpgs();
        createUpgradeUI();
    }

    void fillInvestorUpgs()
    {
        investorUpgrades.Add(new InvestorUpgData(5000, 3, "All Profits x3"));
        investorUpgrades.Add(new InvestorUpgData(10000, 1, 0.02f, "Investor Effectiveness * 2%"));
        investorUpgrades.Add(new InvestorUpgData(50000, 2, 0.05f, "Beans 5% Cheaper"));
    }

    void createUpgradeUI()
    {

        float offsetY = 0f; // Offset value to position each UI object vertically
        float spacing = 30f; // Spacing between UI objects

        for (int i = 0; i < investorUpgrades.Count; i++)
        {
            InvestorUpgData upgradeData = investorUpgrades[i];
            GameObject newUpgradeUI = Instantiate(investorUpgIUPrefab, investorUpgUIContainer);

            // Adjust the position of the instantiated UI object
            RectTransform rectTransform = newUpgradeUI.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0f, 1f); // Anchor to top-left corner
            rectTransform.anchorMax = new Vector2(0f, 1f);
            rectTransform.pivot = new Vector2(0f, 1f);
            rectTransform.anchoredPosition = new Vector2(0f, offsetY);

            InvestorUpgrade upgradeScript = newUpgradeUI.GetComponent<InvestorUpgrade>();
            upgradeScript.cost = upgradeData.cost;
            upgradeScript.gameManagerObj = this.gameManager;

            // Increase the offsetY value to position the next UI object with spacing
            offsetY -= rectTransform.rect.height + spacing;

        }

    }
}

public class InvestorUpgData
{

    public int cost;
    // 0 = Profit multiplier 1 = Investor Effectiveness 2 = Bean Cost reducer
    public int upgType;
    public int multiplier;
    public float invEffectiveness;
    public float costReducer;
    public string upgName;

    //Profit Multiplier
    public InvestorUpgData(int c, int m, string u)
    {
        cost = c;
        multiplier = m;
        upgName = u;
    }

    //Profit Multiplier
    public InvestorUpgData(int c, int uT, float i, string u)
    {
        cost = c;
        if(uT == 1)
        {
            invEffectiveness = i;
        } else if(uT == 2)
        {
            costReducer = i;
        }
        upgName = u;
    }

}