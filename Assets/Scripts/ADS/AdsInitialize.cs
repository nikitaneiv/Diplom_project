using UnityEngine;
using UnityEngine.Advertisements;

namespace ADS
{
    public class AdsInitialize : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] string _androidGameId;
        [SerializeField] string _iOSGameId;
        [SerializeField] bool _testMode = true;
        [SerializeField] string _androidAdUnitId = "Interstitial_Android";
        [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
        [SerializeField] string _androidBannerAdUnitId = "Banner_Android";
        [SerializeField] string _iOsBannerAdUnitId = "Banner_iOS";
        // [SerializeField] string _androidRewardAdUnitId = "Rewarded_Android";
        // [SerializeField] string _iOsRewardAdUnitId = "Rewarded_iOS";
        
        [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

        //string _adRewardUnitId; // This will remain null for unsupported platforms
        
        string _adBannerUnitId; // This will remain null for unsupported platforms.
        
        private string _gameId;
        string _adUnitId;
        
        void Awake()
        {
            InitializeAds();
        }
 
        public void InitializeAds()
        {
            _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOSGameId
                : _androidGameId;
            // Get the Ad Unit ID for the current platform:
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOsAdUnitId
                : _androidAdUnitId;
            _adBannerUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOsBannerAdUnitId
                : _androidBannerAdUnitId;
            // _adRewardUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            //     ? _iOsRewardAdUnitId
            //     : _androidRewardAdUnitId;
            Advertisement.Initialize(_gameId, _testMode, this);
            // Set the banner position:
            Advertisement.Banner.SetPosition(_bannerPosition);
        }

        #region Init

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
            LoadAd();
            LoadBanner();
            //LoadRewardAd();
        }
 
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }

        #endregion

        #region Interstitial

        // Load content to the Ad Unit:
        public void LoadAd()
        {
            // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }
 
        // Show the loaded content in the Ad Unit:
        public void ShowAd()
        {
            // Note that if the ad content wasn't previously loaded, this method will fail
            Debug.Log("Showing Ad: " + _adUnitId);
            Advertisement.Show(_adUnitId, this);
        }
 
        // Implement Load Listener and Show Listener interface methods: 
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            // Optionally execute code if the Ad Unit successfully loads content.
        }
 
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        }
 
        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        }
 
        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            LoadAd();
            FindObjectOfType<GameManager>().ResetGame();
        }

        #endregion

        #region Banner

        // Implement a method to call when the Load Banner button is clicked:
    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
 
        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(_adBannerUnitId, options);
    }
 
    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBannerAd();
    }
 
    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }
 
    // Implement a method to call when the Show Banner button is clicked:
    void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };
 
        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(_adUnitId, options);
    }
 
    // Implement a method to call when the Hide Banner button is clicked:
    void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }
 
    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }
 
    

        #endregion

    //     #region Rewarded
    //     // Load content to the Ad Unit:
    // public void LoadRewardAd()
    // {
    //     // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
    //     Debug.Log("Loading Ad: " + _adRewardUnitId);
    //     Advertisement.Load(_adRewardUnitId, this);
    // }
    //
    // // If the ad successfully loads, add a listener to the button and enable it:
    // public void OnUnityAdsAdRewardLoaded(string adRewardUnitId)
    // {
    //     Debug.Log("Ad Loaded: " + adRewardUnitId);
    //
    //     if (adRewardUnitId.Equals(_adRewardUnitId))
    //     {
    //         
    //     }
    // }
    //
    // // Implement a method to execute when the user clicks the button:
    // public void ShowRewardAd()
    // {
    //     // Then show the ad:
    //     Advertisement.Show(_adRewardUnitId, this);
    // }
    //
    // // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    // public void OnUnityAdsShowRewardComplete(string adRewardUnitId, UnityAdsShowCompletionState showCompletionState)
    // {
    //     if (adRewardUnitId.Equals(_adRewardUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
    //     {
    //         Debug.Log("Unity Ads Rewarded Ad Completed");
    //         // Grant a reward.
    //         _gameManager.AfterReward();
    //         
    //
    //         // Load another ad:
    //         Advertisement.Load(_adRewardUnitId, this);
    //     }
    // }
    //
    // // Implement Load and Show Listener error callbacks:
    // public void OnUnityAdsRewardFailedToLoad(string adRewardUnitId, UnityAdsLoadError error, string message)
    // {
    //     Debug.Log($"Error loading Ad Unit {adRewardUnitId}: {error.ToString()} - {message}");
    //     // Use the error details to determine whether to try to load another ad.
    // }
    //
    // public void OnUnityAdsShowRewardFailure(string adRewardUnitId, UnityAdsShowError error, string message)
    // {
    //     Debug.Log($"Error showing Ad Unit {adRewardUnitId}: {error.ToString()} - {message}");
    //     // Use the error details to determine whether to try to load another ad.
    // }
    //
    // public void OnUnityAdsRewardShowStart(string adRewardUnitId) { }
    // public void OnUnityAdsRewardShowClick(string adRewardUnitId) { }
    //
    //     #endregion     
    }
}