using UnityEngine;
using System.Collections;

public class MainScreenController : MonoBehaviour {

	public GameObject ui;
    	
	void Start(){
	
	}
	
	public void View () {
		gameObject.SetActive (true);
	}

	public void Skip () {
		gameObject.SetActive (false);
	}

	public void Click () {
		Skip ();		
		ui.GetComponent<ScoreScreenController> ().View();	
	}
}
