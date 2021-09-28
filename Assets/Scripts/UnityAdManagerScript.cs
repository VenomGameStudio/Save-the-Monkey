using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class UnityAdManagerScript : MonoBehaviour, IUnityAdsListener
{
    public static UnityAdManagerScript instance;

    private static readonly string storeId = "3936591";

    private static readonly string videoID = "video";
    private static readonly string rewardedID = "rewardedVideo";
    private static readonly string bannerID = "banner";

    private Action adSuccess;
    private Action adSkipped;
    private Action adFailed;

#if UNITY_ANDROID
    private static bool testMode = false;
#else
    private static bool testMode = true;
#endif

    public bool isRunning;

    private void Awake()
    {
        /*if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Advertisement.AddListener(this);
            Advertisement.Initialize(storeId, testMode);
        }
        else
            Destroy(gameObject);*/

        instance = this;
        Advertisement.AddListener(this);
        Advertisement.Initialize(storeId, testMode);
    }

    public static void ShowStandardAd()
    {
        if (Advertisement.IsReady(videoID))
            Advertisement.Show(videoID);
    }

    public static void ShowBanner()
    {
        instance.StartCoroutine(ShowBannerWhenRead());
    }

    public static void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public static void ShowRewardedAd(Action success, Action skipped, Action failed)
    {
        instance.adSuccess = success;
        instance.adSkipped = skipped;
        instance.adFailed = failed;
        if (Advertisement.IsReady(rewardedID))
            Advertisement.Show(rewardedID);
    }

    private static IEnumerator ShowBannerWhenRead()
    {
        while (!Advertisement.IsReady(bannerID))
            yield return new WaitForSeconds(0.5f);

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(bannerID);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        isRunning = true;
        Time.timeScale = 0f;
        FindObjectOfType<AudioManagerScript>().SoundControl(isRunning);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == rewardedID)
        {
            switch (showResult)
            {
                case ShowResult.Failed:
                    adFailed();
                    break;
                case ShowResult.Skipped:
                    adSkipped();
                    break;
                case ShowResult.Finished:
                    adSuccess();
                    break;
            }
        }
        isRunning = false;
        Time.timeScale = 1f;
        FindObjectOfType<AudioManagerScript>().SoundControl(isRunning);
    }

    public void OnUnityAdsReady(string placementId) { }
    public void OnUnityAdsDidError(string message) { }
}
