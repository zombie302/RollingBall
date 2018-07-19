using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPanelMove : MonoBehaviour {

	public float speed;
	Vector2 firstPoint;
	Vector2 dragPoint;


	void Start(){
		
		firstPoint = Vector2.zero;
		if (PlayerPrefs.HasKey("sensitivity"))
		{
			speed = PlayerPrefs.GetFloat("sensitivity");
		}
		
	}
		

	public void drag(){

		if (Input.touchCount > 0) {
			if (firstPoint == Vector2.zero) {
				firstPoint = Input.GetTouch (0).position;
				return;
			}

			dragPoint = (Input.GetTouch (0).position - firstPoint).normalized;
			Vector3 rotate = new Vector3(dragPoint.y, 0, -dragPoint.x);

			transform.Rotate (rotate * speed * Time.deltaTime);
			firstPoint = Input.GetTouch (0).position;
			
		}



	}

	public void pointerUp(){
		
		firstPoint = Vector2.zero;
		dragPoint = Vector2.zero;

	}


}
