using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

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
	public GameObject rocket;
	public float sensitivity = 0.33F;

	private Vector3 velocity = Vector3.zero;
	private bool turbo;
	private bool locked;
	private int planetInt;
	private bool cursorLockState = true;
	private int health = 100;
	public GameObject explosion;
	public Text healthText;

	private Quaternion rocketRotation = Quaternion.identity;

	private bool goingForward = false;
	private bool goingBackward = false;

	void Start(){
	//	cooldown = 0;
		turbo = false;
	//	turboImage.GetComponent<Image>().color = Color.green;
		locked = false;
		Cursor.lockState = CursorLockMode.Locked;
		UnityEngine.Cursor.visible = false;
		InvokeRepeating("add1", 0, 1);
		healthText.text = "H " + health.ToString();
	}
	void Update(){

		float x1;
		if(turbo == true){
			x1 = 50;
		}else{
			x1 = 10;
		}
		
		//rocket.transform.localEulerAngles = Vector3.Lerp(rocket.transform.localEulerAngles, new Vector3(45, 0, 0), Time.deltaTime * 5);

		Vector3 position1 = transform.TransformPoint(x1, 0, 0);
		Vector3 position2 = transform.TransformPoint(3, 0, 0);
		Vector3 projectilePosition = transform.TransformPoint(-11.15F, -3.45F, -0.2F);

		Vector3 raycastOrigin = cam.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0));
		Vector3 forward = transform.TransformDirection(Vector3.left) * 10;

		if(locked == false){
			rocket.transform.localRotation = Quaternion.Lerp(rocket.transform.localRotation, rocketRotation, Time.deltaTime * 3);

			if(health <= 0){
				Explode();
				rocket.SendMessage("Explode", SendMessageOptions.DontRequireReceiver);
			}

			if(Input.GetKey(KeyCode.W) || goingForward == true){
				rb.AddRelativeForce(Vector3.left * flySpeed * Time.deltaTime * 10);
				cam.transform.position = Vector3.SmoothDamp(cam.transform.position, position1, ref velocity, 0.3F);
			}else{
				cam.transform.position = Vector3.SmoothDamp(cam.transform.position, position2, ref velocity, 0.3F);
			}
			if(Input.GetKey(KeyCode.S) || goingBackward == true){
				rb.AddRelativeForce(Vector3.right * flySpeed * Time.deltaTime * 10);
			}
			if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || goingForward == true || goingBackward == true){
				thruster.SetActive(true);
			}else{
				thruster.SetActive(false);
			}

			float z = transform.localEulerAngles.z;
			/*z = Mathf.Clamp((z <= 180) ? z : -(360 - z), -90, 90);
			Quaternion clamp = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
			transform.rotation = clamp;*/
			
	//		float horizontal = Input.GetAxis("Mouse X");
			float horizontal = CrossPlatformInputManager.GetAxis("Horizontal") * sensitivity;
			if(horizontal < 0){
				if(z > 270 && z < 360 || z < 90){
						transform.Rotate(Vector3.down * -horizontal * Time.deltaTime * rotateSpeed, Space.World);
					}else{
						transform.Rotate(Vector3.down * horizontal * Time.deltaTime * rotateSpeed, Space.World);
					}
			}
			if(horizontal > 0){
				if(z > 270 && z < 360 || z < 90){
						transform.Rotate(Vector3.down * -horizontal * Time.deltaTime * rotateSpeed, Space.World);
					}else{
						transform.Rotate(Vector3.down * horizontal * Time.deltaTime * rotateSpeed, Space.World);
					}
			}
	//		float vertical = Input.GetAxis("Mouse Y");
			float vertical = CrossPlatformInputManager.GetAxis("Vertical") * sensitivity;

			if(vertical > 0){
				transform.Rotate(Vector3.back * vertical * Time.deltaTime * rotateSpeed);
			}else{
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
	public void shoot(){
		GameObject clone;
		RaycastHit hit;

		Vector3 raycastOrigin = cam.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0));
		Vector3 forward = transform.TransformDirection(Vector3.left) * 10;
		Vector3 projectilePosition = transform.TransformPoint(-11.15F, -3.45F, -0.2F);

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
	public void forwardTrue(){
		goingForward = true;
	}
	public void forwardFalse(){
		goingForward = false;
	}
	public void backwardTrue(){
		goingBackward = true;
	}
	public void backwardFalse(){
		goingBackward = false;
	}
	IEnumerator addScore(RaycastHit hit2){
		yield return new WaitForSeconds(0.25F);
		hit2.transform.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
	}
	public void Explode(){
		locked = true;
		rb.velocity =  Vector3.zero;
		healthText.text = "H 0";
	}
	public void changeHealth(int healthInt){
		if(health > 0){
			health = health - healthInt;
			healthText.text = "H " + health.ToString();
		}
	}
	public void add1(){
		if(health < 100 && health > 0){
			health += 1;
			healthText.text = "H " + health.ToString();
		}
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
