using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsTesterScript : MonoBehaviour
{
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
