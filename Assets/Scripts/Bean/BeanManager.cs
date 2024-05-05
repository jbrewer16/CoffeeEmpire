using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanManager : MonoBehaviour, IDataPersistence
{
    [Header("Bean Data")]
    public List<BeanData> beanDataList = new List<BeanData>();
    public List<Bean> beans = new List<Bean>();

    [Header("UI Elements")]
    public List<GameObject> beanUIObjects = new List<GameObject>();
    public Transform scrollContent;
    public GameObject beanUIPrefab;
    public GameObject spacerImagePrefab;

    [Header("Game Management")]
    public GameObject gameManager;
    private GameManager gameManagerScript;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        CreateBeansUI();
    }

    public void LoadData(GameData data)
    {
        beanDataList = data.beans;
    }

    public void SaveData(ref GameData data)
    {
        for (int i = 0; i < beans.Count; i++)
        {
            Bean bean = beans[i];
            data.beans[i].unlocked = bean.unlocked;
            data.beans[i].currentUpgrade = bean.upgradeCount;
            data.beans[i].timeToGrow = bean.timeToGrow;
            data.beans[i].timeReductionInterval = bean.timeReductionInterval;
        }
    }

    private void CreateBeansUI()
    {
        foreach (BeanData beanData in beanDataList)
        {
            GameObject newBeanUI = Instantiate(beanUIPrefab, scrollContent);
            Bean beanScript = newBeanUI.GetComponent<Bean>();
            InitializeBeanUI(beanData, beanScript);

            beanUIObjects.Add(newBeanUI);
            beans.Add(beanScript);
            gameManagerScript.AddToBeanScriptList(beanScript);
        }

        GameObject spacer = Instantiate(spacerImagePrefab, scrollContent);
        beanUIObjects.Add(spacer);
    }

    private void InitializeBeanUI(BeanData beanData, Bean beanScript)
    {
        beanScript.unlocked = beanData.unlocked;
        beanScript.name = beanData.beanName;
        beanScript.gameManagerObj = gameManager;
        beanScript.beanName = beanData.beanName;
        beanScript.baseGrowCount = beanData.baseGrowCount;
        beanScript.initialUpgradeCost = beanData.upgradeCost;
        beanScript.upgradeCount = beanData.currentUpgrade;
        beanScript.timeToGrow = beanData.timeToGrow;
        beanScript.unlockPrice = beanData.unlockPrice;
        beanScript.upgradeCoefficient = beanData.upgradeCoefficient;
        beanScript.timeReductionInterval = beanData.timeReductionInterval;
        if (gameManagerScript.baristas.Contains(beanData.id))
        {
            beanScript.SetManager(true);
        }

        if (beanDataList.IndexOf(beanData) == 0)
        {
            beanScript.setUnlock(true);
            beanScript.unlocked = true;
        }
    }

    public void ResetBeansUI()
    {
        foreach (GameObject beanObj in beanUIObjects)
        {
            Destroy(beanObj);
        }
        beanUIObjects.Clear();
        beans.Clear();
        gameManagerScript.ClearBeans();
        CreateBeansUI();
    }
}

[System.Serializable]
public class BeanData
{
    public int id;
    public string beanName;
    public int baseGrowCount;
    public int upgradeCost;
    public int currentUpgrade;
    public int timeReductionInterval;
    public float timeToGrow;
    public float upgradeCoefficient;
    public double unlockPrice;
    public bool unlocked;

    public BeanData(int i, string bName, int bGrowCount, int upgC, float tToGrow, double unlPrice, float upgCoefficient)
    {
        id = i;
        beanName = bName;
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