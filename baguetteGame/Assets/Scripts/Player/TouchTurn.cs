using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour {


	void Update (){
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).position.x > Screen.width/2){
        	Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
        	print(touchDeltaPosition);
        }
	}
}
