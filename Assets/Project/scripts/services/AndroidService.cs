
#if UNITY_ANDROID && !UNITY_EDITOR
    using GooglePlayGames;
    using GooglePlayGames.BasicApi;
    using UnityEngine.SocialPlatforms;
#endif
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AndroidService : MonoBehaviour, IService {

    public string myID;
    public string shareText = "Try to beat my highscore in ";
    public string gameName;
    public string loadUrl;
    private string shareTextMesage;

    private static AndroidService instance = null;
        
    public void Login()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            GooglePlayGames.PlayGamesPlatform.Activate();
            Social.localUser.Authenticate((bool success) =>
            {
            });
        #endif
    }

    private void Singletone()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowTop()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIdsNew.leaderboard_top);
        #endif
    }

    public void SetTop(int i)
    {    
        #if UNITY_ANDROID && !UNITY_EDITOR
		    Social.ReportScore(i, GPGSIdsNew.leaderboard_top, (bool success) => {
            // handle success or failure
		    });
        #endif
    }

    public void ShowAchivs()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            Social.ShowAchievementsUI();
        #endif
    }

    public void Rate()
    {
        Application.OpenURL("market://details?id=" + myID);
    }
   
    public void Share()
    {
        CreateText();
        StartCoroutine(ShareScreenshot());
    }

    private void CreateText()
    {
        shareTextMesage = shareText + gameName;
    }

    IEnumerator ShareScreenshot()
    {
        // wait for graphics to render
        yield return new WaitForEndOfFrame();

        #if UNITY_ANDROID && !UNITY_EDITOR
            
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
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareTextMesage + "\n" + loadUrl);
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), gameName);

                intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
                AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

                currentActivity.Call("startActivity", intentObject);
                // option two WITH chooser:
                /*AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "YO BRO! WANNA SHARE?");
			    currentActivity.Call("startActivity", jChooser);*/
            }
        #endif
    }
}