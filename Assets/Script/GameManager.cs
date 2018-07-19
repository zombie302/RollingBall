using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    public GameObject[] feed;
    public GameObject[] panel;
    public GameObject[] exitTile;
    public Slider SensitivitySlider;
    public TouchPanelMove _panelMove;
    private UIManager _uiManager;
    private bool panelFlag;
    public int point;
    public int level;
    public int nowCubeCount;
    public int selectPanel;
    public int selectFeed;
	
// Use this for initialization
    void Start ()
    {
//feed = GameObject.FindGameObjectsWithTag("Feed");
        _uiManager = GameObject.FindObjectOfType<UIManager>();
        panel = GameObject.FindGameObjectsWithTag("Panel");
        _panelMove = panel[0].GetComponent<TouchPanelMove>();
        level = 1;
        point = 0;
        panelFlag = false;
        selectPanel = 0;
        selectFeed = 0;
        if (PlayerPrefs.HasKey("sensitivity"))
        {
            SensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");
        }
        StartCoroutine(SpawnFeed());
    }

    private void Update()
    {
        #if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        #endif
    }

    public void FeedIt()
    {
		
        for (int i = 0; i < feed.Length; i++)
        {
            if (feed[i].activeSelf)
            {
                feed[i].SetActive(false);
            }
        }


        point++;
		if ((point%3) == 0)
        {
            FindObjectOfType<PanelManager>().DecreasePanel();
        }
        _uiManager.setPoint();
        /*---------------------------------------------------*/
        /*
        List<GameObject> ChildCube = panel[selectPanel].GetComponent<PanelManager>().ChildCube;
        int red = (int) Random.Range(0, 5);
        int green = (int) Random.Range(0, 5);
        int blue = (int) Random.Range(0, 5);
		
        Color color = new Color(red,green,blue,255);

        if (panelFlag)
        {
            panelFlag = false;
        }
        else
        {
            panelFlag = true;
        }

        if (panelFlag)
        {
            for (int i = 0; i < ChildCube.Count; i++)
            {
				
                if (i % 2 == 0)
                {
                    ChildCube[i].GetComponent<MeshRenderer>().material.color = Color.white;
                }
                else
                {
                    ChildCube[i].GetComponent<MeshRenderer>().material.color = color;
                }
				
				
            }
        }
        else
        {
            for (int i = 0; i < ChildCube.Count; i++)
            {
				
                if (i % 2 != 0)
                {
                    ChildCube[i].GetComponent<MeshRenderer>().material.color = Color.white;
                }
                else
                {
                    ChildCube[i].GetComponent<MeshRenderer>().material.color = color;
                }
				
				
            }
            
        }
        */
        StartCoroutine(SpawnFeed());
    }

    IEnumerator SpawnFeed()
    {
        yield return new WaitForSeconds(0.5f);

        List<GameObject> panels = panel[selectPanel].GetComponent<PanelManager>().SpawnFeedTile;
        nowCubeCount = panels.Count;
        Debug.Log(nowCubeCount);
        int num = (int)Random.Range(0, nowCubeCount-1);
        if (num >= nowCubeCount || nowCubeCount == 0)
        {
            StartCoroutine(SpawnFeed());
        }
        else
        {
            if ((point == 0 && num == 4) || (point == 0 && num == 12))
            {
                StartCoroutine(SpawnFeed());
            }
            else
            {
//feed[0].transform.position = panels[num].transform.position + Vector3.up;
                Debug.Log(num);
                feed[selectFeed].transform.parent = panels[num].transform;
                feed[selectFeed].transform.position = panels[num].transform.position + Vector3.up;
                yield return new WaitForSeconds(0.3f);
                feed[selectFeed].SetActive(true);
            }
        }
    }
    
/*
    IEnumerator waitSecond(int num)
    {
        yield return new WaitForSeconds(1f);
           
        if (num > 1)
        {
            StartCoroutine(waitSecond(num - 1));
        }
    }
*/

    public void onClickRestartBtn()
    {
        Time.timeScale = 1.0f;
        _uiManager.turnOnPauseBtn();
        Application.LoadLevel(0);
    }

    public void onClickPauseBtn()
    {
        Time.timeScale = 0.0f;
        _uiManager.turnOnPauseGamePanel();
        _uiManager.turnOffPauseBtn();
    }

    public void onClickContinueBtn()
    {
        _uiManager.turnOffPauseGamePanel();
        _uiManager.turnOnPauseBtn();
        Time.timeScale = 1.0f;
    }

    public void onClickExitBtn()
    {
        Application.Quit();
    }
    
    public void onClickOptionBtn()
    {
        _uiManager.turnOnOption();
    }

    public void onClickOkBtn()
    {
        PlayerPrefs.SetFloat("sensitivity",SensitivitySlider.value);
        PlayerPrefs.Save();
        _panelMove.speed = PlayerPrefs.GetFloat("sensitivity");
        _uiManager.turnOffOption();
    }
}