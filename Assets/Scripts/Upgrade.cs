using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{

    public int cost;
    public string beanName;
    public TMP_Text beanToUpgTxt;
    public TMP_Text costTxt;
    public Bean bean;
    public GameObject gameManagerObj;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        beanToUpgTxt.text = beanName + " profit x3";
        costTxt.text = "$" + cost;
        gameManager = gameManagerObj.GetComponent<GameManager>();
    }

    public void buyUpgrade()
    {
        if (gameManager.money >= cost)
        {
            gameManager.SpendMoney(cost);
            this.gameObject.SetActive(false);
            bean.SetUpgradeMultiplier(bean.GetUpgradeMultiplier() + 3);
        }
    }

}
