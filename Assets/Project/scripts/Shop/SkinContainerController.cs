using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinContainerController : MonoBehaviour {

    public Skin skin;
    public bool isBuy = false;
    public bool isActive = false;

    public void InitSkin(Skin s)
    {
        skin = s;
        GameObject o = Instantiate(s.skin);
        o.GetComponent<Animator>().enabled = true;
        o.transform.parent = this.transform;
        o.transform.localPosition = new Vector3(0,0,0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "skin")
        {
            GetComponent<PulseObject>().StartIncrease();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "skin")
        {
            GetComponent<PulseObject>().StartDecrease();
        }
    }
}
