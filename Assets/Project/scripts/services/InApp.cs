using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InApp {

    public static void NoAds(){
        PlayerPrefs.SetInt("NoAds",1);
    }

    public static void BuyCoins(int count)
    {
        GameObject.FindObjectOfType<Settings>().Coins += count;
    }
}
