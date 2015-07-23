using UnityEngine;
using System.Collections;

public class MenuAudio : MonoBehaviour {

	public AudioSource audioSource;

	public AudioClip select;

	void Start(){
		audioSource.PlayOneShot(select);
	}
}
