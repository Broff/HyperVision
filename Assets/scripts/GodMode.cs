using UnityEngine;
using System.Collections;

public class GodMode : MonoBehaviour
{

    public int angleCount = 5;
    int actualAngleCount = 0;

    public void AddAngle()
    {
        actualAngleCount++;
        if(actualAngleCount >= angleCount){
            gameObject.GetComponent<PlayerController>().GodModeOff();
            actualAngleCount = 0;
        }
    }

}
