using UnityEngine;
using System.Collections;

public class EnemyBread : MonoBehaviour {

	public GameObject playerObject;
	public GameObject player;
	public int speed;
	public GameObject explosion;
	public int damage;

	private bool go = true;

	void Update () {
		if(playerObject){
			transform.LookAt(player.transform, Vector3.up);

	//		Vector3 positionHorizontal = new Vector3(playerObject.transform.position.x, transform.position.y, playerObject.transform.position.z);
			Vector3 direction = playerObject.transform.position - transform.position;
			Vector3 moveHorizontal = new Vector3(playerObject.transform.position.x, transform.position.y, playerObject.transform.position.z);
			RaycastHit hit;
			if(Physics.Raycast(transform.position, direction, out hit, 5)){
				if(hit.transform.tag != "Player"){
					transform.Translate(transform.up * Time.deltaTime * speed, Space.World);
					go = false;
				}
			}else if(Physics.Raycast(transform.position, Vector3.down, out hit, 2.5F)){
				if(hit.transform.tag != "Player"){
					transform.position = Vector3.MoveTowards(transform.position, moveHorizontal, speed * Time.deltaTime);
					go = false;
				}
			}else{
				go = true;
			}
			if(go){
				transform.position = Vector3.MoveTowards(transform.position, playerObject.transform.position, speed * Time.deltaTime);
			}
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
