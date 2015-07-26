using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public GameObject startButton;
	public GameObject settingsButton;
	public GameObject settingsMenu;
	public GameObject menu2;
	public GameObject menu3;

	public Animator uiAnimator;

	public Text progressText;

	private bool settingsStatus;

	public Slider sensitivitySlider;
	public Slider volumeSlider;

	public AudioSource audioSource;
	public AudioClip select;

	public void Start(){
		Time.timeScale = 1;
		progressText.text = "You have destroyed "+PlayerPrefs.GetInt("Progress").ToString()+" planets";
		sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
		volumeSlider.value = PlayerPrefs.GetFloat("Volume");
		AudioListener.volume = PlayerPrefs.GetFloat("Volume");

	}
	public void menu2open(){
		menu2.SetActive(true);
		startButton.SetActive(false);
		menu3.SetActive(false);
		settingsMenu.SetActive(false);
		audioSource.PlayOneShot(select);
	}
	public void menu1open(){
		menu2.SetActive(false);
		startButton.SetActive(true);
		menu3.SetActive(false);
		settingsMenu.SetActive(false);
		audioSource.PlayOneShot(select);
	}
	public void menu3open(){
		menu3.SetActive(true);
		menu2.SetActive(false);
		settingsMenu.SetActive(false);
		audioSource.PlayOneShot(select);

	}
	public void Settings(){
		settingsStatus = !settingsStatus;
		if(settingsStatus){
			Time.timeScale = 0;
			settingsMenu.SetActive(true);
			audioSource.PlayOneShot(select);
		}else{
			Time.timeScale = 1;
			settingsMenu.SetActive(false);
			audioSource.PlayOneShot(select);
		}
	}
	public void startAnim(){
		uiAnimator.SetTrigger("Start");
		menu3.SetActive(false);
		menu2.SetActive(false);
		audioSource.PlayOneShot(select);
	}
	public void setSensitivity(float sens){
		PlayerPrefs.SetFloat("Sensitivity", sens);
	}
	public void setVolume(float vol){
		PlayerPrefs.SetFloat("Volume", vol);
		AudioListener.volume = vol;
	}
	public void reset(){
		PlayerPrefs.DeleteAll();
		progressText.text = "You have destroyed "+PlayerPrefs.GetInt("Progress").ToString()+" planets";
	}
	public void loadlLevel(){
		uiAnimator.SetTrigger("Reset");
		Application.LoadLevel(1);
	}
}
