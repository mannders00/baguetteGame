using UnityEngine;
using System.Collections;

public class TouchTurn : MonoBehaviour {


	void Update (){
		Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
		print(touchDeltaPosition);
	}
}
