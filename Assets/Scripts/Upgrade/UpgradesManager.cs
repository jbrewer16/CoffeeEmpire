//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class UpgradesManager : MonoBehaviour
//{
//    public List<UpgradeData> upgrades = new List<UpgradeData>();
//    public GameObject gameManager;
//    public GameObject upgradeUIPrefab;
//    public Transform upgradeUIContainer;
//    public GameObject growthMultiplierUpg;
//    private GameManager gameManagerScript;

//    // Start is called before the first frame update
//    void Start()
//    {

//        gameManagerScript = gameManager.GetComponent<GameManager>();

//        initializeUpgradeUI(growthMultiplierUpg);

//        //fillUpgrades();
//        //createUpgradeUI();
//    }

//    void initializeUpgradeUI(GameObject uiObj)
//    {
        
//    }

//    void fillUpgrades()
//    {
//        upgrades.Add(new UpgradeData(10000, "Arabica"));
//        upgrades.Add(new UpgradeData(50000, "Robusta"));
//    }

//    void createUpgradeUI()
//    {

//        float offsetY = 0f; // Offset value to position each UI object vertically
//        float spacing = 30f; // Spacing between UI objects

//        for (int i = 0; i < upgrades.Count; i++)
//        {
//            {
//                UpgradeData upgradeData = upgrades[i];
//                GameObject newUpgradeUI = Instantiate(upgradeUIPrefab, upgradeUIContainer);

//                // Adjust the position of the instantiated UI object
//                RectTransform rectTransform = newUpgradeUI.GetComponent<RectTransform>();
//                rectTransform.anchorMin = new Vector2(0f, 1f); // Anchor to top-left corner
//                rectTransform.anchorMax = new Vector2(0f, 1f);
//                rectTransform.pivot = new Vector2(0f, 1f);
//                rectTransform.anchoredPosition = new Vector2(0f, offsetY);

//                Upgrade upgradeScript = newUpgradeUI.GetComponent<Upgrade>();
//                Debug.Log(i + " - " );
//                upgradeScript.bean = gameManagerScript.beans[i];
//                upgradeScript.cost = upgradeData.cost;
//                upgradeScript.beanName = upgradeData.beanName;
//                upgradeScript.gameManagerObj = this.gameManager;

//                // Increase the offsetY value to position the next UI object with spacing
//                offsetY -= rectTransform.rect.height + spacing;

//            }

//        }
//    }

//    public class UpgradeData
//    {

//        public int cost;
//        public string beanName;

//        public UpgradeData(int c, string b)
//        {
//            cost = c;
//            beanName = b;
//        }

//    }
//}