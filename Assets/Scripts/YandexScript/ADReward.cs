using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADReward : MonoBehaviour
{
    private string lastIsAdsOpen = null;


    void Start()
    {
        CheckPerk("Perk_1");
        CheckPerk("Perk_2");
        CheckPerk("Perk_3");
        CheckPerk("Perk_4");
        CheckPerk("Perk_5");
        CheckPerk("Perk_6");

    }

    void Update()
    {

    }

    private void CheckPerk(string perk)
    {
        if (!PlayerPrefs.HasKey(perk)) PlayerPrefs.SetInt(perk, 0);
    }

    public void OpenPerk(string perk)
    {
        PlayerPrefs.SetInt(perk, 1);
    }

    public void ShowAdRewardForPerk()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    	WebGLPluginJS.RewardFunction();
#endif  
    }
    public void ForReward()
    {
        Time.timeScale = 0;
       // rewardPanelText.SetActive(false);
        //rewardPanelText.SetActive(true);
    }
    public void AdsClosed()
    {
        // timer = 0f;
        Time.timeScale = 1;
       // rewardPanelText.SetActive(true);
    }

    public void CheckAds()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        var adsOpen = WebGLPluginJS.GetAdsOpen();
        if (lastIsAdsOpen == null) {
            lastIsAdsOpen = adsOpen;
        }

        if (adsOpen == "yes")
        {
            PlayerPrefs.SetInt("AdsOpen", 1);
            AudioListener.pause = true;
            lastIsAdsOpen = "yes";
        }
        else
        {
            //Коничлась реклама
            PlayerPrefs.SetInt("AdsOpen", 0);
           // if (PlayerPrefs.GetInt("Sound") == 1) AudioListener.pause = false;
            if (lastIsAdsOpen == "yes") {
               // AdsClosed();
                lastIsAdsOpen = "no";
            }
        }
#endif
    }


}
