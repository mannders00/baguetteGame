using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public int destroyTime;
	public Camera cam;
	public int speed;
	Vector3 projectileTarget;

	void Start(){
		cam = GameObject.Find("Camera").GetComponent<Camera>();
		projectileTarget = cam.ViewportToWorldPoint(new Vector3(0.497F, 0.51F, 300));
	}
	void Update(){
		transform.position = Vector3.MoveTowards(transform.position, projectileTarget, speed * Time.deltaTime);
		Destroy(gameObject, destroyTime);
	}
}
