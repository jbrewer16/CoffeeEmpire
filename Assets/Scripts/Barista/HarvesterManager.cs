using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterManager : MonoBehaviour
{
    public List<HarvesterData> harvesters = new List<HarvesterData>();
    public GameObject gameManager;
    public GameObject harvesterUIPrefab;
    public Transform harvesterUIContainer;
    public GameObject spacerImagePrefab;
    public Transform scrollContent;

    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {

        gameManagerScript = gameManager.GetComponent<GameManager>();

        fillHarvesters();
        createHarvesterUI();
    }

    void fillHarvesters()
    {
        harvesters.Add(new HarvesterData(1,     1000, "Brad Pittstop", "Arabica"));
        harvesters.Add(new HarvesterData(2,     5000, "Robusta Downey Jr", "Espresso"));
        harvesters.Add(new HarvesterData(3,     15000, "Elvis Parsley", "Mocha"));
        harvesters.Add(new HarvesterData(4,     75000, "Al Cappuccino", "Chocolate"));
        harvesters.Add(new HarvesterData(5,     350000, "Americano Armstrong", "Irish Cream"));
        harvesters.Add(new HarvesterData(6,     5e5, "George Clooney-Tunes", "Cinnamon"));
        harvesters.Add(new HarvesterData(7,     2.5e6, "Oprah Wind-free", "Maple"));
        harvesters.Add(new HarvesterData(8,     5e8, "Jim Carrey-Oke", "Pumpkin Spice"));
        harvesters.Add(new HarvesterData(9,     1.5e10, "Hom Tanks", "Lava"));
        harvesters.Add(new HarvesterData(10,    5e13, "Chris Steamsworth", "Radioactive"));
        harvesters.Add(new HarvesterData(11,    7e15, "Java Depp", "Magical"));
        harvesters.Add(new HarvesterData(12,    1.5e18,	"Will Sip", "Quantum"));
        harvesters.Add(new HarvesterData(13,    3e24, "Sandra Mocha", "Galactic"));
        harvesters.Add(new HarvesterData(14,    1e30, "Marilyn Monbroe", "Cosmic"));
        harvesters.Add(new HarvesterData(15,    2e36, "Frothy Reynolds", "Time Warp"));
    }

    void createHarvesterUI()
    {

        float offsetY = 0f; // Offset value to position each UI object vertically
        float spacing = 30f; // Spacing between UI objects

        for (int i = 0; i < harvesters.Count; i++)
        {
            HarvesterData harvesterData = harvesters[i];
            GameObject newHarvesterUI = Instantiate(harvesterUIPrefab, scrollContent);

            // Adjust the position of the instantiated UI object
            //RectTransform rectTransform = newHarvesterUI.GetComponent<RectTransform>();
            //rectTransform.anchorMin = new Vector2(0f, 1f); // Anchor to top-left corner
            //rectTransform.anchorMax = new Vector2(0f, 1f);
            //rectTransform.pivot = new Vector2(0f, 1f);
            //rectTransform.anchoredPosition = new Vector2(0f, offsetY);

            Harvester harvesterScript = newHarvesterUI.GetComponent<Harvester>();
            harvesterScript.bean = gameManagerScript.beans[i];
            harvesterScript.id = harvesterData.id;
            harvesterScript.name = harvesterData.harvesterName;
            harvesterScript.cost = harvesterData.cost;
            harvesterScript.harvesterName = harvesterData.harvesterName;
            harvesterScript.beanName = harvesterData.beanName;
            harvesterScript.gameManagerObj = this.gameManager;
            if (gameManagerScript.harvesters.Contains(harvesterScript.id))
            {
                harvesterScript.bought = true;
            }

            // Increase the offsetY value to position the next UI object with spacing
            //offsetY -= rectTransform.rect.height + spacing;

        }

    }

}

public class HarvesterData
{

    public int id;
    public double cost;
    public string harvesterName;
    public string beanName;

    public HarvesterData(int i, double c, string harN, string beanN)
    {
        id = i;
        cost = c;
        harvesterName = harN;
        beanName = beanN;
    }

}
