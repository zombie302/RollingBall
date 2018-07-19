using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class AdManager : MonoBehaviour {
    
    public static AdManager _AdManager = null;
    public int PlayGameCount = 0;
    public int CountMax = 0;
    
    private void Awake()
    {
        if (_AdManager == null)
        {
            _AdManager = this;
        }else if (_AdManager != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("Rhe ad failed to be shown.");
                break;
				
        }
    }

    public void ShowEndGameAd()
    {
        PlayGameCount++;
        Debug.Log("showEndGameAd Start");
        if (CountMax == 0)
        {
            CountMax = RandomNum();
        }
        if (PlayGameCount >= CountMax)
        {
            if (Advertisement.IsReady("rewardedVideo"))
            {
                ShowOptions options = new ShowOptions
                {
                    resultCallback = HandleShowResult
                };
                Advertisement.Show("rewardedVideo", options);
                CountMax = RandomNum();
            }
            else
            {
                Debug.Log("endGameVideo is not ready");
            }

            PlayGameCount = 0;
        }
    }

    private int RandomNum()
    {
        //int num = (int) Random.Range(7,10);
        int num = 2111111111;
        return num;
    }
}
