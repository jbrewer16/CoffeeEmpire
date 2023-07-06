using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<Bean> beans = new List<Bean>();
    public float money = 0;
    public float investorEffectiveness = 0.001f;
    public float costReducer = 0f;
    public int beanCnt = 0;
    public int coffee = 0;
    public int gems = 0;
    public int prestigeLevel = 0;
    public int investors = 0;

    // Track unlocked coffee beans
    private bool[] unlockedBeans;
    // Reference to UI text for displaying money and gems
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text beanCntText;
    [SerializeField] private TMP_Text coffeeCntText;
    [SerializeField] private TMP_Text gemsText;

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize base stats
        money = 10;
        gems = 0;
        prestigeLevel = 1;

        // Initialize unlocked beans array (assuming there are 10 beans in total)
        unlockedBeans = new bool[10];

        // Unlock the first bean at the start of the game
        UnlockBean(0);

        // Update the money and gems UI display
        updateText();
    }

    public void Update()
    {
        // Update the money and gems UI display
        updateText();
    }

    public void updateText()
    {
        // Update the gems UI text with the current gems value
        beanCntText.text = "Beans: " + beanCnt.ToString();
        // Update the money UI text with the current money value
        moneyText.text = "Money: " + money.ToString();
        // Update the gems UI text with the current gems value
        gemsText.text = "Gems: " + gems.ToString();
        // Update the gems UI text with the current gems value
        coffeeCntText.text = "Coffee: " + coffee.ToString() + "c";

    }

    public void AddToBeanScriptList(Bean b)
    {
        beans.Add(b);
    }

    public void AddMoney(float amount)
    {
        money += amount;

        // Update the money UI display
        //UpdateMoneyText();
    }

    public void AddBeans(int amount)
    {
        beanCnt += amount;

        // Update the money UI display
        //UpdateMoneyText();
    }

    public void SpendMoney(float amount)
    {
        money -= amount;

        // Update the money UI display
        //UpdateMoneyText();
    }

    public void AddGems(int amount)
    {
        gems += amount;

        // Update the gems UI display
        //UpdateGemsText();
    }

    public void UnlockBean(int beanIndex)
    {
        // Set the corresponding bean index to true in the unlocked beans array
        unlockedBeans[beanIndex] = true;
    }

    public bool IsBeanUnlocked(int beanIndex)
    {
        // Check if the bean at the given index is unlocked
        return unlockedBeans[beanIndex];
    }

    public void Prestige()
    {
        // Reset game progress, increase prestige level, and provide any bonuses
        money = 0;
        gems = 0;

        // Reset unlocked beans array
        for (int i = 0; i < unlockedBeans.Length; i++)
        {
            unlockedBeans[i] = false;
        }

        // Increase prestige level
        prestigeLevel++;

        // Update the money and gems UI display
        //UpdateMoneyText();
        //UpdateGemsText();
    }
}
