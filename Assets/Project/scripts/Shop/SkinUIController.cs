using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinUIController : MonoBehaviour {

    public bool isFree = true;
    public GameObject standController;
    public Text label;
    public Text cost;
    public GameObject playButton;
    public GameObject buyButton;
    public GameObject activateButton;
    private GameObject shop;
    private GameObject skin;
    private Settings sett;

    public void InitSkin(GameObject o, GameObject s)
    {
        shop = transform.parent.gameObject;
        sett = FindObjectOfType<Settings>();
        skin = s;
        standController = o;
        label.text = skin.GetComponent<SkinContainerController>().skin.name;
        cost.text = skin.GetComponent<SkinContainerController>().skin.cost.ToString();
        CheckStates();
    }

    private void CheckStates()
    {
        int activeNumber = FindObjectOfType<SkinsController>().GetActiveIndex();
        if (skin.GetComponent<SkinContainerController>().skin.number == activeNumber)
        {
            playButton.SetActive(true);
            buyButton.SetActive(false);
            activateButton.SetActive(false);
        }
        else if (skin.GetComponent<SkinContainerController>().isBuy == true)
        {
            playButton.SetActive(false);
            buyButton.SetActive(false);
            activateButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
            buyButton.SetActive(true);
            activateButton.SetActive(false);
        }
    }

    public void Buy()
    {
        GameObject sContr = FindObjectOfType<SkinsController>().gameObject;
        if (sContr.GetComponent<SkinsController>().CheckPurchased(skin.GetComponent<SkinContainerController>().skin.number) == false)
        {
            if (isFree)
            {
                sContr.GetComponent<SkinsController>().BuySkin(skin.GetComponent<SkinContainerController>().skin.number);
                buyButton.SetActive(false);
                activateButton.SetActive(false);
                playButton.SetActive(true);
                Activate();
            }
            else
            {
                int cost = skin.GetComponent<SkinContainerController>().skin.cost;
                if (cost < sett.GetComponent<Settings>().Coins)
                {
                    sett.GetComponent<Settings>().Coins -= cost;
                    sContr.GetComponent<SkinsController>().BuySkin(skin.GetComponent<SkinContainerController>().skin.number);
                    buyButton.SetActive(false);
                    activateButton.SetActive(false);
                    playButton.SetActive(true);
                    Activate();
                }
                else
                {
                    shop.GetComponent<ShopScreenController>().OpenCoinShop();
                }
            }            
        }
        else
        {
            buyButton.SetActive(false);
            activateButton.SetActive(true);
        }        
    }

    public void Activate()
    {
        GameObject sContr = FindObjectOfType<SkinsController>().gameObject;
        if (sContr.GetComponent<SkinsController>().CheckPurchased(skin.GetComponent<SkinContainerController>().skin.number) == true)
        {
            standController.GetComponent<StandController>().Unactivate();
            sContr.GetComponent<SkinsController>().SetActive(skin.GetComponent<SkinContainerController>().skin.number);
            buyButton.SetActive(false);
            activateButton.SetActive(false);
            playButton.SetActive(true);
        }
    }

    public void UnActivate()
    {
        GameObject sContr = FindObjectOfType<SkinsController>().gameObject;
        if (sContr.GetComponent<SkinsController>().CheckPurchased(skin.GetComponent<SkinContainerController>().skin.number) == true)
        {
            buyButton.SetActive(false);
            activateButton.SetActive(true);
        }
        else
        {
            buyButton.SetActive(true);
            activateButton.SetActive(false);
        }
    }
}
