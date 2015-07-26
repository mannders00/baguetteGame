using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour {

	public Rect ignore;
//	private Vector2 lastDelta = new Vector2(0, 0);

//	private Character script;
//	private float sens;

	void Start(){
//		script = gameObject.GetComponent<Character>();
//		sens = PlayerPrefs.GetFloat("Sensitivity");
	}
	void Update (){
		if(Input.touchCount > 0){
			for(int i=0; i < Input.touchCount; i++){
				Touch touch = Input.GetTouch(0);
				if(ignore.Contains(touch.position)){
					break;
				}else{
					if(touch.phase == TouchPhase.Moved){
//						Vector2 delta = touch.deltaPosition;

//						Vector2 newDelta = new Vector2(Mathf.Lerp(lastDelta.x, delta.x, Time.deltaTime), Mathf.Lerp(lastDelta.y, delta.y, Time.deltaTime));

//						script.horizontal = newDelta.x * sens;
//						script.vertical = newDelta.y * sens;
//
//						lastDelta = delta;
					}
				}
			}
		}
	}
}
