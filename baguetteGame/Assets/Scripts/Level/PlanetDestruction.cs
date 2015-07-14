using UnityEngine;
using System.Collections;

public class PlanetDestruction : MonoBehaviour {

	public GameObject planet;
	public int planetInt;
	public Vector3 explosionPosition;
	public GameObject explosion;

	void Start(){
		planetInt = 1;
		DestructionInit(planetInt);

	}
	void DestructionInit(int planetIntInp){
		planetInt = planetIntInp;
		switch(planetInt){
			case 1: planet = GameObject.Find("Planet1"); break;
			case 2: planet = GameObject.Find("Planet2"); break;
			case 3: planet = GameObject.Find("Planet3"); break;
			case 4: planet = GameObject.Find("Planet4"); break;
			case 5: planet = GameObject.Find("Planet5"); break;
		}
		planet.GetComponent<SphereCollider>().enabled = false;
		explosionPosition = planet.transform.position;
	}

}
