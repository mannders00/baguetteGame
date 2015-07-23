using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	public GameObject settingsMenu;

	private bool settingsStatus;

	public Slider sensitivitySlider;

	public GameObject player;

	public AudioSource audioSource;
	public AudioClip click;
	
	public void Start(){
		sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
	}
	public void Settings(){
		settingsStatus = !settingsStatus;
		if(settingsStatus){
			Time.timeScale = 0;
			settingsMenu.SetActive(true);
			audioSource.PlayOneShot(click);
		}else{
			Time.timeScale = 1;
			settingsMenu.SetActive(false);
			audioSource.PlayOneShot(click);
		}
	}
	public void setSensitivity(float sens){
		PlayerPrefs.SetFloat("Sensitivity", sens);
		player.SendMessage("ChangeSensitivity", sens);
	}
	public void menu(){
		Application.LoadLevel(0);
	}
}
