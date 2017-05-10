using UnityEngine;
using UnityEngine.UI; 
using System;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class DeadScreenController : MonoBehaviour {
    
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
        
        int topScore = PlayerPrefs.GetInt("TopScore");
        int score = player.GetComponent<PlayerController>().getScore();

        FindObjectOfType<ServiceController>().SetTop(score);

        if (score > topScore)
        {
            PlayerPrefs.SetInt("TopScore", score);
            topScore = score;
        }
        textScore.GetComponent<Text>().text = Convert.ToString(score);
        textTopScore.GetComponent<Text>().text = "TOP " + Convert.ToString(topScore);    
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<Canvas>().enabled = true;            
    }

	public void Skip () {
		gameObject.SetActive (false);
	}

	public void Click () {
		Skip ();
		Application.LoadLevel("main");
	}
}
