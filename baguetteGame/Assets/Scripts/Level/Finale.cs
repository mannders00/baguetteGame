using UnityEngine;
using System.Collections;

public class Finale : MonoBehaviour {

	public Animator canvasAnimator;
	public Animator playerAnimator;
	

	public void menu(){
		Application.LoadLevel(0);
	}
	public void canvasStart(){
		canvasAnimator.SetTrigger("YouWin");
	}
}
