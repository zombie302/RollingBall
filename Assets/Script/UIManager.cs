using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	public Text point;
	public Text bestText;
	public Text nowText;
	public GameObject uiPanel;
	public GameObject pausePanel;
	public GameObject optionPanel;
	public GameObject tutorialPanel;
	public GameObject pauseBtn;
	private GameManager _gameManager;
	public AudioSource cameraAudio;
	
	// Use this for initialization
	void Start ()
	{
		_gameManager = GameObject.FindObjectOfType<GameManager>();
		cameraAudio.enabled = true;
	}

	private void Awake()
	{
		if (!PlayerPrefs.HasKey("isFirst"))
		{
			tutorialPanel.SetActive(true);
		}
	}

	public void setPoint()
	{
		point.text = _gameManager.point.ToString();
	}

	public void setTextResult(string best, string score)
	{
		bestText.text = best;
		nowText.text = score;
	}

	public void turnOnUiPanel()
	{
		uiPanel.SetActive(true);
		cameraAudio.enabled = false;
		
	}

	public void turnOnPauseGamePanel()
	{
		pausePanel.SetActive(true);
	}

	public void turnOffPauseGamePanel()
	{
		pausePanel.SetActive(false);
	}

	public void turnOnOption()
	{
		optionPanel.SetActive(true);
	}
	
	public void turnOffOption()
	{
		optionPanel.SetActive(false);
	}

	public void turnOnPauseBtn()
	{
		pauseBtn.SetActive(true);
	}
	
	public void turnOffPauseBtn()
	{
		pauseBtn.SetActive(false);
	}

	public void tutorialDrag()
	{
		PlayerPrefs.SetInt("isFirst", 1);
		tutorialPanel.SetActive(false);
	}
	
}
