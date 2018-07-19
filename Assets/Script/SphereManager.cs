using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
	private GameManager gameManager;
	public GameObject eatSound;
	
	private void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Feed")
		{
			gameManager.FeedIt();
			StartCoroutine(eatSoundActive());
		}
	}

	IEnumerator eatSoundActive()
	{
		eatSound.SetActive(true);
		
		yield return new WaitForSeconds(0.5f);
		
		eatSound.SetActive(false);
	}
}
