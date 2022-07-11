using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class GoogleAdmob : MonoBehaviour
{
    static public GoogleAdmob instance;
    public bool isReset;
    bool isStartScean;
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        this.RequestReward();
    }

    public void StartInGameScean()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        this.RequestReward();
        this.RequestBanner();
        isStartScean = true;
    }


    void RequestReward()
    {
        string adUnitId;
        #if UNITY_ANDROID
                adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #elif UNITY_IPHONE
                                    adUnitId = "ca-app-pub-3940256099942544/1712485313";
        #else
                                    adUnitId = "unexpected_platform";
        #endif

        this.rewardedAd = new RewardedAd(adUnitId);
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
 
    }

    private void RequestBanner()
    {
        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        #elif UNITY_IPHONE
                                    string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
                                    string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    void HandleRewardedAdClosed(object sender, System.EventArgs args)
    {
        if(isStartScean)
        {
            isReset = true;
            RequestReward();
        }
        else
        {
            SceneManager.LoadScene("InGame");
        }
    }

    void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        PlayerData.instance.goCheckPoint = true;
    }
    public void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }
}
