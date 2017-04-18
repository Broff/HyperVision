using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RotateImageController : MonoBehaviour {

	public float angle;
	public Image[] imgs;

	void Start () {
	
	}
	
	void Update(){
		foreach(Image i in imgs){
			i.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, angle));
		}
	}
}
