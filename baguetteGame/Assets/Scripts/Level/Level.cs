using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level : MonoBehaviour {

	public GameObject pedestal;
	public GameObject butter;
	public GameObject dust;
	public GameObject player;

	public float pedestalSpeed = 1;
	Vector3 spawnUnderPos;
	Vector3 spawnGroundPos;
	private bool isSpawned = false;

	public float boundary;

	public void SetupPedestal(Vector3 hitPoint){
		// this SpawnGroundPos is a vector3 position that is taken from the boss after it dies,
		// basically when it dies i want it to spawn this little pedestal where it explodes
		spawnGroundPos = hitPoint;
		spawnUnderPos = new Vector3(spawnGroundPos.x, spawnGroundPos.y - 10, spawnGroundPos.z);
	//	spawnUpPos = new Vector3(spawnGroundPos.x, spawnGroundPos.y + 5.4F, spawnGroundPos.z);
	}
	public void SpawnPedestal(){
		pedestal.SetActive(true);
		pedestal.transform.position = spawnUnderPos;
		Vector3 butterLocation = new Vector3(spawnGroundPos.x, spawnGroundPos.y + 16, spawnGroundPos.z);
		Instantiate(butter, butterLocation, Quaternion.identity);
		Instantiate(dust, butterLocation, Quaternion.identity);
		isSpawned = true;

	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.Alpha0)){
			Application.LoadLevel(0);
		}
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			Application.LoadLevel(1);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			Application.LoadLevel(2);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			Application.LoadLevel(3);
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			Application.LoadLevel(4);
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)){
			Application.LoadLevel(5);
		}
		if(isSpawned){
			pedestal.transform.position = Vector3.Lerp(pedestal.transform.position, spawnGroundPos, Time.deltaTime * pedestalSpeed);
		}
		player.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, -boundary, boundary),
		Mathf.Clamp(player.transform.position.y, -500, 500), Mathf.Clamp(player.transform.position.z, -boundary, boundary));
	}
	public void butterCollect(){
		print("butterr");
	}
}