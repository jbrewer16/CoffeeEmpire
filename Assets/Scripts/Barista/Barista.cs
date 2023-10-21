using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Barista : MonoBehaviour
{

    public int cost;
    public string baristaName;
    public string beanName;
    public TMP_Text sellsBeanTxt;
    public TMP_Text costTxt;
    public TMP_Text baristaNameTxt;
    public Bean bean;
    public GameObject gameManagerObj;
    public Button buyButton;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        sellsBeanTxt.text = "Sells " + beanName;
        costTxt.text = GlobalFunctions.FormatNumber(cost, true);
        baristaNameTxt.text = baristaName;
        gameManager = gameManagerObj.GetComponent<GameManager>();
        buyButton.interactable = false;
    }

    private void Update()
    {
        if(gameManager.money >= cost && bean.unlocked)
        {
            buyButton.interactable = true;
        }
    }

    public void buyBarista()
    {
        if(gameManager.money >= cost)
        {
            gameManager.SpendMoney(cost);
            this.gameObject.SetActive(false);
            bean.SetManager(true);
        }
    }

}
