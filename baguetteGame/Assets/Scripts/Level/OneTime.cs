using UnityEngine;
using System.Collections;

public class Variables : MonoBehaviour {

	public int planetInt;

	void Start (){
		DontDestroyOnLoad(transform.gameObject);
	}
}
