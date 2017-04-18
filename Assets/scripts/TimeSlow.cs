using UnityEngine;
using System.Collections;

public class TimeSlow : MonoBehaviour {

    public float timeScaleMax = 0.5f;
    public float speed = 0.007f;
    public float duration;

    bool workState = false;
    float actualDuration = 0;

    float decelerationTime = 0;

	void Update () {
	    if(workState == true){
            if (Time.timeScale > timeScaleMax && actualDuration >= duration/2)
            {
                Time.timeScale -= speed;
                decelerationTime += Time.deltaTime;
            }
            else
            {
                if(actualDuration <= decelerationTime)
                {
                    if (Time.timeScale < 1)
                    {
                        Time.timeScale += speed;
                    }
                    else if (Time.timeScale > 1)
                    {
                        Time.timeScale = 1;
                    }

                    if (Time.timeScale == 1 && actualDuration == 0)
                    {
                        actualDuration = 0;
                        decelerationTime = 0;
                        workState = false;
                        gameObject.GetComponent<PlayerController>().TimeBonusOff();
                        Debug.Log("TIME OFF");
                    }
                }
            }

            if (actualDuration <= 0)
            {
                actualDuration = 0;
                decelerationTime = 0;
                workState = false;
                gameObject.GetComponent<PlayerController>().TimeBonusOff();
                Debug.Log("TIME OFF");
            }

            actualDuration -= Time.deltaTime;
        }
	}

    public void startBonus()
    {
        workState = true;
        actualDuration = duration;
        decelerationTime = 0;
    }
}
