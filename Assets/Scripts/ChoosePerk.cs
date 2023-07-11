using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePerk : MonoBehaviour
{
    public GameObject[] perks; 
    private void Start()
    {
        if (!PlayerPrefs.HasKey("SelectedPerk")) PlayerPrefs.SetInt("SelectedPerk", 0);
    }
    public void ChoosePerkForGame(int numberPerk)
    {
        PlayerPrefs.SetInt("SelectedPerk", numberPerk);
        foreach (GameObject perk in perks)
        {
            perk.SetActive(false);
        }
        perks[numberPerk].SetActive(true);
    }

}
