using UnityEngine;
using System.Collections;

public class RotateClockEffect : MonoBehaviour {

    bool toX = false;
	
	void Start () {
        setAngle(toX);
	}
	
	
	void Update () {
	    if(gameObject.transform.parent.gameObject.GetComponent<CameraMove>().getWayToX() != toX){
            toX = !toX;
            setAngle(toX);
        }
	}

    void setAngle(bool b)
    {
        if (b == false)
        {
            transform.localEulerAngles = new Vector3(90,90,0);
            gameObject.GetComponent<ParticleSystem>().startRotation3D = new Vector3(0,90,0);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 90, 0);
            gameObject.GetComponent<ParticleSystem>().startRotation3D = new Vector3(90, 0, 0);
        }
    }
}
