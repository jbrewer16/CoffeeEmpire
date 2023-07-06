using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaristaManager : MonoBehaviour
{
    public List<BaristaData> baristas = new List<BaristaData>();
    public GameObject gameManager;
    public GameObject baristaUIPrefab;
    public Transform baristaUIContainer;

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
        baristas.Add(new BaristaData(1000, "Forest Trump", "Arabica"));
        baristas.Add(new BaristaData(5000, "Dawn Cheri", "Robusta"));
    }

    void createBaristaUI()
    {

        float offsetY = 0f; // Offset value to position each UI object vertically
        float spacing = 30f; // Spacing between UI objects

        for (int i = 0; i < baristas.Count; i++)
        {
            BaristaData baristaData = baristas[i];
            GameObject newBaristaUI = Instantiate(baristaUIPrefab, baristaUIContainer);

            // Adjust the position of the instantiated UI object
            RectTransform rectTransform = newBaristaUI.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0f, 1f); // Anchor to top-left corner
            rectTransform.anchorMax = new Vector2(0f, 1f);
            rectTransform.pivot = new Vector2(0f, 1f);
            rectTransform.anchoredPosition = new Vector2(0f, offsetY);

            Barista baristaScript = newBaristaUI.GetComponent<Barista>();
            baristaScript.bean = gameManagerScript.beans[i];
            baristaScript.name = baristaData.baristaName;
            baristaScript.cost = baristaData.cost;
            baristaScript.baristaName = baristaData.baristaName;
            baristaScript.beanName = baristaData.beanName;
            baristaScript.gameManagerObj = this.gameManager;

            // Increase the offsetY value to position the next UI object with spacing
            offsetY -= rectTransform.rect.height + spacing;

        }

    }

}

public class BaristaData
{

    public int cost;
    public string baristaName;
    public string beanName;

    public BaristaData(int c, string barN, string beanN)
    {
        cost = c;
        baristaName = barN;
        beanName = beanN;
    }

}
