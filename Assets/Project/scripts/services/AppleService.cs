using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

public class AppleService : MonoBehaviour, IService {

    [Header("ITunce")]
    public string iOS_LeaderboardID = "grp.co.smallgames.sis.Leaderboard";
    public string myID;
    public string shareText = "Try to beat my highscore in ";
    public string gameName;
    public string loadUrl;

    public GameObject shareConf;

    private string shareTextMesage;

    private static AppleService instance = null;

    public void Login()
    {
        #if UNITY_IOS && !UNITY_EDITOR
            Social.localUser.Authenticate ( 
            success => { 
            if (success) 
            { 
                Debug.Log("==iOS GC authenticate OK");
            } 
            else
            {
                Debug.Log("==iOS GC authenticate Failed");
            } } );
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
        #if UNITY_IOS && !UNITY_EDITOR
            GameCenterPlatform.ShowLeaderboardUI(iOS_LeaderboardID, UnityEngine.SocialPlatforms.TimeScope.AllTime);
            //Social.ShowLeaderboardUI();
        #endif
    }

    public void SetTop(int score)
    {
        bool isGCAuthenticated = Social.localUser.authenticated;
        #if UNITY_IOS && !UNITY_EDITOR
            if (isGCAuthenticated)
            {
                Social.ReportScore(score, iOS_LeaderboardID, success => { 
                if (success)
                { 
                    Debug.Log("==iOS GC report score ok: " + score + "\n");
                } 
                else
                {
                    Debug.Log("==iOS GC report score Failed: " + iOS_LeaderboardID + "\n");
                } });
            }
            else
            {
                Debug.Log("==iOS GC can't report score, not authenticated\n");
            }
        #endif
    }

    public void ShowAchivs()
    {
        #if UNITY_IOS && !UNITY_EDITOR
            Social.ShowAchievementsUI();
        #endif
    }

    public void Rate()
    {
        Application.OpenURL("itms-apps://itunes.apple.com/app/" + myID);
    }

    public void Share()
    {
        CreateText();
        shareConf.GetComponent<NativeShare>().Share();//.TakeScreen(shareText, loadUrl);
    }

    private void CreateText()
    {
        shareTextMesage = shareText + gameName;
    }
}
