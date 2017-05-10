using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsController : MonoBehaviour {

    private GameObject playerBody;
    public List<Skin> skins = new List<Skin>();

    void Start()
    {
        InitSkinPrefs();
        playerBody = GameObject.Find("newPlayer");
        ActivatePlayerSkin();
    }

    private void ActivatePlayerSkin()
    {
        if (playerBody.transform.childCount > 0)
        {
            Destroy(playerBody.transform.GetChild(0).gameObject);
        }

        Skin activeSkin = GetActiveSkin();

        GameObject skinObj = Instantiate(activeSkin.skin);
        skinObj.transform.parent = playerBody.transform;
        skinObj.transform.localPosition = new Vector3(0,0,0);
        playerBody.GetComponent<PlayerController>().UpdateColor(skinObj);
    }

    private void InitSkinPrefs()
    {
        if (PlayerPrefs.GetString("skins") == "")
        {
            PlayerPrefs.SetString("skins", "0!0");
            Debug.Log("skins initialized");
        }
    }

    public bool CheckPurchased(int num)
    {
        //Debug.Log(PlayerPrefs.GetString("skins"));
        string[] va = (PlayerPrefs.GetString("skins").Split('!'));
        //Debug.Log(va.Length);
        string[] val = va[1].Split(',');
        int[] b = new int[val.Length];
        for (int i = 0; i < b.Length; i++)
        {
            b[i] = Convert.ToInt32(val[i]);
        }

        for (int i = 0; i < b.Length; i++)
        {
            if (b[i] == num)
            {
                return true;
            }
        }
        return false;
    }

    public Skin GetActiveSkin()
    {
        int i = GetActiveIndex();
        if (i == -1)
        {
            //return null;
        }
        return skins[i];
    }

    public void SetActive(int i)
    {
        if (i >= 0 && i < skins.Count)
        {
            string[] val = PlayerPrefs.GetString("skins").Split('!');
            string[] buying = val[1].Split(',');
            bool buy = false;
            for (int j = 0; j < buying.Length; j++)
            {
                if (i == Convert.ToInt32(buying[j]))
                {
                    buy = true;
                    break;
                }
            }

            if (buy == true)
            {
                PlayerPrefs.SetString("skins", i + "!" + val[1]);                
            }
            ActivatePlayerSkin();
        }
    }

    public int[] GetBuySkins()
    {
        string[] val = PlayerPrefs.GetString("skins").Split('!');
        string[] buying = val[1].Split(',');
        int[] b = new int[buying.Length];
        for (int i = 0; i < buying.Length; i++ )
        {
            b[i] = Convert.ToInt32(buying[i]);
        }
        return b;
    }

    public void BuySkin(int i)
    {
        if (i >= 0 && i < skins.Count)
        {
            string[] val = PlayerPrefs.GetString("skins").Split('!');
            string[] buying = val[1].Split(',');
            bool buy = true;
            for (int j = 0; j < buying.Length; j++)
            {
                if (i == Convert.ToInt32(buying[j]))
                {
                    buy = false;
                    break;
                }
            }

            if (buy == true)
            {
                PlayerPrefs.SetString("skins", val[0] + "!" + val[1] + "," + i);
            }
        }
    }

    public int GetActiveIndex()
    {
        string s = PlayerPrefs.GetString("skins");
        int num = 0;
        try
        {
            num = Convert.ToInt32(s.Split('!')[0]);
            if (num < 0 || num >= skins.Count)
            {
                num = -1;
            }
        }
        catch (FormatException e) { }
        return num;
    }
}
