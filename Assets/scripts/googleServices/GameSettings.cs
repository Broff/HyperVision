using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using System;

namespace googleServices{
	public class GameSettings : MonoBehaviour
	{
		public GoogleAnalyticsV3 googleAnalytics;
		
		string text = "";
		public string url = "https://play.google.com/store/apps/details?id=com.indieroom.hypervision";
		public string subject = "Hyper Vision";
		public string myID;
		string TopIDstring = ConstGoogle.leaderboard_top_players_hyper_vision;

		void CreateText()
		{
			text = "Try to beat my highscore in " + subject;
		}

		public void SetTop(int score)
		{
			Social.ReportScore(score, TopIDstring, (bool success) => {
			// handle success or failure
			});
		}

		public void CheckAchivements()
		{
			/*int games = PlayerPrefs.GetInt("allGames");
			games++;
			PlayerPrefs.SetInt("allGames", games);
			googleAnalytics.LogEvent("Games", "end", "end_game_Number_"+games , games ); //Write games count
			if(games >= 10){
				Debug.Log("More then 10");
				//PlayGamesPlatform.Instance.unlockAchievement(ConstGoogle.achievement_play_10_games, AchResult);	
				Social.ReportProgress(ConstGoogle.achievement_play_10_games, 100.0f, (bool success) => {
					if(success == true){
						googleAnalytics.LogEvent("Achievement", "Unlocked", "play_10_games", 10);
					}
				});			
			} 
			if(games >= 100){
				Social.ReportProgress(
					ConstGoogle.achievement_play_100_games, 100.0f, (bool success) => {
					if(success == true){
						googleAnalytics.LogEvent("Achievement", "Unlocked", "play_100_games", 100);
					}
				});
			} 
			
			int topScore = PlayerPrefs.GetInt("TopScore");
			
			if(topScore >= 30){
				Social.ReportProgress(
					ConstGoogle.achievement_already_no_stranger, 100.0f, (bool success) => {
					if(success == true){
						googleAnalytics.LogEvent("Achievement", "Unlocked", "get_30_score", 30);
					}
				});
			}
			if(topScore >= 150){
				Social.ReportProgress(
					ConstGoogle.achievement_almost_professional, 100.0f, (bool success) => {
					if(success == true){
						googleAnalytics.LogEvent("Achievement", "Unlocked", "get_1000_score", 100);
					}
				});
			}
			if(topScore >= 300){
				Social.ReportProgress(
					ConstGoogle.achievement_the_best_of_the_best, 100.0f, (bool success) => {
						if(success == true){
						googleAnalytics.LogEvent("Achievement", "Unlocked", "get_300_score", 300);
					}
				});
			}*/
		}

		static bool gServiceShow = false;
		static bool gServiceAuth = false;
		void Start()
		{
			if(gServiceShow == false){
				showGService();
			}
			gServiceShow = true;
		}
		
		bool showGService(){
			GooglePlayGames.PlayGamesPlatform.Activate();
			Social.localUser.Authenticate((bool success) => {
				gServiceAuth = success;
				if (success == true)
				{
					googleAnalytics.LogEvent("GoogleService", "login", "login_true", 1);						
					//Debug.Log("success");
				}
				else
				{
					googleAnalytics.LogEvent("GoogleService", "login", "login_fail", 0);
					//Debug.Log("fail");
				}
			});
			return true;
		}
		
		public void ShowAchivements()
		{			
			if(gServiceAuth == true){			
				Social.ShowAchievementsUI();
			} else {
				bool web = showGService();
				if(web == false){
				
				}
			}
			googleAnalytics.LogEvent("Button", "Click", "show_achives", 1);
		}

		public void ShowTop()
		{			    
			if(gServiceAuth == true){			
				PlayGamesPlatform.Instance.ShowLeaderboardUI(TopIDstring);
			} else {
				bool web = showGService();
				if(web == false){
				
				}
			}
			googleAnalytics.LogEvent("Button", "Click", "show_top", 2);    
		}

		public void Rate()
		{			
			Application.OpenURL("market://details?id=" + myID);
			googleAnalytics.LogEvent("Button", "Click", "Open_Rate_window", 3);
		}
		/// <summary>
		/// /////////////////////////
		/// </summary>

		public void Share()
		{			
			CreateText();
			StartCoroutine(ShareScreenshot());
			googleAnalytics.LogEvent("Button", "Click", "Open_Share_window", 4);
		}


		IEnumerator ShareScreenshot()
		{
			// wait for graphics to render
			yield return new WaitForEndOfFrame();
			//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
			// create the texture
			Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);

			// put buffer into texture
			screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);

			// apply
			screenTexture.Apply();
			//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO

			byte[] dataToSave = screenTexture.EncodeToPNG();

			string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");

			File.WriteAllBytes(destination, dataToSave);

			if (!Application.isEditor)
			{
				AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
				AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
				intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
				AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");

				AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);

				intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
				intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), text + "\n" + url);
				intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);

				intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
				AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

				currentActivity.Call("startActivity", intentObject);
				// option two WITH chooser:
				/*AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "YO BRO! WANNA SHARE?");
				currentActivity.Call("startActivity", jChooser);*/
			}
		}
	}
}
