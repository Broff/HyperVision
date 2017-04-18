using UnityEngine;
using System.Collections;

public class BonusDestroy : MonoBehaviour {


    public BonusType type;

	void Start () {
        //
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public BonusType Type
    {
        get
        {
            return type;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "newPlayer"){
            Destroy(gameObject, 0.1f);
        }
    }

    public enum BonusType
    {
        Time = 1,
        Shield = 2,
        Color = 3
    }
}
