/*
 * AdsManager.cs
 * 
 * Description:
 * - This script is responsible for all the ad functionality for a game.
 * 
 * Author(s): 
 * - Kody Wood
*/

using UnityEngine;
using UnityEngine.Events;

using System;

#if RUBBER_DUCKS_ADS
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
#endif

using RubberDucks.Utilities;

namespace RubberDucks.Utilities.Ads
{
#if RUBBER_DUCKS_ADS
    public class AdsManager : PersistentSingleton<AdsManager>
    {
		//--- Events ---//
		[System.Serializable]
		public class EventList
		{
            // Reward specific
            public UnityEvent OnRewardAdCompleted;
            public UnityEvent OnRewardAdClosed;

            // Interstital specific
            public UnityEvent OnInterstitalAdClosed;

            // Banner specific
            public UnityEvent OnBannerAdClosed;

            // Generic ad lifecycle
            public UnityEvent OnAdFailedToLoad;
            public UnityEvent OnAdFailedToShow;
            public UnityEvent OnAdLeftApplication;
            public UnityEvent OnAdLoaded;
            public UnityEvent OnAdOpened;
		}
		[Header("Events")]
		public EventList Events = default;

        //--- Properties ---//

        //--- Public Variables ---//

        //--- Protected Variables ---//

        //--- Private Variables ---//
        private BannerView m_BannerView;
        private InterstitialAd m_Interstitial;
        private RewardedAd m_RewardedAd;

        //--- Unity Methods ---//
        private void Start()
        {
            MobileAds.Initialize(initStatus => { });

            this.RequestBanner();
            this.RequestInterstitialAd();
            this.RequestRewardAd();
        }

        //--- Public Methods ---//
        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("Ad Loaded");

            Events.OnAdLoaded.Invoke();
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            //MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);

            Events.OnAdFailedToLoad.Invoke();
        }

        public void HandleOnAdOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("Ad Opened");

            //--- CODE HERE---//
            // Pause Game and Audio

