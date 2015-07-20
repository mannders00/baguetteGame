using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	public GameObject settingsMenu;

	private bool settingsStatus;

	public Slider sensitivitySlider;

	public GameObject player;
	
	public void Start(){
		sensitivitySlider.normalizedValue = PlayerPrefs.GetFloat("Sensitivity");
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
	public void setSensitivity(float sens){
		PlayerPrefs.SetFloat("Sensitivity", sens);
		player.SendMessage("ChangeSensitivity", sens);
	}
	public void menu(){
		Application.LoadLevel(0);
	}
}
