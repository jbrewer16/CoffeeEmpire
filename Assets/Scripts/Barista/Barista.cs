using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Barista : MonoBehaviour
{

    public int cost;
    public int id;
    public string baristaName;
    public string beanName;
    public bool bought;
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
        if (this.gameObject.activeSelf)
        {
            if(gameManager.money >= cost && bean.unlocked)
            {
                buyButton.interactable = true;
            }
            if (bought)
            {
                this.gameObject.SetActive(false);
                bean.SetManager(true);
            }
        }
    }

    public void buyBarista()
    {
        if(gameManager.money >= cost)
        {
            gameManager.SpendMoney(cost);
            this.gameObject.SetActive(false);
            bean.SetManager(true);
            this.gameManager.baristas.Add(id);
        }
    }

}
