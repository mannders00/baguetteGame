using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour {

	public GameObject settingsMenu;

	private bool settingsStatus;
	
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
	}
	public void menu(){
		Application.LoadLevel(0);
	}
}
