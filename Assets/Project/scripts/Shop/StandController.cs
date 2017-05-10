using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandController : MonoBehaviour {

    public GameObject swController;
    public float speed;
    public GameObject focus;
    public GameObject ui;
    public float moveCoff = 2;
    public float forceCoff = 100;
    private Vector3 pos = new Vector3(0,0,0);
    bool touch = false;
    float force = 0;

    void Start()
    {
        swController.GetComponent<SwipeManager>().AddStartTouch(() => { touch = true; force = 0; });
        swController.GetComponent<SwipeManager>().AddFinishTouch(() => { touch = false; });
        swController.GetComponent<SwipeManager>().AddMoveTouch(MoveMenu);
        swController.GetComponent<SwipeManager>().AddEndMoveTouch(ForceMenu);
    }

    public void Unactivate()
    {
        foreach (SkinContainerController s in FindObjectsOfType<SkinContainerController>())
        {
            s.isActive = false;
        }
    }

    public void SetFocus(GameObject f)
    {
        ui.GetComponent<SkinUIController>().InitSkin(gameObject, f);
        Vector3 p = transform.position;
        Debug.Log(FindObjectOfType<ShowSkinsController>().widthDelta);
        Debug.Log(f.GetComponent<SkinContainerController>().skin.number);
        float fv = (f.GetComponent<SkinContainerController>().skin.number * FindObjectOfType<ShowSkinsController>().widthDelta )/ Mathf.Pow(2,0.5f);
        p.x -= fv;
        p.z += fv;
        pos = p;
    }

    public void moveToFocused()
    {
        focus.transform.position = pos;
    }

    void Update()
    {
        if (force != 0)
        {
            force = Vector2.MoveTowards(new Vector2(force, force), new Vector2(0, 0), Time.deltaTime).x;
            MoveMenu(force);
            if (force == 0)
            {
                FindClosest();
            }
        }
        else if (touch == false && force == 0)
        {
            focus.transform.position = Vector3.Lerp(focus.transform.position, pos, Time.deltaTime * speed);
        }
    }

    private void FindClosest()
    {
        float length = float.MaxValue;
        GameObject o = null;
        foreach(SkinContainerController sc in FindObjectsOfType<SkinContainerController>()){
            float dlt = (transform.position - sc.gameObject.transform.position).magnitude;
            if (dlt <= length)
            {
                length = dlt;
                o = sc.gameObject;
            }
        }

        SetFocus(o);
    }

    public void MoveMenu(float f)
    {
        f *= moveCoff;
        float d = f / Mathf.Pow(2, 0.5f);
        focus.transform.position += new Vector3(-d,0,d);
    }

    public void ForceMenu(float f)
    {
        f *= forceCoff;
        force = f;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<SkinContainerController>())
        {
            SetFocus(other.gameObject);
        }
    }
}
