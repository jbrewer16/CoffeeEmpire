using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using TMPro;

public class DoubleCustomerCapacityAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    public GameManager gameManager;
    public GameObject adConfirmationPanel;
    public TMP_Text titleTxt;
    public Image iconImg;
    public Texture2D srcImage;
    public Button okBtn;
    public Button cancelBtn;
    string _adUnitId = null; // This will remain null for unsupported platforms

    private CanvasGroup showAdBtnCG;

    void Start()
    {
        showAdBtnCG = _showAdButton.GetComponent<CanvasGroup>();
    }

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        #if UNITY_IOS
                                _adUnitId = _iOSAdUnitId;
        #elif UNITY_ANDROID
                _adUnitId = _androidAdUnitId;
        #endif

        // Disable the button until the ad is ready to show:
        _showAdButton.interactable = false;
    }

    // Call this public method when you want to get an ad ready to show.
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            _showAdButton.onClick.AddListener(showConfirmationPanel);
            // Enable the button for users to click:
            _showAdButton.interactable = true;
        }
    }

    public void showConfirmationPanel()
    {
        double adReward = gameManager.offlineMoneyGains * 0.25;
        titleTxt.text = "Watch an AD for 5 minutes of double Customer Capacity!";
        Sprite srcSprite = Sprite.Create(srcImage, new Rect(0, 0, srcImage.width, srcImage.height), new Vector2(0.5f, 0.5f));
        iconImg.sprite = srcSprite;
        okBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.RemoveAllListeners();
        okBtn.onClick.AddListener(ShowAd);
        cancelBtn.onClick.AddListener(Cancel);
        adConfirmationPanel.SetActive(true);
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Disable the button:
        _showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
        okBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.RemoveAllListeners();
        adConfirmationPanel.SetActive(false);
    }

    public void Cancel()
    {
        okBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.RemoveAllListeners();
        adConfirmationPanel.SetActive(false);
        HideAdBtn();
        StartCoroutine(StartAdTimer());
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            Debug.Log("You have gained 5 minutes of double Customer Capacity!");
            gameManager.StartDoubleCustomerCapacity();
            _showAdButton.onClick.RemoveAllListeners();
            HideAdBtn();

            LoadAd();
        }
    }
    
    public void HideAdBtn()
    {
        showAdBtnCG.alpha = 0;
        showAdBtnCG.interactable = false;
        showAdBtnCG.blocksRaycasts = false;
    }

    public void ShowAdBtn()
    {
        showAdBtnCG.alpha = 1;
        showAdBtnCG.interactable = true;
        showAdBtnCG.blocksRaycasts = true;
    }

    public void InitialAdLoad()
    {
        HideAdBtn();
        StartCoroutine(StartAdTimer());
    }

    public IEnumerator StartAdTimer()
    {

        int adMinutes = Random.Range(1, 10);
        Debug.Log("Showing another ad in " + adMinutes + " minutes.");

        int timeInSeconds = adMinutes * 60;
        yield return new WaitForSeconds(timeInSeconds);

        ShowAdBtn();
        LoadAd();

    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }
}
