using UnityEngine;
using System.Collections;

public class ColorController : MonoBehaviour {

	public Color color1,color2,color3;
	public Color color1P,color2P,color3P;
	public Color color1Circle,color2Circle,color3Circle;
		
	public int getPlayerColor(GameObject o, int colorNum){		
		if(colorNum > 3){
			colorNum = 1;
		}
		getColorPlayer(o, colorNum);
		colorNum++;		
		return colorNum;
	}
	
	public void getColor(GameObject o, int colorNum){
		switch(colorNum){
			case 1:
			o.tag = "c1";
			//o.transform.GetComponent<Renderer>().material = m1;
			o.transform.GetComponent<Renderer>().material.SetColor ("_EmissionColor", color1);
			break;
			case 2:
			o.tag = "c2";
			//o.transform.GetComponent<Renderer>().material = m2;
			o.transform.GetComponent<Renderer>().material.SetColor ("_EmissionColor", color2);
			break;
			case 3:
			o.tag = "c3";
			//o.transform.GetComponent<Renderer>().material = m3;
			o.transform.GetComponent<Renderer>().material.SetColor ("_EmissionColor", color3);
			break;
			default :
			o.tag = "block";
			//o.transform.GetComponent<Renderer>().material = m3;
			//o.transform.GetComponent<Renderer>().material.color = color3;
			break;
		}
	}
	
	public void getPointColor(GameObject o, int colorNum){
		switch(colorNum){
			case 1:
			o.tag = "c1";
			//o.transform.GetComponent<Renderer>().material = m1;
			o.transform.FindChild("box").GetComponent<Renderer>().material.SetColor ("_EmissionColor", color1);
			o.transform.FindChild("point_eff").GetComponent<ParticleSystem>().startColor = Color.white;
			o.GetComponent<PointPickUp>().setEffColor(color1);
			break;
			case 2:
			o.tag = "c2";
			//o.transform.GetComponent<Renderer>().material = m2;
			o.transform.FindChild("box").GetComponent<Renderer>().material.SetColor ("_EmissionColor", color2);
			o.transform.FindChild("point_eff").GetComponent<ParticleSystem>().startColor =  Color.white;
			o.GetComponent<PointPickUp>().setEffColor(color2);
			break;
			case 3:
			o.tag = "c3";
			//o.transform.GetComponent<Renderer>().material = m3;
			o.transform.FindChild("box").GetComponent<Renderer>().material.SetColor ("_EmissionColor", color3);
			o.transform.FindChild("point_eff").GetComponent<ParticleSystem>().startColor =  Color.white;
			o.GetComponent<PointPickUp>().setEffColor(color3);
			break;
			default :
			o.tag = "block";
			//o.transform.GetComponent<Renderer>().material = m3;
			//o.transform.GetComponent<Renderer>().material.color = color3;
			break;
		}
	}
	
	public Color getColorValue(int colorNum){
		switch(colorNum){
			case 1:
			return color1P;
			case 2:
			return color2P;
			case 3:
			return color3P;	
			default:
			return color1P;			
		}
	}
	
	public Color getColorCircleValue(int colorNum){
		if(colorNum > 3){
			colorNum -= 3;
		}
		switch(colorNum){
			case 1:
			return color1Circle;
			case 2:
			return color2Circle;
			case 3:
			return color3Circle;	
		}
		return color1Circle;
	}
	
	public void getColorPlayer(GameObject o, int colorNum){
		switch(colorNum){
			case 1:
			o.tag = "c1";
			//o.transform.GetComponent<Renderer>().material = m1;
			o.transform.GetComponent<Renderer>().material.SetColor ("_EmissionColor", color1P);
			break;
			case 2:
			o.tag = "c2";
			//o.transform.GetComponent<Renderer>().material = m2;
			o.transform.GetComponent<Renderer>().material.SetColor ("_EmissionColor", color2P);
			break;
			case 3:
			o.tag = "c3";
			//o.transform.GetComponent<Renderer>().material = m3;
			o.transform.GetComponent<Renderer>().material.SetColor ("_EmissionColor", color3P);
			break;
			default :
			o.tag = "block";
			//o.transform.GetComponent<Renderer>().material = m3;
			//o.transform.GetComponent<Renderer>().material.color = color3;
			break;
		}
	}
}
