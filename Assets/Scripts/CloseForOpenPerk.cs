using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseForOpenPerk : MonoBehaviour
{
    public string checkOpenPerk;
    void Start()
    {
        CloseIfOpenPerk(checkOpenPerk);
    }

    public void CloseIfOpenPerk(string perk)
    {
        if(PlayerPrefs.GetInt(perk) == 1) gameObject.SetActive(false);
    }

}
