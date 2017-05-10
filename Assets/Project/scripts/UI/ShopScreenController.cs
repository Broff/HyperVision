using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreenController : MonoBehaviour {

    public Text score;
    public Camera cam;
    public GameObject environment;
    public GameObject shopEnvironment;
    public GameObject endScreen;
    public GameObject coinShopScreen;

	void Start () {
        cam.transform.position = new Vector3(-6.54f, 12.14f, -6.54f);	
	}

    public void Open()
    {
        View();
    }

    public void View()
    {
        environment.SetActive(false);
        shopEnvironment.SetActive(true);
        gameObject.SetActive(true);
    }

    public void Skip()
    {
        environment.SetActive(true);
        shopEnvironment.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Back()
    {
        Skip();
        endScreen.GetComponent<DeadScreenController>().View();
    }

    public void OpenCoinShop()
    {
        coinShopScreen.GetComponent<CoinShopController>().View();
        gameObject.SetActive(false);
    }

    public void updateScore(int i)
    {
        score.GetComponent<Text>().text = "" + i;
    }
}
