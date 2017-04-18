using UnityEngine;
using System.Collections;

public class DeadPlayerEffect : MonoBehaviour {

    public int min = 10;
    public int max = 100;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void dead(Vector3 pos, Vector3 direction, Color col)
    {
        gameObject.transform.position = pos;
        gameObject.SetActive(true);
        System.Random rnd = new System.Random();
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().material.color= col;
            if (direction.x == 1)
            {
                child.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, rnd.Next(-5, 5) * 0.1f) * rnd.Next(min, max), ForceMode.Acceleration);
            }
            else
            {
                child.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(rnd.Next(-5, 5) * 0.1f, 0, 1) * rnd.Next(min, max), ForceMode.Acceleration);
            }
            //Debug.Log(child.gameObject.name);
        }
    }
}
