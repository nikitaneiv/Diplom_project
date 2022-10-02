using UnityEngine;
using UnityEngine.Advertisements;

namespace ADS
{
    public class RewardAds : MonoBehaviour,IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
    {
        [SerializeField] string _androidRewardAdUnitId = "Rewarded_Android";
        [SerializeField] string _iOsRewardAdUnitId = "Rewarded_iOS";
        
        string _adRewardUnitId; // This will remain null for unsupported platforms
    
 
    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adRewardUnitId = _iOsRewardAdUnitId;
#elif UNITY_ANDROID
            _adRewardUnitId = _androidRewardAdUnitId;
#endif
        InitializeReward();
    }

    private void InitializeReward()
    {
        _adRewardUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsRewardAdUnitId
            : _androidRewardAdUnitId;
    }

    #region Init

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadRewardAd();
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    #endregion   
    

    // Load content to the Ad Unit:
    public void LoadRewardAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adRewardUnitId);
        Advertisement.Load(_adRewardUnitId, this);
    }
 
    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adRewardUnitId)
    {
        Debug.Log("Ad Loaded: " + adRewardUnitId);
        
    }
 
    // Implement a method to execute when the user clicks the button:
    public void ShowRewardAd()
    {
        // Then show the ad:
        Advertisement.Show(_adRewardUnitId, this);
    }
 
    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adRewardUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adRewardUnitId.Equals(_adRewardUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            FindObjectOfType<GameManager>().AfterReward();

            // Load another ad:
            Advertisement.Load(_adRewardUnitId, this);
        }
    }
 
    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adRewardUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adRewardUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowFailure(string adRewardUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adRewardUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowStart(string adRewardUnitId) { }
    public void OnUnityAdsShowClick(string adRewardUnitId) { }
    }
}