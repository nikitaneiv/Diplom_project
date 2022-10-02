using UnityEngine;

namespace ADS
{
    public class ShowAdd : MonoBehaviour
    {
        public void ShowInterstitial()
        {
            FindObjectOfType<AdsInitialize>().ShowAd();
        }
        
        public void ShowReward()
        {
            FindObjectOfType<RewardAds>().ShowRewardAd();
        }
    }
}