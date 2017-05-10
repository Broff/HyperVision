using UnityEngine;
using System.Collections;

public class PlayerColorController : MonoBehaviour {

	public Material m1, m2, m3;
		
	public int setColor(GameObject o, int colorNum){		
		if(colorNum > 3){
			colorNum = 1;
		}
        getColor(o, colorNum);
		colorNum++;		
		return colorNum;
	}
		
	public void getColor(GameObject o, int colorNum){
		switch(colorNum){
			case 1:
			o.tag = "c1";
			o.transform.GetComponent<Renderer>().material = m1;
			//o.transform.GetComponent<Renderer>().material.color = color1;
			break;
			case 2:
			o.tag = "c2";
			o.transform.GetComponent<Renderer>().material = m2;
			//o.transform.GetComponent<Renderer>().material.color = color2;
			break;
			case 3:
			o.tag = "c3";
			o.transform.GetComponent<Renderer>().material = m3;
			//o.transform.GetComponent<Renderer>().material.color = color3;
			break;
			default :
			o.tag = "c3";
			o.transform.GetComponent<Renderer>().material = m3;
			//o.transform.GetComponent<Renderer>().material.color = color3;
			break;
		}
	}
}