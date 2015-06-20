﻿using UnityEngine;
using System.Collections;

public class EnemyBread : MonoBehaviour {

	public GameObject player;
	public int speed;

	void Update () {
		transform.LookAt(player.transform, Vector3.up);
		transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
	}
	void Hit(){
		GameObject.Destroy(gameObject);
	}
}