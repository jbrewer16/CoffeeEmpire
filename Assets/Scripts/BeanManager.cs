using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeanManager : MonoBehaviour
{

    public List<BeanData> beans = new List<BeanData>();
    public GameManager gameManagerScript;
    public Transform beanUIContainer;
    public GameObject beanUIPrefab;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {

        gameManagerScript = gameManager.GetComponent<GameManager>();

        fillBeans();
        createBeansUI();

    }

    void fillBeans()
    {
        beans.Add(new BeanData("Arabica", 1, 2, 1, 0));
        beans.Add(new BeanData("Robusta", 30, 34, 5, 500));
        beans.Add(new BeanData("Balls", 100, 105, 5, 3000));
    }

    void createBeansUI()
    {

        float offsetY = 0f; // Offset value to position each UI object vertically
        float spacing = 10f; // Spacing between UI objects

        for (int i = 0; i < beans.Count; i++)
        {
            BeanData beanData = beans[i];
            GameObject newBeanUI = Instantiate(beanUIPrefab, beanUIContainer);

            // Adjust the position of the instantiated UI object
            RectTransform rectTransform = newBeanUI.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0f, 1f); // Anchor to top-left corner
            rectTransform.anchorMax = new Vector2(0f, 1f);
            rectTransform.pivot = new Vector2(0f, 1f);
            rectTransform.anchoredPosition = new Vector2(0f, offsetY);

            Bean beanScript = newBeanUI.GetComponent<Bean>();
            if (i == 0) beanScript.unlockPanel.SetActive(false);
            beanScript.name = beanData.beanName;
            beanScript.gameManagerObj = this.gameManager;
            beanScript.beanName = beanData.beanName;
            //beanScript.baseSellPrice = beanData.baseSellPrice;
            beanScript.baseGrowCount = beanData.baseGrowCount;
            beanScript.upgradeCost = beanData.upgradeCost;
            beanScript.currentUpgrade = beanData.currentUpgrade;
            beanScript.timeToGrow = beanData.timeToGrow;
            beanScript.unlockPrice = beanData.unlockPrice;

            gameManagerScript.AddToBeanScriptList(beanScript);

            // Increase the offsetY value to position the next UI object with spacing
            offsetY -= rectTransform.rect.height + spacing;

        }

    }

}

public class BeanData
{

    public string beanName;
    //public int baseSellPrice;
    public int baseGrowCount;
    public int upgradeCost;
    public int currentUpgrade;
    public int timeToGrow;
    public int unlockPrice;

    public BeanData(string bN, int bGC, int upgC, int tTG, int unlP)
    {
        beanName = bN;
        //baseSellPrice = bSP;
        baseGrowCount = bGC;
        upgradeCost = upgC;
        currentUpgrade = 1;
        timeToGrow = tTG;
        unlockPrice = unlP;
    }

}