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
	public GameObject impact;
	public GameObject enemyBread;
	public int spawnY;
	public int spawnXZ;
	public GameObject Level;
	public float hitDamage = 100;
	private int currentEnemies;

	public float totalHits = 50;
	private float damage;
	private float totalStep;
	private float health = 100;
	private float guiStepState;

	void Start(){
		bossHealth = bossHealth.GetComponent<RectTransform>();
		totalStep = bossHealth.anchoredPosition.x * 2 / totalHits;
		guiStepState = bossHealth.anchoredPosition.x;
		damage = health / totalHits;
		Invoke("SpawnEnemy", 5);

	}
	void Update (){
		if(!isDying){
			if(playerObject){
				transform.position = Vector3.MoveTowards(transform.position, playerObject.transform.position, Time.deltaTime * speed);
			}
		}
	}

	void OnTriggerEnter(Collider collision){
		if(collision.transform.tag != "CanBeShot" && collision.transform.tag == "Player"){
			player.SendMessage("changeHealth", hitDamage, SendMessageOptions.DontRequireReceiver);
		}

	}
	void OnCollisionEnter(Collision other){
		if(other.transform.tag != "CanBeShot" && other.transform.tag != "Player" && isDying){
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
	void SpawnEnemy(){
		if(!isDying){
			int spawns = Random.Range(0, 3);
			for(int i = 0; i <= spawns; i++){
				int x = Random.Range(0, spawnXZ);
				int z = Random.Range(0, spawnXZ);
				int abs = Random.Range(0, 2);
				if(abs == 1){
					x = -x;
					z = -z;
				}
				if(currentEnemies < 10){
					Vector3 spawnPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + spawnY, transform.localPosition.z + z);

					GameObject enemyBreadClone = Instantiate(enemyBread, spawnPosition, Quaternion.identity) as GameObject;
					enemyBreadClone.SetActive(true);
					GameObject impactClone = Instantiate(impact, spawnPosition, Quaternion.identity) as GameObject;
					GameObject.Destroy(impactClone, 1);
					currentEnemies += 1;
				}
			}

			int invokeTime = Random.Range(5, 10);
			Invoke("SpawnEnemy", invokeTime);
		}
	}
	RaycastHit pedestalPos;
	void DestroyBoss(){
		isDying = true;
		fire.SetActive(true);
		gameObject.GetComponent<Rigidbody>().isKinematic = false;
		gameObject.GetComponent<Rigidbody>().useGravity = true;

		RaycastHit pedestalPos;
	//	Vector3 down = transform.TransformDirection(Vector3.back) * 1000;
		if(Physics.Raycast(transform.position, Vector3.down, out pedestalPos)){
			
		}

		Level levelScript = Level.GetComponent<Level>();
		levelScript.SetupPedestal(pedestalPos.point);
	}
	void finish(){
		Level levelScript = Level.GetComponent<Level>();
		levelScript.SpawnPedestal();

		Object.Destroy(gameObject);
	}
	void DestroyedEnemy(){
		currentEnemies -= 1;
	}
}