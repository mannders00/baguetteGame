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

	public void Start(){
		Time.timeScale = 1;
		progressText.text = "You have destroyed "+PlayerPrefs.GetInt("Progress").ToString()+" planets";
		sensitivitySlider.normalizedValue = PlayerPrefs.GetFloat("Sensitivity");
	}
	public void menu2open(){
		menu2.SetActive(true);
		startButton.SetActive(false);
		menu3.SetActive(false);
		settingsMenu.SetActive(false);
	}
	public void menu1open(){
		menu2.SetActive(false);
		startButton.SetActive(true);
		menu3.SetActive(false);
		settingsMenu.SetActive(false);
	}
	public void menu3open(){
		menu3.SetActive(true);
		menu2.SetActive(false);
		settingsMenu.SetActive(false);

	}
	public void Settings(){
		settingsStatus = !settingsStatus;
		if(settingsStatus){
			Time.timeScale = 0;
			settingsMenu.SetActive(true);
		}else{
			Time.timeScale = 1;
			settingsMenu.SetActive(false);
		}
	}
	public void startAnim(){
		uiAnimator.SetTrigger("Start");
		menu3.SetActive(false);
		menu2.SetActive(false);
	}
	public void setSensitivity(float sens){
		PlayerPrefs.SetFloat("Sensitivity", sens);
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
