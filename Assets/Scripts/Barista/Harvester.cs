using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Harvester : MonoBehaviour
{

    public double cost;
    public int id;
    public string harvesterName;
    public string beanName;
    public bool bought;
    public TMP_Text sellsBeanTxt;
    public TMP_Text costTxt;
    public TMP_Text harvesterNameTxt;
    public Bean bean;
    public GameObject gameManagerObj;
    public Button buyButton;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        sellsBeanTxt.text = "Harvests " + beanName;
        costTxt.text = GlobalFunctions.FormatNumber(cost, true);
        harvesterNameTxt.text = harvesterName;
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

    public void buyHarvester()
    {
        if(gameManager.money >= cost)
        {
            gameManager.SpendMoney(cost);
            this.gameObject.SetActive(false);
            bean.SetManager(true);
            this.gameManager.harvesters.Add(id);
        }
    }

}
