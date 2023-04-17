using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Collections.Generic;
using TMPro;

public class GooglePlayGamesManager : MonoBehaviour
{
    private static GooglePlayGamesManager instance;
    public bool debugMode = false;

    public static GooglePlayGamesManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GooglePlayGamesManager>();
                if (instance == null)
                {
                    instance = new GameObject("GooglePlayGamesManager").AddComponent<GooglePlayGamesManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(OnSignInResult);
    }

    private void OnSignInResult(SignInStatus signInStatus)
    {
        if (signInStatus == SignInStatus.Success)
        {
            Debug.Log("Authenticated. Hello, " + Social.localUser.userName + " (" + Social.localUser.id + ")");
        }
        else
        {
            Debug.Log("*** Failed to authenticate with " + signInStatus);
        }

        ShowEffect(signInStatus == SignInStatus.Success);
    }
    
    internal void ShowEffect(bool success)
    {
      if (debugMode == true){
        Camera.main.backgroundColor =
            success ? new Color(0.0f, 0.0f, 0.8f, 1.0f) : new Color(0.8f, 0.0f, 0.0f, 1.0f);
            }
    }

    internal void DoAuthenticate()
    {
        Debug.Log("Authenticating...");
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.ManuallyAuthenticate(OnSignInResult);
    }

    public void ShowLeaderboards()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            this.DoAuthenticate();
        }
    }

    public void ShowAchievements()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
        else
        {
            this.DoAuthenticate();
        }
    }
}
