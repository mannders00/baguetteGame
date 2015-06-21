using UnityEngine;
using System.Collections;

public class Crash : MonoBehaviour {

	public GameObject explosion;
	public GameObject player;

	void OnTriggerEnter(Collider other){
		if(other.tag != "CanBeShot"){
			Explode();
		}
	}
	void Explode(){
		Instantiate(explosion, transform.position, Quaternion.identity);
		GameObject.Destroy(gameObject);
		player.SendMessage("Explode", SendMessageOptions.RequireReceiver);
	}
}
