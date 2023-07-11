using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ADGameScript : MonoBehaviour
{
   // private const float CheckTimer = 150f;
   // private const float TimeOffset = 5f;

    //public string nameScene;
    private string lastIsAdsOpen = null;
    private float timer;
    private Canvas canvasComponent;
    private int i = 0;

    public GameObject panelForReward;
    public GameObject panelDeath;
    public Button restartButton;
    public Button backButton;
    public TextMeshProUGUI adWarningScene;
   // public TextMeshProUGUI adWarningSetting;
   // public TextMeshProUGUI adWarningCategory;

    public void ShareFriend(){
#if UNITY_WEBGL && !UNITY_EDITOR
        WebGLPluginJS.ShareFunction();
#endif
    }

    public void ShowAdInterstitial(){

#if UNITY_WEBGL && !UNITY_EDITOR
    	            WebGLPluginJS.InterstitialFunction();
#endif
    }

    IEnumerator TimerAds()
    {
        i += 1;
        restartButton.interactable = false;
        backButton.interactable = false;
        adWarningScene.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        adWarningScene.text = "�� �������� ������� �������� 3 �������";
        yield return new WaitForSeconds(1f);
        adWarningScene.text = "�� �������� ������� �������� 2 �������";
        yield return new WaitForSeconds(1f);
        adWarningScene.text = "�� �������� ������� �������� 1 �������";
        yield return new WaitForSeconds(1f);
        ShowAdInterstitial();
        adWarningScene.gameObject.SetActive(false);
        restartButton.interactable = true;
        backButton.interactable = true;
    }

    public void ShowAdReward(){
#if UNITY_WEBGL && !UNITY_EDITOR
    	WebGLPluginJS.RewardFunction();
#endif
       // sliderHome.value += rewardBonusSliderHome;
    	//if(sliderFuelCar.value<=lowBalanceFuel) sliderFuelCar.value += rewardBonusSliderFuel;
    }

    private void Start()
    {
        canvasComponent = panelDeath.GetComponent<Canvas>();
    }

    private void Update()
    {
        CheckAds();
        if (canvasComponent.enabled && i==0)
        {
            StartCoroutine(TimerAds());
        }
        
    }
    /* //� ���� ������� �������� � PROSoundManager
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = true;
        }
    }

    public void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = true;
        }
    }
    */               
    public void AdsClosed()
    {
       // timer = 0f;
        Time.timeScale = 1;
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
            //AudioListener.pause = true;
            lastIsAdsOpen = "yes";
           // if(!panelDeath.activeSelf) panelForReward.SetActive(true);
        }
        else
        {
            //��������� �������
            PlayerPrefs.SetInt("AdsOpen", 0);
          // if (PlayerPrefs.GetInt("Sound") == 1) AudioListener.pause = false;
            if (lastIsAdsOpen == "yes") {
                AdsClosed();
                lastIsAdsOpen = "no";
            }
        }
#endif
    }
}
