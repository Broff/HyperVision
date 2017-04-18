using UnityEngine;
using System.Collections;

public class PlatformsColor : MonoBehaviour {

	public Color endColor;
	public float time;
	Color startColor;
	
	
	bool start = false;
	void Start () {
		startColor = transform.GetComponent<Renderer>().material.GetColor ("_EmissionColor");
	}
	
	float t = 0;
	void Update () {
		if(start == true){
			tweenColor(t,time,startColor, endColor);
			t += Time.deltaTime;
			if(t>=time){
				start = false;
			}
		}
	}
	
	public void lightOff(){
		start = true;
	}
	
	void tweenColor(float t, float time, Color start, Color finish){
		transform.GetComponent<Renderer>().material.SetColor ("_EmissionColor",Color.Lerp(start, finish, t / time));	
		//transform.GetComponent<Renderer>().material.color = Color.Lerp(start, finish, t / time);	
	}
}
