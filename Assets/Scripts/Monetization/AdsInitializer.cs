using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] RewardedAdsButton rewardedAdsButton;
    [SerializeField] DoubleIncomeAdScript doubleIncomeAdsButton;
    [SerializeField] DoubleBrewingCapacityAd doubleBrewingCapacityAdsButton;
    [SerializeField] DoubleCustomerCapacityAd doubleCustomerCapacityAdsButton;
    [SerializeField] OfflineAdRewards offlineAdButton;
    [SerializeField] CashRewardAd cashRewardAd;
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        #if UNITY_IOS
                    _gameId = _iOSGameId;
        #elif UNITY_ANDROID
                _gameId = _androidGameId;
        #elif UNITY_EDITOR
                    _gameId = _androidGameId; //Only for testing the functionality in the Editor
        #endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        rewardedAdsButton.LoadAd();
        doubleIncomeAdsButton.LoadAd();
        doubleBrewingCapacityAdsButton.LoadAd();
        doubleCustomerCapacityAdsButton.LoadAd();
        offlineAdButton.LoadAd();
        cashRewardAd.InitialAdLoad();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}