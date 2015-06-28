using UnityEngine;
using System.Collections;

public class EnemyBread : MonoBehaviour {

	public GameObject playerObject;
	public GameObject player;
	public int speed;
	public GameObject explosion;
	public int damage;

	void Update () {
		if(playerObject){
			transform.LookAt(player.transform, Vector3.up);
			transform.position = Vector3.MoveTowards(transform.position, playerObject.transform.position, speed * Time.deltaTime);
		}
	}
	void Hit(){
		Instantiate(explosion, transform.position, Quaternion.identity);
		GameObject.Destroy(gameObject);
	}
	void OnTriggerEnter(Collider other){
		if(other.tag != "CanBeShot" && other.tag == "Player"){
			player.SendMessage("changeHealth", 25, SendMessageOptions.DontRequireReceiver);
			Hit();
		}
	}
}
