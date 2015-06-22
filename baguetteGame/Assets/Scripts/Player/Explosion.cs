using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	void Start (){
		GameObject.Destroy(gameObject, 1.5F);
	}
}
