using UnityEngine;

public class AdsTesterScript : MonoBehaviour
{
    public static AdsTesterScript instace;
    
    public void StanderedAd()
    {
        UnityAdManagerScript.ShowStandardAd();
    }

    public void ShowBanner()
    {
        GameManager.instance.adManager.ShowBanner();
    }

    public void HideBanner() 
    {
        GameManager.instance.adManager.HideBanner();
    }
}
