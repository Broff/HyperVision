using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseObject : MonoBehaviour {
    
    public Transform defaultScale;
    public Vector3 newScale;
    public float scaleSpeed;
    [Range(0,1)]
    public float mistake;
    private Vector3 startScale;
    private Vector3 scaleVectSpeed;
    private ScaleState state = ScaleState.Off;

	void Start () {
        startScale = defaultScale.localScale;
        scaleVectSpeed = new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
	}

    public void StartIncrease()
    {
        state = ScaleState.Increase;        
    }

    public void StartDecrease()
    {
        state = ScaleState.Decrease;
    }

	void Update () {
        if (state == ScaleState.Off)
        {
            return;
        }

        if (state == ScaleState.Decrease)
        {
            if (CheckPoint(transform.localScale.x, startScale.x)
                && CheckPoint(transform.localScale.z, startScale.z))
            {
                transform.localScale = startScale;
                state = ScaleState.Off;
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, startScale, Time.deltaTime * scaleSpeed);
                //SetScale(-scaleVectSpeed);
            }
        }
        else
        {
            if (transform.localScale.x >= newScale.x
                && transform.localScale.z >= newScale.z)
            {
                transform.localScale = newScale;
                state = ScaleState.Off;
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, newScale, Time.deltaTime * scaleSpeed);
                //SetScale(scaleVectSpeed);
            }
        }
	}

    private void SetScale(Vector3 s)
    {
        transform.localScale += s;
    }

    private bool CheckPoint(float x1, float x2)
    {
        if ((x1 >= x2 + mistake && x1 <= x2 - mistake)
            || (x1 >= x2 - mistake && x1 <= x2 + mistake))
        {
            return true;
        }
        return false;
    }
}
public enum ScaleState
{
    Decrease,
    Increase,
    Off
}