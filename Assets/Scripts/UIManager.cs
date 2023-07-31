using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject header;
    public GameObject footer;
    public GameObject beansPage;
    public GameObject coffeePage;
    public GameObject customerPage;
    public GameObject hamburgerMenu;
    public GameObject baristasPanel;
    public GameObject upgradesPanel;
    public GameObject investorsPanel;

    private CanvasGroup beansPageCG;
    private CanvasGroup coffeePageCG;
    private CanvasGroup customerPageCG;

    // Start is called before the first frame update
    void Start()
    {
        header.SetActive(true);
        footer.SetActive(true);
        beansPage.SetActive(true);
        coffeePage.SetActive(true);
        customerPage.SetActive(true);
        hamburgerMenu.SetActive(false);
        baristasPanel.SetActive(false);
        upgradesPanel.SetActive(false);
        investorsPanel.SetActive(false);

        beansPageCG = beansPage.GetComponent<CanvasGroup>();
        coffeePageCG = coffeePage.GetComponent<CanvasGroup>();
        customerPageCG = customerPage.GetComponent<CanvasGroup>();

        ToggleCanvasGroup(coffeePageCG, false);
        ToggleCanvasGroup(beansPageCG, true);
        ToggleCanvasGroup(customerPageCG, false);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void OpenBaristasPanel()
    {
        hamburgerMenu.SetActive(false);
        baristasPanel.SetActive(true);
        upgradesPanel.SetActive(false);
        investorsPanel.SetActive(false);
    }

    public void CloseBaristasPanel()
    {
        baristasPanel.SetActive(false);
        footer.SetActive(true);
    }

    public void OpenUpgradesPanel()
    {
        hamburgerMenu.SetActive(false);
        baristasPanel.SetActive(false);
        upgradesPanel.SetActive(true);
        investorsPanel.SetActive(false);
    }

    public void CloseUpgradesPanel()
    {
        upgradesPanel.SetActive(false);
        footer.SetActive(true);
    }

    public void OpenInvestorsPanel()
    {
        hamburgerMenu.SetActive(false);
        baristasPanel.SetActive(false);
        upgradesPanel.SetActive(false);
        investorsPanel.SetActive(true);
    }

    public void CloseInvestorsPanel()
    {
        investorsPanel.SetActive(false);
        footer.SetActive(true);
    }

    public void OpenBeansPage()
    {
        ToggleCanvasGroup(beansPageCG, true);
        ToggleCanvasGroup(coffeePageCG, false);
        ToggleCanvasGroup(customerPageCG, false);
    }

    public void OpenCoffeePage()
    {
        ToggleCanvasGroup(beansPageCG, false);
        ToggleCanvasGroup(coffeePageCG, true);
        ToggleCanvasGroup(customerPageCG, false);
    }

    public void OpenCustomerPage()
    {
        ToggleCanvasGroup(beansPageCG, false);
        ToggleCanvasGroup(coffeePageCG, false);
        ToggleCanvasGroup(customerPageCG, true);
    }

    private void ToggleCanvasGroup(CanvasGroup canvasGroup, bool isVisible)
    {
        canvasGroup.alpha = isVisible ? 1 : 0;
        canvasGroup.interactable = isVisible;
        canvasGroup.blocksRaycasts = isVisible;
    }

}
