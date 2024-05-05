using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaristaManager : MonoBehaviour
{
    public List<BaristaData> baristas = new List<BaristaData>();
    public GameObject gameManager;
    public GameObject baristaUIPrefab;
    public Transform baristaUIContainer;
    public GameObject spacerImagePrefab;
    public Transform scrollContent;

    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {

        gameManagerScript = gameManager.GetComponent<GameManager>();

        fillBaristas();
        createBaristaUI();
    }

    void fillBaristas()
    {
        baristas.Add(new BaristaData(1, 1000, "Brad Pittstop", "Arabica"));
        baristas.Add(new BaristaData(2, 5000, "Robusta Downey Jr", "Espresso"));
        baristas.Add(new BaristaData(3, 15000, "Elvis Parsley", "Mocha"));
        baristas.Add(new BaristaData(4, 30000, "Al Cappuccino", "Chocolate"));
        baristas.Add(new BaristaData(5, 75000, "Americano Armstrong", "Irish Cream"));
        baristas.Add(new BaristaData(6, 125000, "George Clooney-Tunes", "Cinnamon"));
        baristas.Add(new BaristaData(7, 200000, "Oprah Wind-free", "Maple"));
        baristas.Add(new BaristaData(8, 500000, "Jim Carrey-Oke", "Pumpkin Spice"));
        baristas.Add(new BaristaData(9, 750000, "Hom Tanks", "Lava"));
        baristas.Add(new BaristaData(10, 1000000, "Chris Steamsworth", "Radioactive"));
        baristas.Add(new BaristaData(11, 2500000, "Java Depp", "Magical"));
        baristas.Add(new BaristaData(12, 12000000, "Will Sip", "Quantum"));
        baristas.Add(new BaristaData(13, 50000000, "Sandra Mocha", "Galactic"));
        baristas.Add(new BaristaData(14, 250000000, "Marilyn Monbroe", "Cosmic"));
        baristas.Add(new BaristaData(15, 1000000000, "Frothy Reynolds", "Time Warp"));
    }

    void createBaristaUI()
    {

        float offsetY = 0f; // Offset value to position each UI object vertically
        float spacing = 30f; // Spacing between UI objects

        for (int i = 0; i < baristas.Count; i++)
        {
            BaristaData baristaData = baristas[i];
            GameObject newBaristaUI = Instantiate(baristaUIPrefab, scrollContent);

            // Adjust the position of the instantiated UI object
            //RectTransform rectTransform = newBaristaUI.GetComponent<RectTransform>();
            //rectTransform.anchorMin = new Vector2(0f, 1f); // Anchor to top-left corner
            //rectTransform.anchorMax = new Vector2(0f, 1f);
            //rectTransform.pivot = new Vector2(0f, 1f);
            //rectTransform.anchoredPosition = new Vector2(0f, offsetY);

            Barista baristaScript = newBaristaUI.GetComponent<Barista>();
            baristaScript.bean = gameManagerScript.beans[i];
            baristaScript.id = baristaData.id;
            baristaScript.name = baristaData.baristaName;
            baristaScript.cost = baristaData.cost;
            baristaScript.baristaName = baristaData.baristaName;
            baristaScript.beanName = baristaData.beanName;
            baristaScript.gameManagerObj = this.gameManager;
            if (gameManagerScript.baristas.Contains(baristaScript.id))
            {
                baristaScript.bought = true;
            }

            // Increase the offsetY value to position the next UI object with spacing
            //offsetY -= rectTransform.rect.height + spacing;

        }

    }

}

public class BaristaData
{

    public int id;
    public int cost;
    public string baristaName;
    public string beanName;

    public BaristaData(int i, int c, string barN, string beanN)
    {
        id = i;
        cost = c;
        baristaName = barN;
        beanName = beanN;
    }

}
