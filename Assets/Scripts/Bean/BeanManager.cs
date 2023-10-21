using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeanManager : MonoBehaviour, IDataPersistence
{

    public List<BeanData> beanDataList = new List<BeanData>();
    public List<Bean> beans = new List<Bean>();
    public List<GameObject> beanUIObjects = new List<GameObject>();
    public GameManager gameManagerScript;
    public Transform beanUIContainer;
    public GameObject beanUIPrefab;
    public GameObject spacerImagePrefab;
    public GameObject gameManager;
    public Transform scrollContent;

    private bool dataLoadedAndGainsNotCalc = false;

    // Start is called before the first frame update
    void Start()
    {

        gameManagerScript = gameManager.GetComponent<GameManager>();

        //fillBeans();
        createBeansUI();

    }

    void Update()
    {
        if (dataLoadedAndGainsNotCalc)
        {
            gameManagerScript.CalculateOfflineGains();
            dataLoadedAndGainsNotCalc = false;
        }
    }

    public void LoadData(GameData data)
    {
        this.beanDataList = data.beans;
        dataLoadedAndGainsNotCalc = true;
    }

    public void SaveData(ref GameData data)
    {

        List<Bean> b = new List<Bean>();

        for (int i = 0; i < beans.Count; i++){

            b.Add(beans[i]);
            data.beans[i].unlocked = beans[i].unlocked;
            data.beans[i].currentUpgrade = beans[i].upgradeCount;
            data.beans[i].timeToGrow = beans[i].timeToGrow;
            data.beans[i].timeReductionInterval = beans[i].timeReductionInterval;

        }

    }

    void createBeansUI()
    {

        float offsetY = 0f; // Offset value to position each UI object vertically
        float spacing = 15f; // Spacing between UI objects

        for (int i = 0; i < beanDataList.Count; i++)
        {
            BeanData beanData = beanDataList[i];
            GameObject newBeanUI = Instantiate(beanUIPrefab, scrollContent);

            // Adjust the position of the instantiated UI object
            //RectTransform rectTransform = newBeanUI.GetComponent<RectTransform>();
            //rectTransform.anchorMin = new Vector2(0f, 1f); // Anchor to top-left corner
            //rectTransform.anchorMax = new Vector2(0f, 1f);
            //rectTransform.pivot = new Vector2(0f, 1f);
            //rectTransform.anchoredPosition = new Vector2(0f, offsetY);

            Bean beanScript = newBeanUI.GetComponent<Bean>();
            if (i == 0)
            {
                beanScript.unlockPanel.SetActive(false);
                beanScript.setUnlock(true);
                beanScript.unlocked = true;
                //beanScript.unlockBean();
                //Debug.Log("unlocking the first bean! : " + beanScript.unlocked);
            } else
            {
                beanScript.unlocked = beanData.unlocked;
            }
            beanScript.name = beanData.beanName;
            beanScript.gameManagerObj = this.gameManager;
            beanScript.beanName = beanData.beanName;
            //beanScript.baseSellPrice = beanData.baseSellPrice;
            beanScript.baseGrowCount = beanData.baseGrowCount;
            beanScript.initialUpgradeCost = beanData.upgradeCost;
            beanScript.upgradeCount = beanData.currentUpgrade;
            beanScript.timeToGrow = beanData.timeToGrow;
            beanScript.unlockPrice = beanData.unlockPrice;
            beanScript.upgradeCoefficient = beanData.upgradeCoefficient;
            beanScript.timeReductionInterval = beanData.timeReductionInterval;

            beanUIObjects.Add(newBeanUI);
            beans.Add(newBeanUI.GetComponent<Bean>());
            gameManagerScript.AddToBeanScriptList(beanScript);

            // Increase the offsetY value to position the next UI object with spacing
            //offsetY -= rectTransform.rect.height + spacing;

        }

        GameObject spacer = Instantiate(spacerImagePrefab, scrollContent);
        beanUIObjects.Add(spacer);

    }

    public void ResetBeansUI()
    {
        foreach(GameObject beanObj in beanUIObjects)
        {
            Destroy(beanObj);
        }
        beanUIObjects.Clear();
        beans.Clear();
        gameManagerScript.ClearBeans();
        createBeansUI();
    }

}

[System.Serializable]
public class BeanData
{

    public string beanName;
    //public int baseSellPrice;
    public int baseGrowCount;
    public int upgradeCost;
    public int currentUpgrade;
    public int timeReductionInterval;
    public float timeToGrow;
    public float upgradeCoefficient;
    public int unlockPrice;
    public bool unlocked;
    
    /// <summary>
    /// Constructor for BeanData Object
    /// </summary>
    /// <param name="bName">Bean Name</param>
    /// <param name="bGrowCount">Bean Grow Count</param>
    /// <param name="upgC">Upgrade Count</param>
    /// <param name="tToGrow">Time to Grow</param>
    /// <param name="unlPrice">Unlock Price</param>
    /// <param name="upgCoefficient">Upgrade Coefficient</param>
    public BeanData(string bName, int bGrowCount, int upgC, int tToGrow, int unlPrice, float upgCoefficient)
    {
        beanName = bName;
        //baseSellPrice = bSP;
        baseGrowCount = bGrowCount;
        upgradeCost = upgC;
        currentUpgrade = 1;
        timeToGrow = tToGrow;
        unlockPrice = unlPrice;
        upgradeCoefficient = upgCoefficient;
        timeReductionInterval = 25;
        unlocked = false;
    }

}