using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinShopController : MonoBehaviour {

    public GameObject shopScreen;
    public GameObject skins;

    public void View()
    {
        skins.SetActive(false);
        gameObject.SetActive(true);
    }

    public void Skip()
    {
        skins.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Back(){
        shopScreen.GetComponent<ShopScreenController>().View();
        Skip();
    }
}
