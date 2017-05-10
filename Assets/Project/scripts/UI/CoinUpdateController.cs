using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUpdateController : MonoBehaviour {

    private Text val;
    private Settings s;

	void Start () {
        val = GetComponent<Text>();
        s = FindObjectOfType<Settings>();
        UpdateCoinsValue();
        s.GetComponent<Settings>().AddSetCoinEvent(UpdateCoinsValue);
	}

    private void UpdateCoinsValue()
    {
        val.text = s.GetComponent<Settings>().Coins.ToString();
    }
}