            Events.OnAdOpened.Invoke();
        }

        public void HandleOnAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            //MonoBehaviour.print("HandleRewardedAdFailedToShow event received with message: "+ args.Message);

            Events.OnAdFailedToShow.Invoke();
        }

        public void HandleOnBannerAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("Ad Closed");

            Events.OnBannerAdClosed.Invoke();

            //--- CODE HERE---//
            // Resume Paused Game and Audio
            RequestBanner();
        }

        public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("Ad Closed");

            Events.OnInterstitalAdClosed.Invoke();

            //--- CODE HERE---//
            // Resume Paused Game and Audio
            RequestInterstitialAd();
        }

        public void HandleOnRewardAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("Ad Closed");

            Events.OnRewardAdClosed.Invoke();

            //--- CODE HERE---//
            // Resume Paused Game and Audio
            RequestRewardAd();
        }

        public void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("Leaving Application due to Ad");

            Events.OnAdLeftApplication.Invoke();
        }

        public void HandleOnUserEarnedReward(object sender, Reward args)
        {
            string type = args.Type;
            double amount = args.Amount;
            MonoBehaviour.print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);

            Events.OnRewardAdCompleted.Invoke();
        }

        public void ShowInterstitialAd()
        {
            if (m_Interstitial != null && this.m_Interstitial.IsLoaded())
            {
                this.m_Interstitial.Show();
            }
            else
            {
                MonoBehaviour.print("Interstitial Ad is not ready.");
                RequestInterstitialAd();
            }
        }

        public void ShowRewardAd()
        {
            if (m_RewardedAd != null && this.m_RewardedAd.IsLoaded())
            {
                this.m_RewardedAd.Show();
            }
            else
            {
                MonoBehaviour.print("Rewarded Ad is not ready.");
                RequestRewardAd();
            }
        }

        public void DestroyBannerAd()
        {
            if (m_BannerView != null)
            {
                m_BannerView.Destroy();
            }
        }

        public void DestroyInterstitialAd()
        {
            if (m_Interstitial != null)
            {
                m_Interstitial.Destroy();
            }
        }

        public void DestroyRewardAd()
        {
            if (m_RewardedAd != null)
            {
                m_RewardedAd.Destroy();
            }
        }

        //--- Protected Methods ---//

        //--- Private Methods ---//
        private void RequestBanner()
        {
#if UNITY_ANDROID
            //Real Unit ID (Use when Live)
            //string adUnitId = "ca-app-pub-0000000000000000/0000000000";


            //Fake Unit ID (Use when testing)
            //string adUnitId = "ca-app-pub-0000000000000000/0000000000";
#elif UNITY_IPHONE
            //Real Unit ID (Use when Live)
            //string adUnitId = "ca-app-pub-0000000000000000/0000000000";

            //Fake Unit ID (Use when testing)
            //string adUnitId = "ca-app-pub-0000000000000000/0000000000";
#else
            string adUnitId = "unexpected_platform";
#endif
            // Clean up banner before reusing
            if (m_BannerView != null)
            {
                m_BannerView.Destroy();
            }

            AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
            this.m_BannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

            // Called when an ad request has successfully loaded.
            this.m_BannerView.OnAdLoaded += this.HandleOnAdLoaded;

            // Called when an ad request failed to load.
            this.m_BannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;

            // Called when an ad is clicked.
            this.m_BannerView.OnAdOpening += this.HandleOnAdOpened;

            // Called when the user returned from the app after an ad click.
            this.m_BannerView.OnAdClosed += this.HandleOnBannerAdClosed;

            // Called when the ad click caused the user to leave the application.
            //this.m_BannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();

            // Load the banner with the request.
            this.m_BannerView.LoadAd(request);    
        }

        private void RequestInterstitialAd()
        {
#if UNITY_ANDROID
            //Real Unit ID (Use when Live)
            //string adUnitId = "ca-app-pub-0000000000000000/0000000000";

            //Fake Unit ID (Use when testing)
            //string adUnitId = "ca-app-pub-0000000000000000/0000000000";
#elif UNITY_IPHONE
            //Real Unit ID (Use when Live)
            //string adUnitId = "ca-app-pub-0000000000000000/0000000000";

            //Fake Unit ID (Use when testing)
            //string adUnitId = "ca-app-pub-0000000000000000/0000000000";
#else
            string adUnitId = "unexpected_platform";
#endif
            // Clean up banner before reusing
            if (m_Interstitial != null)
            {
                m_Interstitial.Destroy();
            }

            // Initialize an InterstitialAd.
            this.m_Interstitial = new InterstitialAd(adUnitId);

            // Called when an ad request has successfully loaded.
            this.m_Interstitial.OnAdLoaded += HandleOnAdLoaded;

            // Called when an ad request failed to load.
            this.m_Interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;

            // Called when an ad is shown.
            this.m_Interstitial.OnAdOpening += HandleOnAdOpened;

            // Called when the ad is closed.
            this.m_Interstitial.OnAdClosed += HandleOnInterstitialAdClosed;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();

            // Load the interstitial with the request.
            this.m_Interstitial.LoadAd(request);
        }


        private void RequestRewardAd()
        {
#if UNITY_ANDROID
            // Real Unit ID(Use when Live)
            //string adUnitId = "ca-app-pub-0000000000000000/0000000000";

            //Fake Unit ID (Use when testing)
            //string adUnitId = "ca-app-pub-0000000000000000/0000000000";
#elif UNITY_IPHONE
            // Real Unit ID(Use when Live)
            //string adUnitId = "ca-app-pub-0000000000000000/0000000000";

            //Fake Unit ID (Use when testing)
            //string adUnitId = "ca-app-pub-0000000000000000/0000000000";
#else
            string adUnitId = "unexpected_platform";
#endif
            // Clean up banner before reusing
            if (m_RewardedAd != null)
            {
                m_RewardedAd.Destroy();
            }

            this.m_RewardedAd = new RewardedAd(adUnitId);

            // Called when an ad request has successfully loaded.
            this.m_RewardedAd.OnAdLoaded += HandleOnAdLoaded;

            // Called when an ad request failed to load.
            this.m_RewardedAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;

            // Called when an ad is shown.
            this.m_RewardedAd.OnAdOpening += HandleOnAdOpened;

            // Called when an ad request failed to show.
            this.m_RewardedAd.OnAdFailedToShow += HandleOnAdFailedToShow;

            // Called when the user should be rewarded for interacting with the ad.
            this.m_RewardedAd.OnUserEarnedReward += HandleOnUserEarnedReward;

            // Called when the ad is closed.
            this.m_RewardedAd.OnAdClosed += HandleOnRewardAdClosed;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();

            // Load the rewarded ad with the request.
            this.m_RewardedAd.LoadAd(request);
        }
    }
#endif
}
