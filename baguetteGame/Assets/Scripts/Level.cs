using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level : MonoBehaviour {

	public int score;
	public Text scoreBox;

	void Start(){
		/*This was the code to randomly spawn, i just plopped the global variables here
		public GameObject butterParent;
		public GameObject butter;
		public int butterAmount;
		public int radius;
		for(int i = 0; i < butterAmount; i++){
			GameObject butterClone;
			butterClone = Instantiate(butter, new Vector3(Random.Range(-radius, radius),
			 Random.Range(-radius, radius), Random.Range(-radius, radius)), Quaternion.identity) as GameObject;
			butterClone.transform.parent = butterParent.transform;
		}*/
	}
	public void addScore(){
		score += 1;
		scoreBox.text = ""+score;
	}
}