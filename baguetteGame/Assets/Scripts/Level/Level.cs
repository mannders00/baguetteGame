using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level : MonoBehaviour {

	void Update(){
		if(Input.GetKeyDown(KeyCode.Alpha0)){
			Application.LoadLevel(0);
		}
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			Application.LoadLevel(1);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			Application.LoadLevel(2);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			Application.LoadLevel(3);
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			Application.LoadLevel(4);
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)){
			Application.LoadLevel(5);
		}
		
	}
}