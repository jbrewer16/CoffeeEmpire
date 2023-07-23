using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvestorUpgrade : MonoBehaviour
{

    public int cost;
    // 0 = Profit multiplier 1 = Investor Effectiveness 2 = Bean Cost reducer
    public int upgType;
    public int multiplier;
    public float invEffectiveness;
    public float costReducer;
    public string upgName;

    public TMP_Text beanToUpgTxt;
    public TMP_Text costTxt;
    public Bean bean;
    public GameObject gameManagerObj;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        costTxt.text = "$" + cost;
        gameManager = gameManagerObj.GetComponent<GameManager>();
    }

    public void buyUpgrade()
    {
        if (gameManager.money >= cost)
        {
            gameManager.SpendMoney(cost);
            this.gameObject.SetActive(false);
            switch (upgType)
            {
                case 0:
                    upgradeAllProfits(multiplier);
                    break;
                case 1:
                    upgradeInvestorEffectiveness(invEffectiveness);
                    break;
                case 2:
                    upgradeCostReducer(costReducer);
                    break;
            }
        }
    }

    public void upgradeAllProfits(int beanMultiplier)
    {
        for(int i = 0; i < gameManager.beans.Count; i++)
        {
            gameManager.beans[i].SetUpgradeMultiplier(gameManager.beans[i].GetUpgradeMultiplier() + beanMultiplier);
        }
    }

    public void upgradeInvestorEffectiveness(float eff)
    {
        gameManager.investorEffectiveness += eff;
    }

    public void upgradeCostReducer(float cR)
    {
        gameManager.growthCostReducer += cR;
    }

}
