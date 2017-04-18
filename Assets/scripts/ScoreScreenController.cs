using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScreenController : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;

	public GameObject player, tapToPlay, button;
	public Color co1,co2;	
	public float timeSwitch;
	float delta = 0;
	bool a = false;
	bool state = true;
	public Text score;
	public Image c1, c2, c3;
	public Image r1, r2, r3;
	public GameObject colorController;
	
	void Start(){
		c1.GetComponent<Image>().color = colorController.GetComponent<ColorController>().getColorCircleValue(1);
		c2.GetComponent<Image>().color = colorController.GetComponent<ColorController>().getColorCircleValue(2);
		c3.GetComponent<Image>().color = colorController.GetComponent<ColorController>().getColorCircleValue(3);
	}
	
	void Update(){
		if(state == true){
			if(delta >= timeSwitch){
				delta = 0;
				a = !a;
			} 
			
			if(a == false){
				tapToPlay.GetComponent<Image>().color = Color.Lerp(co1, co2, delta / timeSwitch);			
				delta += Time.deltaTime;
			} else {
				tapToPlay.GetComponent<Image>().color = Color.Lerp(co2, co1, delta / timeSwitch);			
				delta += Time.deltaTime;
			}			
		}
	}
	
	public void setColors(int num){
		//c1.GetComponent<Image>().color = colorController.GetComponent<ColorController>().getColorCircleValue(num);
		//c2.GetComponent<Image>().color = colorController.GetComponent<ColorController>().getColorCircleValue(num+1);
		//c3.GetComponent<Image>().color = colorController.GetComponent<ColorController>().getColorCircleValue(num+2);
		offRings();
		switch(num){
			case 1:
				r1.enabled = true;
			break;
			case 2:
				r2.enabled = true;
			break;
			case 3:
				r3.enabled = true;
			break;
			default :
				r3.enabled = true;
			break;
		}
	}
	
	void offRings(){
		r1.enabled = false;
		r2.enabled = false;
		r3.enabled = false;
	}
	
	public void TapToPlay(){
		googleAnalytics.LogEvent("Button", "Click", "start_game", 1);	
		tapToPlay.SetActive(false);
		button.SetActive(false);
		state = false;
		player.GetComponent<PlayerController> ().setGameState (true);
		player.GetComponent<PlayerController> ().playerMove();
		googleAnalytics.LogScreen("Play Screen");
	}
	
	public void View () {
		gameObject.SetActive (true);
		googleAnalytics.LogScreen("Game Screen");
	}

	public void Skip () {
		gameObject.SetActive (false);
	}

	public void updateScore (int i) {
		score.GetComponent<Text>().text = ""+i;
	}
}
