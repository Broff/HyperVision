using UnityEngine;
using UnityEngine.UI; 
using System;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using googleServices;

public class DeadScreenController : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;

	public GameObject player;
	public GameObject textScore, textTopScore;
	public GameObject GSe;
    public float showTimer = 1.5f;

	public void View () {
        gameObject.SetActive(true);
        StartCoroutine(Show(showTimer));
	}

    private IEnumerator Show(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<Canvas>().enabled = true;

        int topScore = PlayerPrefs.GetInt("TopScore");
        int score = player.GetComponent<PlayerController>().getScore();

        googleAnalytics.LogEvent("Score", "get", "get_Score_" + score, score); //Write score

        if (score > topScore)
        {
            PlayerPrefs.SetInt("TopScore", score);
            topScore = score;
            googleAnalytics.LogEvent("Score", "newHirgscore", "get_Score_" + score, score); //Write score
        }
        textScore.GetComponent<Text>().text = Convert.ToString(score);
        textTopScore.GetComponent<Text>().text = "TOP " + Convert.ToString(topScore);
        GSe.GetComponent<GameSettings>().SetTop(score);
        GSe.GetComponent<GameSettings>().CheckAchivements();
        googleAnalytics.LogScreen("Dead Menu");	        
    }

	public void Skip () {
		gameObject.SetActive (false);
	}

	public void Click () {
		googleAnalytics.LogEvent("Button", "Click", "start_game_new", 1);	
		Skip ();
		Application.LoadLevel("main");
	}
}
