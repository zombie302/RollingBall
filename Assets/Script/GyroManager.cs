using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroManager : MonoBehaviour {

	Gyroscope gyro;
	Vector3 gyroValue;

	void Awake(){

		gyro = Input.gyro;
		Input.gyro.enabled = true;

	}

	void Start () {
		
		StartCoroutine ("getGyro");

	}

	//자이로 센서 값 받아오기
	IEnumerator getGyro()
	{
		gyroValue = gyro.rotationRate;

		yield return new WaitForSeconds(0.01f);

		StartCoroutine ("getGyro");

	}

	//자이로 회전값
	public Vector3 getGyroValue(){

		return gyroValue;

	}

}
