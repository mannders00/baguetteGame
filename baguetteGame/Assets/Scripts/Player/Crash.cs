using UnityEngine;
using System.Collections;

public class Crash : MonoBehaviour {

	public GameObject explosion;
	public GameObject impact;
	public float knockback;

	public GameObject player;
	private Vector3 backward;

	void OnTriggerEnter(Collider other){
		if(other.tag == "CanBeShot"){
			Vector3 impactDirection = other.transform.position - transform.position;
			impactDirection.Normalize();
			Vector3 impactPos = transform.TransformPoint(-14.65F, -3.35F, -0.22F);
			Quaternion impactRot = Quaternion.Euler(player.transform.localEulerAngles.x - 40, player.transform.localEulerAngles.y + 90, player.transform.localEulerAngles.z - 90);
			GameObject clone = Instantiate(impact, impactPos, impactRot) as GameObject;
			Destroy(clone, 1);
			player.GetComponent<Rigidbody>().velocity = -impactDirection * knockback;
		}else if(other.tag == "NoCollide"){
			
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
