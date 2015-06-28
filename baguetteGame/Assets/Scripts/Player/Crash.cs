using UnityEngine;
using System.Collections;

public class Crash : MonoBehaviour {

	public GameObject explosion;
	public GameObject impact;

	public GameObject player;
	private Vector3 backward;

	void OnTriggerEnter(Collider other){
		if(other.tag == "CanBeShot"){
			Vector3 impactPos = transform.TransformPoint(-14.65F, -3.35F, -0.22F);
			Quaternion impactRot = Quaternion.Euler(player.transform.localEulerAngles.x - 40, player.transform.localEulerAngles.y + 90, player.transform.localEulerAngles.z - 90);
			GameObject clone = Instantiate(impact, impactPos, impactRot) as GameObject;
			Destroy(clone, 1);
			player.GetComponent<Rigidbody>().velocity = transform.right * 30;
		}else{
			Explode();
		}
	}
	void Explode(){
		Instantiate(explosion, transform.position, Quaternion.identity);
		GameObject.Destroy(gameObject);
		player.SendMessage("Explode", SendMessageOptions.RequireReceiver);
	}
}
