using UnityEngine;
using System.Collections;

public class SpaceLevel : MonoBehaviour {

	public GameObject explosion;
	public int planetSelection;

	public void Start(){
		GameObject instantiatedExplosion = Instantiate(explosion, new Vector3(-500, 0, 2000), Quaternion.identity) as GameObject;
		GameObject.Destroy(instantiatedExplosion, 5);
		planetSelection = 1;
		planetExplosionInit();
	}
	public void planetExplosionInit(){
		GameObject planet = GameObject.Find("Planet1");
		SphereCollider coll = GetComponent<SphereCollider>();
		GameObject.Destroy(planet, 1);
	}
}