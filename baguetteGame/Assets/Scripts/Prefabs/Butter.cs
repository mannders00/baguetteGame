using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Butter : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		GameObject Level = GameObject.Find("Level");
		Level script = Level.GetComponent<Level>();
		script.butterCollect();
		Object.Destroy(gameObject);
	}
}
