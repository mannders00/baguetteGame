using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Character : MonoBehaviour {
	
	public float rotateSpeed = 50;
	public float flySpeed = 200;

	public Camera cam;
	public Rigidbody rb;
	public bool flying;
	public GameObject thruster;
	public Animator animator;
	public GameObject turboImage;
	public Text header;
	public GameObject rocket;
	public float sensitivity = 0.33F;

	private Vector3 velocity = Vector3.zero;
	private bool turbo;
	private bool locked;
	private int planetInt;
	private bool cursorLockState = true;

	private Quaternion rocketRotation = Quaternion.identity;

	private bool goingForward = false;
	private bool goingBackward = false;
	public GameObject level;
	private bool thrusterBool;

	public Slider slider;

	private Vector2 touchStart;
	private Vector2 direction;

	public AudioSource audioSource;

	public AudioClip thrusterClip;

	bool isPlaying;

	void Start(){
		turbo = false;
		turboImage.GetComponent<Image>().color = Color.green;
		locked = false;
		Cursor.lockState = CursorLockMode.Locked;
		UnityEngine.Cursor.visible = false;
		sensitivity = PlayerPrefs.GetFloat("Sensitivity");
	}
	public void ChangeSensitivity(float sens){
		sensitivity = sens;
	}
	void Update(){

		float x1;
		if(turbo == true){
			x1 = 58;
		}else{
			x1 = 18;
		}
		
		//rocket.transform.localEulerAngles = Vector3.Lerp(rocket.transform.localEulerAngles, new Vector3(45, 0, 0), Time.deltaTime * 5);

		//Forward Position
		Vector3 position1 = transform.TransformPoint(x1, 0, 0);

		Vector3 position2 = transform.TransformPoint(13, 0, 0);

		if(locked == false){
			rocket.transform.localRotation = Quaternion.Lerp(rocket.transform.localRotation, rocketRotation, Time.deltaTime * 3);

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
				thrusterBool = true;
				if(isPlaying == false){
					audioSource.Play();
					isPlaying = true;
				}
			}else{
				isPlaying = false;
				thrusterBool = false;
				audioSource.Pause();
			}
			if(thrusterBool == true){
				thruster.SetActive(true);
				}else{
				thruster.SetActive(false);
			}


			for(int i=0; i < Input.touchCount; i++){
				Touch touch = Input.GetTouch(i);
				if(touch.position.x > Screen.width / 2 && touch.phase == TouchPhase.Moved){
					direction = touch.position - touchStart;
					direction = new Vector2(Mathf.Clamp(direction.x, -100, 100), Mathf.Clamp(direction.y, -100, 100));

				}else{
					direction = new Vector2(0, 0);
				}
				touchStart = touch.position;
			}

			float z = transform.localEulerAngles.z;
			/*z = Mathf.Clamp((z <= 180) ? z : -(360 - z), -90, 90);
			Quaternion clamp = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
			transform.rotation = clamp;*/
			
			float horizontal = Input.GetAxis("Mouse X") * sensitivity;
	//		float horizontal = CrossPlatformInputManager.GetAxis("Horizontal") * sensitivity;	
	//		float horizontal = direction.x * sensitivity;

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
			float vertical = Input.GetAxis("Mouse Y") * sensitivity;
	//		float vertical = CrossPlatformInputManager.GetAxis("Vertical") * sensitivity;
	//		float vertical = direction.y * sensitivity;

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
	public void Turbo(){
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
			case "Planet1": stop("Pita", 1); planet = 2; break;
			case "Planet2": stop("Tortilla", 2); planet = 3; break;
			case "Planet3": stop("White Brea", 3); planet = 4; break;
			case "Planet4": stop("Sourdough", 4); planet = 5; break;
			case "Planet5": stop("Rye", 5); planet = 6; break;
		}
	}
	public void PlanetDestruction(){
		PlanetDestruction script = level.GetComponent<PlanetDestruction>();
		script.Destruction();
	}
	public void stopMoving(){
		PlanetDestruction script = level.GetComponent<PlanetDestruction>();
		script.stopMoving();		
	}
	public void Lock(int lockState){
		if(lockState == 1){
			locked = true;
			thrusterBool = true;
		}else{
			locked = false;
			thrusterBool = false;
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
	}
}
