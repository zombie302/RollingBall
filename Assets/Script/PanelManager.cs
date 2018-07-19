﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public List<GameObject> ChildCube;		//패널의 액티브 되있는 큐브들
	public List<GameObject> SpawnFeedTile;	//먹이가 나올 큐브들
    public Color nowColor;		//색을 바꾸기 위한 객체
	public int[] removeCubes;
	public int[] feedArea;
	int currentRemover;

    // Use this for initialization
    void Start ()
    {
        Transform[] tempTransform = this.GetComponentsInChildren<Transform>();	//패널아래 모든 오브젝트의 Transform 정보를 가져온다
        int count = 0;			//?
        foreach (Transform child in tempTransform)			//큐브를 ChildCube에 널기위한 반복문 
        {
            if (child.name.Contains("Cube"))		//게임오브젝트 이름에 Cube가 포함되어 있다면
            {
                ChildCube.Add(child.gameObject);	//배열에 추가
            }
        }
		currentRemover = 0;


        SetPanel();		//	패널 재 배열
		setFeedArea ();
      /*  int red = (int) Random.Range(0, 5);
        int green = (int) Random.Range(0, 5);
        int blue = (int) Random.Range(0, 5);
		
        Color color = new Color(red,green,blue,255);
        
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
        StartCoroutine(ChangeColor(true));*/
		
        //StartCoroutine(ChangeColor());		//패널의 색 바꾸기
    }

	public void setFeedArea()
	{

		SpawnFeedTile = new List<GameObject>();

		for(int i = 0; i < feedArea.Length; i++)
		{
			SpawnFeedTile.Add(ChildCube[feedArea[i]]);
		}

	}

	//패널의 구성을 ChildCube의 값대로 재 배열 하기위한 메서드
    public void SetPanel()
    {	
        int CubeCount = 0;		//현재 액티브 되어있는 큐브의 수 

		//ChildCube의 수에 맞게 count 증가
        for (int i = 0; i < ChildCube.Count; i++)
        {
            if (ChildCube[i].activeInHierarchy)	//해당 오브젝트가 액티브 상태면
            {
                CubeCount++; //카운트 증가
            }
        }
        
        int sqrtNum = (int) Mathf.Sqrt(CubeCount);	//액티브 상태의 큐브 수의 제곱근 구하기
        Debug.Log("sqrtNum : " + sqrtNum);
        float blockDistance = 1.5f;		//블록간 거리
        float checkValue = sqrtNum / 2 * blockDistance;	//가장자리에 있는지
        
        float vecX;
        float vecZ;

		//큐브 위치 좌표
        vecX = sqrtNum / 2 * blockDistance;
        vecZ = sqrtNum / 2 * blockDistance;
        
       // SpawnFeedTile = new List<GameObject>();	//먹이 리스트 초기화
        Quaternion quaternion = this.transform.rotation;	//현재 위치를 저장
        this.transform.rotation = new Quaternion();	//새로운 Quaternion 생성

		//
        for (int i = 0; i < CubeCount; i++)
        {
           // ChildCube[i].transform.position = this.transform.position + new Vector3(vecX, 0, vecZ);
            ChildCube[i].transform.parent = this.gameObject.transform;	//큐브의 부모객체를 패널로 설정
            ChildCube[i].transform.position = new Vector3(vecX, 0, vecZ); //큐브의 위치 설정

			//패널의 왼쪽 끝 위치 보다 작으면
            if (vecX <= -checkValue)
            {
				//왼쪽으로 한칸 이동
                vecZ -= blockDistance;
                vecX = checkValue;
            }
            else //왼쪽끝 보다 크면 
            {
				//마지막 위치 설정
                vecX -= blockDistance;
            }



			/*
			//패널의 크기가 5 * 5 라면
            if (sqrtNum >= 3)
            {
                int num = i + 1;
				if (num < sqrtNum || num % sqrtNum == 0 || num % sqrtNum == 1 || num > sqrtNum * (sqrtNum - 1)) {	//패널의 범위를 벗어나지 않으면 
					
				} else {
					SpawnFeedTile.Add (ChildCube [i]);	//음식이 나오는 타일로 추가
				}
            }
            else 	//아니면 
            {
                SpawnFeedTile = ChildCube;	//childCube로 초기화
            }
			*/
        }

        this.transform.rotation = quaternion;	//재배열 전의 회전값으로 변경
    }

	//색 변경 코루틴
    IEnumerator ChangeColor()
    {
        float progress = 0;
        float increment = 0.02f / 2;
        if (nowColor == Color.white)
        {
            randomColor();
            while (progress < 1)
            {
                for (int i = 0; i < ChildCube.Count; i++)
                {
                    ChildCube[i].GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, nowColor, progress);
                }
                progress += increment;
                yield return new WaitForSeconds(0.02f);
            }
        }
        else
        {
            while (progress < 1)
            {
                for (int i = 0; i < ChildCube.Count; i++)
                {
                    ChildCube[i].GetComponent<MeshRenderer>().material.color = Color.Lerp(nowColor, Color.white, progress);
                }
                progress += increment;
                yield return new WaitForSeconds(0.02f);
            }
            nowColor = Color.white;
        }
        StartCoroutine(ChangeColor());
    }

	//색 랜덤 지정
    public void randomColor()
    {
        int choseColor = (int)Random.Range(0, 5);
        switch (choseColor)
        {
                case 0:
                    nowColor = Color.red;
                    break;
                case 1:
                    nowColor = Color.green;
                    break;
                case 2:
                    nowColor = Color.blue;
                    break;
                case 3:
                    nowColor = Color.yellow;
                    break;
                case 4:
                    nowColor = Color.black;
                    break;
                case 5:
                    nowColor = Color.magenta;
                    break;
        }
    }
    /*IEnumerator ChangeColor(bool nono)
    {
        yield return new WaitForEndOfFrame();

        int red = (int) Random.Range(0, 5);
        int green = (int) Random.Range(0, 5);
        int blue = (int) Random.Range(0, 5);
		
        Color color = new Color(red,green,blue,255);

        if (nono)
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

        yield return  new WaitForSeconds(0.4f);
        StartCoroutine(ChangeColor(!nono));
    }*/

	//패널 떨어 뜨리기
    public void DecreasePanel()
    {
        int RandomNum;
		
        if (currentRemover >= removeCubes.Length) {
			return;
		}

        while (true)
        {
            RandomNum = (int) Random.Range(0, removeCubes.Length);
            if (ChildCube[removeCubes[RandomNum]].activeSelf)
            {
                StartCoroutine(animationPanel(ChildCube[removeCubes[RandomNum]]));
                break;
            }
        }
        currentRemover++;
    }

	//큐브를 떨어 뜨리기 위한 메서드
    IEnumerator animationPanel(GameObject target)
    {
        target.GetComponent<Animation>().Play("cube_Ani" + Random.Range(0, 2));
        yield return new WaitForSeconds(1.5f);
		target.SetActive(false);
		//SetPanel();
    }
}