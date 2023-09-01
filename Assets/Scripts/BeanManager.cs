using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeanManager : MonoBehaviour, IDataPersistence
{

    public List<BeanData> beans = new List<BeanData>();
    public List<Bean> beanUIObjs = new List<Bean>();
    public GameManager gameManagerScript;
    public Transform beanUIContainer;
    public GameObject beanUIPrefab;
    public GameObject spacerImagePrefab;
    public GameObject gameManager;
    public Transform scrollContent;

    // Start is called before the first frame update
    void Start()
    {

        gameManagerScript = gameManager.GetComponent<GameManager>();

        //fillBeans();
        createBeansUI();

    }

    public void LoadData(GameData data)
    {
        this.beans = data.beans;
    }

    public void SaveData(ref GameData data)
    {

        List<Bean> b = new List<Bean>();

        foreach (Bean bd in beanUIObjs)
        {
            Debug.Log("Name: " + bd.beanName + " | Unlocked?: " + bd.unlocked);
        }

        for (int i = 0; i < beanUIObjs.Count; i++){

            b.Add(beanUIObjs[i]);
            data.beans[i].unlocked = beanUIObjs[i].unlocked;
            data.beans[i].currentUpgrade = beanUIObjs[i].currentUpgrade;
            data.beans[i].timeToGrow = beanUIObjs[i].timeToGrow;
            data.beans[i].timeReductionInterval = beanUIObjs[i].timeReductionInterval;

        }

    }

    void fillBeans()
    {
        beans.Add(new BeanData("Arabica", 1, 2, 1, 0));
        beans.Add(new BeanData("Espresso", 16, 20, 3, 250));
        beans.Add(new BeanData("Mocha", 32, 40, 5, 500));
        beans.Add(new BeanData("Chocolate", 64, 75, 10, 2000));
        beans.Add(new BeanData("Irish Cream", 128, 200, 25, 5000));
        beans.Add(new BeanData("Cinnamon", 256, 500, 45, 10000));
        beans.Add(new BeanData("Maple", 512, 1000, 90, 25000));
        beans.Add(new BeanData("Pumpkin Spice", 1024, 2500, 120, 50000));
        beans.Add(new BeanData("Lava", 2048, 5000, 180, 150000));
        beans.Add(new BeanData("Radioactive", 4096, 15000, 300, 500000));
        beans.Add(new BeanData("Magical", 8192, 30000, 420, 750000));
        beans.Add(new BeanData("Quantum", 16384, 75000, 600, 1500000));
        beans.Add(new BeanData("Galactic", 32768, 250000, 900, 3750000));
        beans.Add(new BeanData("Cosmic", 65536, 500000, 1500, 10000000));
        beans.Add(new BeanData("Time Warp", 131072, 1000000, 1800, 25000000));
    }

    void createBeansUI()
    {

        float offsetY = 0f; // Offset value to position each UI object vertically
        float spacing = 15f; // Spacing between UI objects

        for (int i = 0; i < beans.Count; i++)
        {
            BeanData beanData = beans[i];
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
                Debug.Log("unlocking the first bean! : " + beanScript.unlocked);
            } else
            {
                beanScript.unlocked = beanData.unlocked;
            }
            beanScript.name = beanData.beanName;
            beanScript.gameManagerObj = this.gameManager;
            beanScript.beanName = beanData.beanName;
            //beanScript.baseSellPrice = beanData.baseSellPrice;
            beanScript.baseGrowCount = beanData.baseGrowCount;
            beanScript.upgradeCost = beanData.upgradeCost;
            beanScript.currentUpgrade = beanData.currentUpgrade;
            beanScript.timeToGrow = beanData.timeToGrow;
            beanScript.unlockPrice = beanData.unlockPrice;
            beanScript.timeReductionInterval = beanData.timeReductionInterval;

            beanUIObjs.Add(newBeanUI.GetComponent<Bean>());
            gameManagerScript.AddToBeanScriptList(beanScript);

            // Increase the offsetY value to position the next UI object with spacing
            //offsetY -= rectTransform.rect.height + spacing;

        }

        Instantiate(spacerImagePrefab, scrollContent);

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
    public int unlockPrice;
    public bool unlocked;

    public BeanData(string bN, int bGC, int upgC, int tTG, int unlP)
    {
        beanName = bN;
        //baseSellPrice = bSP;
        baseGrowCount = bGC;
        upgradeCost = upgC;
        currentUpgrade = 1;
        timeToGrow = tTG;
        unlockPrice = unlP;
        timeReductionInterval = 25;
        unlocked = false;
    }

}