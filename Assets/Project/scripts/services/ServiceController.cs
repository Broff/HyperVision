using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ServiceController : MonoBehaviour, IService {

    public GameObject ios;
    public GameObject android;

    private static ServiceController instance = null;
    private static GameObject service = null;

    void Start()
    {        
        #if UNITY_ANDROID && !UNITY_EDITOR       
             service = android;
        #elif UNITY_IOS && !UNITY_EDITOR     
            service = ios;    
        #endif
        //Singletone();
        
        Login();
    }

    public void Login()
    {
        if (service != null)
        {
            Debug.Log("Login");
            service.GetComponent<IService>().Login();
        }
    }

    private void Singletone(){
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowTop()
    {
        if (service != null)
        {
            Debug.Log("ShowTop");
            service.GetComponent<IService>().ShowTop();
        }
    }

    public void SetTop(int i)
    {
        if (service != null)
        {
            Debug.Log("SetTop");
            service.GetComponent<IService>().SetTop(i);
        }
    }

    public void ShowAchivs()
    {
        if (service != null)
        {
            Debug.Log("ShowAchivs");
            service.GetComponent<IService>().ShowAchivs();
        }
    }

    public void Rate()
    {
        if (service != null)
        {
            Debug.Log("Rate");
            service.GetComponent<IService>().Rate();
        }
    }

    public void Share()
    {
        if (service != null)
        {
            Debug.Log("Rate");
            service.GetComponent<IService>().Share();
        }
    }
}
