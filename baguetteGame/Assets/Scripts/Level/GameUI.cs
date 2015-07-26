using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	public GameObject settingsMenu;

	private bool settingsStatus;

	public Slider sensitivitySlider;
	public Slider volumeSlider;

	public GameObject player;

	public AudioSource audioSource;
	public AudioClip click;
	
	public void Start(){
		sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
		volumeSlider.value = PlayerPrefs.GetFloat("Volume");
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
	public void setVolume(float vol){
		PlayerPrefs.SetFloat("Volume", vol);
		AudioListener.volume = vol;
	}
	public void menu(){
		Application.LoadLevel(0);
	}
}
