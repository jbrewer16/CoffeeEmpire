using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheatsManager : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject cheatPanel;
    public TMP_InputField amountInput;

    private double GetAmount()
    {
        double enteredValue;
        double.TryParse(amountInput.text, out enteredValue);
        return enteredValue;
    }

    private int GetAmountInt()
    {
        int enteredValue;
        int.TryParse(amountInput.text, out enteredValue);
        return enteredValue;
    }

    public void AddCash()
    {
        gameManager.AddMoney(GetAmount());
    }

    public void AddBeans()
    {
        gameManager.AddBeans(GetAmount());
    }

    public void AddCoffee()
    {
        gameManager.AddCoffee(GetAmount());
    }

    public void AddGems()
    {
        gameManager.AddGems(GetAmountInt());
    }

    public void OpenCheatPanel()
    {
        cheatPanel.SetActive(true);
    }

    public void CloseCheatPanel()
    {
        cheatPanel.SetActive(false);
    }

}
