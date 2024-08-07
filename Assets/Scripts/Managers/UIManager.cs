using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject adConfirmationPanel;
    public GameObject bugReportPanel;
    public GameObject header;
    public GameObject footer;
    public GameObject beansPage;
    public GameObject coffeePage;
    public GameObject customerPage;
    public GameObject hamburgerMenu;
    public GameObject harvestersPanel;
    public GameObject upgradesPanel;
    public GameObject investorsPanel;
    public GameObject shopPanel;
    public GameObject debugPanel;
    public GameObject doubleIncomeBtn;
    public GameObject doubleBrewCapBtn;
    public GameObject doubleCustCapBtn;
    public GameObject statisticsPage;
    //public GameObject confirmationPanel;

    public ShopScript shopScript;

    private CanvasGroup beansPageCG;
    private CanvasGroup coffeePageCG;
    private CanvasGroup customerPageCG;
    public int currPage;

    public GameObject gameManagerObj;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameManagerObj.GetComponent<GameManager>();

        adConfirmationPanel.SetActive(false);
        //bugReportPanel.SetActive(false);
        header.SetActive(true);
        footer.SetActive(true);
        beansPage.SetActive(true);
        coffeePage.SetActive(true);
        customerPage.SetActive(true);
        hamburgerMenu.SetActive(false);
        harvestersPanel.SetActive(false);
        upgradesPanel.SetActive(false);
        investorsPanel.SetActive(false);
        shopPanel.SetActive(false);
        debugPanel.SetActive(false);
        statisticsPage.SetActive(false);
        //confirmationPanel.SetActive(false);

        beansPageCG = beansPage.GetComponent<CanvasGroup>();
        coffeePageCG = coffeePage.GetComponent<CanvasGroup>();
        customerPageCG = customerPage.GetComponent<CanvasGroup>();

        currPage = 0;

        SetActivePage();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.doubleIncomeAdsWatched < 4)
        {
            doubleIncomeBtn.SetActive(true);
        } else
        {
            doubleIncomeBtn.SetActive(false);
        }

        if(gameManager.doubleBrewCapacityActive)
        {
            doubleBrewCapBtn.SetActive(false);
        } else
        {
            doubleBrewCapBtn.SetActive(true);
        }

        if (gameManager.doubleCustCapacityActive)
        {
            doubleCustCapBtn.SetActive(false);
        }
        else
        {
            doubleCustCapBtn.SetActive(true);
        }

    }

    public void openStatisticsPage()
    {
        hamburgerMenu.SetActive(false);
        statisticsPage.SetActive(true);
    }

    public void closeStatisticsPage()
    {
        hamburgerMenu.SetActive(true);
        statisticsPage.SetActive(false);
    }

    public void openBugReportPanel()
    {
        hamburgerMenu.SetActive(false);
        bugReportPanel.SetActive(true);
    }

    public void closeBugReportPanel()
    {
        hamburgerMenu.SetActive(true);
        bugReportPanel.SetActive(false);
    }

    public void openMenu()
    {
        hamburgerMenu.SetActive(true);
        footer.SetActive(false);
    }

    public void closeMenu()
    {
        hamburgerMenu.SetActive(false);
        footer.SetActive(true);
    }

    public void OpenHarvestersPanel()
    {
        hamburgerMenu.SetActive(false);
        harvestersPanel.SetActive(true);
        upgradesPanel.SetActive(false);
        investorsPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void CloseHarvestersPanel()
    {
        harvestersPanel.SetActive(false);
        footer.SetActive(true);
    }

    public void OpenUpgradesPanel()
    {
        hamburgerMenu.SetActive(false);
        harvestersPanel.SetActive(false);
        upgradesPanel.SetActive(true);
        investorsPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void CloseUpgradesPanel()
    {
        upgradesPanel.SetActive(false);
        footer.SetActive(true);
    }

    public void OpenInvestorsPanel()
    {
        hamburgerMenu.SetActive(false);
        harvestersPanel.SetActive(false);
        upgradesPanel.SetActive(false);
        investorsPanel.SetActive(true);
        shopPanel.SetActive(false);
    }

    public void CloseInvestorsPanel()
    {
        investorsPanel.SetActive(false);
        footer.SetActive(true);
    }

    public void OpenShopPanel()
    {
        shopScript.CalculateWarpRewards();
        hamburgerMenu.SetActive(false);
        harvestersPanel.SetActive(false);
        upgradesPanel.SetActive(false);
        investorsPanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    public void CloseShopPanel()
    {
        shopPanel.SetActive(false);
        footer.SetActive(true);
    }

    public void OpenDebugPanel()
    {
        debugPanel.SetActive(true);
    }

    public void CloseDebugPanel()
    {
        debugPanel.SetActive(false);
    }

    //public void OpenBeansPage()
    //{
    //    ToggleCanvasGroup(beansPageCG, true);
    //    ToggleCanvasGroup(coffeePageCG, false);
    //    ToggleCanvasGroup(customerPageCG, false);
    //}

    //public void OpenCoffeePage()
    //{
    //    ToggleCanvasGroup(beansPageCG, false);
    //    ToggleCanvasGroup(coffeePageCG, true);
    //    ToggleCanvasGroup(customerPageCG, false);
    //}

    //public void OpenCustomerPage()
    //{
    //    ToggleCanvasGroup(beansPageCG, false);
    //    ToggleCanvasGroup(coffeePageCG, false);
    //    ToggleCanvasGroup(customerPageCG, true);
    //}

    public void NavLeftPage()
    {
        if(currPage > 0)
        {
            currPage--;
        }
        SetActivePage();
    }

    public void NavRightPage()
    {
        if (currPage < 2)
        {
            currPage++;
        }
        SetActivePage();
    }

    public void SetActivePage()
    {
        ToggleCanvasGroup(beansPageCG, (currPage == 0));
        ToggleCanvasGroup(coffeePageCG, (currPage == 1));
        ToggleCanvasGroup(customerPageCG, (currPage == 2));
    }

    public void OpenConfirmationPanel()
    {
        //confirmationPanel.SetActive(true);
    }

    public void RestartGame()
    {
        // TODO - Confirmation panel

    }

    public void Cancel()
    {

    }

    private void ToggleCanvasGroup(CanvasGroup canvasGroup, bool isVisible)
    {
        canvasGroup.alpha = isVisible ? 1 : 0;
        canvasGroup.interactable = isVisible;
        canvasGroup.blocksRaycasts = isVisible;
    }

}
