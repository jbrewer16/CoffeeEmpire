using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{

    public CashRewardAd cashRewardAd;
    public GameObject cashRewardAdBtn;

    public DoubleIncomeAdScript doubleIncomeAdScript;
    public GameObject doubleIncomeAdBtn;

    public DoubleBrewingCapacityAd doubleBrewingCapacityAd;
    public GameObject doubleBrewingCapacityAdBtn;

    public DoubleCustomerCapacityAd doubleCustomerCapacityAd;
    public GameObject doubleCustomerCapacityAdBtn;

    // Start is called before the first frame update
    void Start()
    {
        cashRewardAd = cashRewardAdBtn.GetComponent<CashRewardAd>();
        doubleIncomeAdScript = doubleIncomeAdBtn.GetComponent<DoubleIncomeAdScript>();
        doubleBrewingCapacityAd = doubleBrewingCapacityAdBtn.GetComponent<DoubleBrewingCapacityAd>();
        doubleCustomerCapacityAd = doubleCustomerCapacityAdBtn.GetComponent<DoubleCustomerCapacityAd>();
        //StartCoroutine(StartAdTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartAdTimer()
    {

        int adMinutes = Random.Range(1, 10);
        Debug.Log("Showing another ad in " + adMinutes + " minutes.");

        int timeInSeconds = adMinutes; // * 60;
        yield return new WaitForSeconds(timeInSeconds);

        cashRewardAd.ShowAdBtn();
        cashRewardAd.LoadAd();

    }
}
