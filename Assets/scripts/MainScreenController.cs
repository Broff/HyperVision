using UnityEngine;
using System.Collections;

public class MainScreenController : MonoBehaviour {

	public GameObject ui;
	public GoogleAnalyticsV3 googleAnalytics;
	
	void Start(){
	
	}
	
	public void View () {
		gameObject.SetActive (true);
		googleAnalytics.LogScreen("Main Menu");
	}

	public void Skip () {
		gameObject.SetActive (false);
	}

	public void Click () {
		Skip ();		
		ui.GetComponent<ScoreScreenController> ().View();	
		googleAnalytics.LogEvent("Button", "Click", "start_game_first", 1);
	}
}
