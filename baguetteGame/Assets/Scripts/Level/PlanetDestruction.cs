using UnityEngine;
using System.Collections;

public class PlanetDestruction : MonoBehaviour {

	private GameObject planet;
	public GameObject explosion;
	public Animator animator;
	public Transform player;

	private Vector3 explosionPosition;
	private bool isMoving = false;
	public int planetIntVar;
	public bool resetPlayerPrefs;

	public void Start(){
		if(resetPlayerPrefs == true){
			PlayerPrefs.SetInt("Planet1", 0); PlayerPrefs.SetInt("Planet2", 0); PlayerPrefs.SetInt("Planet3", 0); PlayerPrefs.SetInt("Planet4", 0); PlayerPrefs.SetInt("Planet5", 0); 
			PlayerPrefs.SetInt("Progress", 0);
		}
		print("Player has finished "+PlayerPrefs.GetInt("Progress")+" levels");
		if(PlayerPrefs.GetInt("Progress") == 5){
			Application.LoadLevel(7);
		}

		planetIntVar = PlayerPrefs.GetInt("Planet");

		if(PlayerPrefs.GetInt("Planet1") == 1 && planetIntVar != 1){GameObject go = GameObject.Find("Planet1"); GameObject.Destroy(go);}
		if(PlayerPrefs.GetInt("Planet2") == 1 && planetIntVar != 2){GameObject go = GameObject.Find("Planet2"); GameObject.Destroy(go);}
		if(PlayerPrefs.GetInt("Planet3") == 1 && planetIntVar != 3){GameObject go = GameObject.Find("Planet3"); GameObject.Destroy(go);}
		if(PlayerPrefs.GetInt("Planet4") == 1 && planetIntVar != 4){GameObject go = GameObject.Find("Planet4"); GameObject.Destroy(go);}
		if(PlayerPrefs.GetInt("Planet5") == 1 && planetIntVar != 5){GameObject go = GameObject.Find("Planet5"); GameObject.Destroy(go);}

		switch(planetIntVar){
			case 0: break;
			case 1: planet = GameObject.Find("Planet1"); player.transform.position = new Vector3(-1500, 250, -500); player.transform.rotation = Quaternion.Euler(0, 180, 0); break;
			case 2: planet = GameObject.Find("Planet2"); player.transform.position = new Vector3(2469, -310, -59); player.transform.rotation = Quaternion.Euler(0, 0, 0); break;
			case 3: planet = GameObject.Find("Planet3"); player.transform.position = new Vector3(2000, 316, -2774); player.transform.rotation = Quaternion.Euler(0, 90, 0); break;
			case 4: planet = GameObject.Find("Planet4"); player.transform.position = new Vector3(-25, -291, -1676); player.transform.rotation = Quaternion.Euler(0, 90, 0); break;
			case 5: planet = GameObject.Find("Planet5"); player.transform.position = new Vector3(-494, 14, 1621); player.transform.rotation = Quaternion.Euler(0, -90, 0); break;
		}
		if(planetIntVar == 1 || planetIntVar == 2 || planetIntVar == 3 || planetIntVar == 4 || planetIntVar == 5){
			planet.GetComponent<SphereCollider>().enabled = false;
			explosionPosition = planet.transform.position;
			animator.SetTrigger("Explode");
			isMoving = true;
		}
		PlayerPrefs.SetInt("Planet", 0);
	}
	public void Destruction(){
		GameObject explosionInstantiate = Instantiate(explosion, explosionPosition, Quaternion.identity) as GameObject;
		GameObject.Destroy(explosionInstantiate, 10);
		GameObject.Destroy(planet, 0.5F);
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
