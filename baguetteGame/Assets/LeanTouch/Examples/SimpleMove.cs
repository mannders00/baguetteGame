using UnityEngine;

// This script will move the GameObject based on finger gestures
public class SimpleMove : MonoBehaviour
{	
    void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 150, 100), "I am a button")){}}
	protected virtual void LateUpdate()
	{
		// This will move the current transform based on a finger drag gesture
		Lean.LeanTouch.MoveObject(transform, Lean.LeanTouch.DragDelta);
	}
}