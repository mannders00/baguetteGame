using UnityEngine;
using System.Collections;


public class Pan : MonoBehaviour
{
	private Vector2 _scrollPosition; // for the scroll view
	public AnimationCurve touchPadInputCurve = AnimationCurve.Linear( 0.0f, 0.0f, 1.0f, 1.0f );
	public GameObject player;

	public void Start(){
//		Character script = player.GetComponent<Character>();
		var recognizer = new TKPanRecognizer();

		if( Application.platform == RuntimePlatform.IPhonePlayer )
			recognizer.minimumNumberOfTouches = 2;

		recognizer.gestureRecognizedEvent += ( r ) =>
		{
//			script.horizontal = recognizer.deltaTranslation.x;
//			script.vertical = recognizer.deltaTranslation.y;

		//	Debug.Log( "pan recognizer fired: " + r );
		};

		recognizer.gestureCompleteEvent += r =>
		{
		//	Debug.Log( "pan gesture complete" );
		};
		TouchKit.addGestureRecognizer( recognizer );
	}
}
