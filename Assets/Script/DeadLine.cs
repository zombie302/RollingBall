using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class DeadLine : MonoBehaviour
{

	public Text score;
	public UIManager uiManager;
	public GameObject deadSound;
	public AdManager adManager;


	void Start()
	{		
		adManager =  FindObjectOfType<AdManager>();
		deadSound.SetActive(false);	
	}
	
	private void OnTriggerEnter(Collider col)
	{		
		StartCoroutine(ShowAd());
		if (!PlayerPrefs.HasKey("High Score"))
		{
			PlayerPrefs.SetInt("High Score", 0);
			PlayerPrefs.Save();
		}
		if (PlayerPrefs.GetInt("High Score") < Int32.Parse(score.text))
		{
			PlayerPrefs.SetInt("High Score", Int32.Parse(score.text));
		}
		PlayerPrefs.Save();
		uiManager.setTextResult(PlayerPrefs.GetInt("High Score").ToString(), score.text);
		uiManager.turnOnUiPanel();
		
		deadSound.SetActive(true);
	}

	IEnumerator ShowAd()
	{
		yield return new WaitForEndOfFrame();
		adManager.ShowEndGameAd();
	}
}
