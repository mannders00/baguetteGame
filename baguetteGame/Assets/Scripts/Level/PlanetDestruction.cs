using UnityEngine;
using System.Collections;

public class PlanetDestruction : MonoBehaviour {

	private GameObject planet;
	public GameObject explosion;
	public Animator animator;
	public Transform player;
	public int planetInt;

	private Vector3 explosionPosition;
	private bool isMoving = false;

	public void Start(){
		planetInt = 0;
		switch(planetInt){
			case 0: break;
			case 1: planet = GameObject.Find("Planet1"); player.transform.position = new Vector3(-1500, 250, -500); player.transform.rotation = Quaternion.Euler(0, 180, 0); break;
			case 2: planet = GameObject.Find("Planet2"); player.transform.position = new Vector3(2469, -310, -59); player.transform.rotation = Quaternion.Euler(0, 0, 0); break;
			case 3: planet = GameObject.Find("Planet3"); player.transform.position = new Vector3(2000, 316, -2774); player.transform.rotation = Quaternion.Euler(0, 90, 0); break;
			case 4: planet = GameObject.Find("Planet4"); player.transform.position = new Vector3(-25, -291, -1676); player.transform.rotation = Quaternion.Euler(0, 90, 0); break;
			case 5: planet = GameObject.Find("Planet5"); player.transform.position = new Vector3(-494, 14, 1621); player.transform.rotation = Quaternion.Euler(0, -90, 0); break;
		}
		if(planetInt == 1 || planetInt == 2 || planetInt == 3 || planetInt == 4 || planetInt == 5){
			planet.GetComponent<SphereCollider>().enabled = false;
			explosionPosition = planet.transform.position;
			animator.SetTrigger("Explode");
			isMoving = true;
		}
	}
	public void Destruction(){
		GameObject explosionInstantiate = Instantiate(explosion, explosionPosition, Quaternion.identity) as GameObject;
		GameObject.Destroy(explosionInstantiate, 10);
		GameObject.Destroy(planet, 0.5F);
		planetInt = 0;
	}
	public void stopMoving(){
		isMoving = false;
	}
	void Update(){
		if(isMoving == true){
			player.transform.Translate(-transform.right * Time.deltaTime * 300);
		}
	}
}
