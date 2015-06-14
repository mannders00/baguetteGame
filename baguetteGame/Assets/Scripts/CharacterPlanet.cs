using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterPlanet : MonoBehaviour {
	
	public float rotateSpeed = 50;
	public float flySpeed = 200;

	public Camera cam;
	public Rigidbody rb;
	public bool flying;
	public GameObject thruster;
	public Animator animator;
	public GameObject projectile;
	public GameObject projectileParent;
	private float cooldown = 0.5F;
	public float projectileForce;
//	public GameObject turboImage;
//	public Text header;
	public Transform rocket;

	private Vector3 velocity = Vector3.zero;
	private bool turbo;
	private bool locked;
	private int planetInt;
	private bool cursorLockState = true;

	private Quaternion rocketRotation = Quaternion.identity;

	void Start(){
		cooldown = 0;
		turbo = false;
	//	turboImage.GetComponent<Image>().color = Color.green;
		locked = false;
	}
	void Update(){

		float x1;
		if(turbo == true){
			x1 = 50;
		}else{
			x1 = 10;
		}
		
		//rocket.transform.localEulerAngles = Vector3.Lerp(rocket.transform.localEulerAngles, new Vector3(45, 0, 0), Time.deltaTime * 5);
		rocket.transform.localRotation = Quaternion.Lerp(rocket.transform.localRotation, rocketRotation, Time.deltaTime * 3);

		Vector3 position1 = transform.TransformPoint(x1, 0, 0);
		Vector3 position2 = transform.TransformPoint(3, 0, 0);
		Vector3 projectilePosition = transform.TransformPoint(-11.15F, -3.45F, -0.2F);

		Vector3 raycastOrigin = cam.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0));
		Vector3 forward = transform.TransformDirection(Vector3.left) * 10;

		if(locked == false){
			if(Input.GetKey(KeyCode.W)){
				rb.AddRelativeForce(Vector3.left * flySpeed * Time.deltaTime * 10);
				cam.transform.position = Vector3.SmoothDamp(cam.transform.position, position1, ref velocity, 0.3F);
			}else{
				cam.transform.position = Vector3.SmoothDamp(cam.transform.position, position2, ref velocity, 0.3F);
			}
			if(Input.GetKey(KeyCode.S)){
				rb.AddRelativeForce(Vector3.right * flySpeed * Time.deltaTime * 10);
			}
			if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)){
				thruster.SetActive(true);
			}else{
				thruster.SetActive(false);
			}

			float horizontal = Input.GetAxis("Mouse X");
			//float horizontal = Input.GetAxis("Horizontal");
			if(horizontal < 0){
				transform.Rotate(Vector3.down * -horizontal * Time.deltaTime * rotateSpeed, Space.World);
			}
			if(horizontal > 0){
				transform.Rotate(Vector3.up * horizontal * Time.deltaTime * rotateSpeed, Space.World);
			}
			float vertical = Input.GetAxis("Mouse Y");
			//float vertical = Input.GetAxis("Vertical");

			if(vertical > 0){
				transform.Rotate(Vector3.back * vertical * Time.deltaTime * rotateSpeed);
			}
			if(vertical < 0){
				transform.Rotate(Vector3.forward * -vertical * Time.deltaTime * rotateSpeed);
			}
			rocketRotation.eulerAngles = new Vector3(horizontal * 15, 0, vertical * -5);

			if(Input.GetKeyDown(KeyCode.Escape)){
				cursorLockState = !cursorLockState;
				if(cursorLockState == true){
					Cursor.lockState = CursorLockMode.Locked;
					UnityEngine.Cursor.visible = false;
				}else{
					Cursor.lockState = CursorLockMode.None;
					UnityEngine.Cursor.visible = true;
				}
			}

			cooldown -= Time.deltaTime;
			//Firing
			GameObject clone;
			RaycastHit hit;
			if(Input.GetKeyDown(KeyCode.Space)){
				if(cooldown <= 0){
					clone = Instantiate(projectile, projectilePosition, transform.rotation) as GameObject;
					clone.transform.parent = projectileParent.transform;
					cooldown = 0.5F;
					if(Physics.Raycast(raycastOrigin, forward, out hit)){
						if(hit.transform.tag == "CanBeShot"){
							StartCoroutine(addScore(hit));
						}
					}
				}
			}
		}

	}
	IEnumerator addScore(RaycastHit hit2){
		yield return new WaitForSeconds(0.25F);
		hit2.transform.SendMessage("Hit");
	}
	/*public void Turbo(){
		turbo = !turbo;
		if(turbo == true){
			turboImage.GetComponent<Image>().color = Color.yellow;
			flySpeed = 1000; 
		}else{
			turboImage.GetComponent<Image>().color = Color.green;
			flySpeed = 100;
		}
	}
	private int planet;
	void OnTriggerEnter(Collider other){
		switch(other.tag){
			case "Planet1": stop("Pita (1)", 1); planet = 1; break;
			case "Planet2": stop("Tortilla (2)", 2); planet = 2; break;
			case "Planet3": stop("White Bread (3)", 3); planet = 3; break;
			case "Planet4": stop("Sourdough (4)", 4); planet = 4; break;
			case "Planet5": stop("Rye (5)", 5); planet = 5; break;
		}
	}
	void stop(string planetText, int planetNumber){
		locked = true;
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		animator.SetTrigger("Cinematic_Drive");
		header.text = "you are now entering planet "+planetText;
	}
	void switchPlanet(){
		Application.LoadLevel(planet);
	}*/
}
 