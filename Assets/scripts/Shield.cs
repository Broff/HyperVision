using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    public float timeActive = 1f;
    bool shieldState = false;

    public void ActivateShield()
    {
        State = true;
        hit = false;
        time = 0;
    }

    public bool State
    {
        get 
        {
            return shieldState;
        }
        set
        {
            shieldState = value;
        }
    }

    public void Hit()
    {
        hit = true;
        gameObject.GetComponent<PlayerController>().ShieldOff();
    }

    bool hit = false;
    float time = 0;
	void Update () {
	    if(hit == true){
            time += Time.deltaTime;
            if(time >= timeActive){
                State = false;
                hit = false;
                time = 0;                
            }
        }
	}
}
