using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelRotate : MonoBehaviour
{

	public float sensitivity = 1;
	GyroManager gyro;
	Vector3 gyroValue;

	// Use this for initialization
	void Start () {

		gyro = GameObject.Find ("GyroManager").GetComponent<GyroManager> ();
		StartCoroutine ("panelRotate");

	}
	
	IEnumerator panelRotate()
	{
		gyroValue = gyro.getGyroValue ();
		transform.Rotate (-gyroValue.x * sensitivity, 0, -gyroValue.y * sensitivity);


		yield return new WaitForSeconds (0.01f);

		StartCoroutine ("panelRotate");
	}
}
