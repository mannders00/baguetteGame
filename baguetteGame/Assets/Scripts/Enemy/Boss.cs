using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	public GameObject player;
	public GameObject playerObject;
	public RectTransform bossHealth;
	public float speed;
	private bool isDying;
	public GameObject fire;
	public GameObject explosion;

	public float totalHits;
	private float damage;
	private float totalStep;
	private float health = 100;
	private float guiStepState;

	void Start(){
		bossHealth = bossHealth.GetComponent<RectTransform>();
		totalStep = bossHealth.anchoredPosition.x * 2 / totalHits;
		guiStepState = bossHealth.anchoredPosition.x;
		damage = health / totalHits;

	}
	void Update (){

		if(isDying == false){
			if(playerObject){
				transform.position = Vector3.MoveTowards(transform.position, playerObject.transform.position, Time.deltaTime * speed);
			}
		}else{
			transform.Translate(Vector3.down * Time.deltaTime * 20, Space.World);
		}
	}

	void OnTriggerEnter(Collider collision){
		if(collision.transform.tag != "CanBeShot" && collision.transform.tag == "Player"){
			player.SendMessage("changeHealth", 75, SendMessageOptions.DontRequireReceiver);
		}
		if(collision.transform.tag != "CanBeShot" && collision.transform.tag != "Player" && isDying){
			Instantiate(explosion, transform.position, Quaternion.identity);
				finish();
		}

	}
	public void Hit(){
		if(health > 0){
			health -= damage;
			guiStepState -= totalStep;
			bossHealth.anchoredPosition = new Vector2(guiStepState, bossHealth.anchoredPosition.y);
		}
		if(health <= 0){
			DestroyBoss();
		}
	}
	void DestroyBoss(){
		isDying = true;
		fire.SetActive(true);
	}
	void finish(){
		Object.Destroy(gameObject);
	}
}