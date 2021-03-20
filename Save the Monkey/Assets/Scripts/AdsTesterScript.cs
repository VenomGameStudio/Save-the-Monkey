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
        UnityAdManagerScript.ShowBanner();
    }

    public void HideBanner() 
    {
        UnityAdManagerScript.HideBanner();
    }
}
