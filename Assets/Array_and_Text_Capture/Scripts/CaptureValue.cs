//Fall2016
//LMSC-281
//concepts for capturing values to an array
//modified Unity2D UFO tutorial
//jcowen

using UnityEngine;
using System.Collections;

public class CaptureValue : MonoBehaviour {

	//an array to hold all of our values
	public int[] allScores = new int[100];


	int arrayPosition = 0;
	//holds the value received from the game
	public int currentNumber = 1;

	// Use this for initialization
	void Start () {
		//testing our custom function
//		CaptureToArray();
		GetComponent<PlayerController_w_Array_Capture>().autoRun = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CaptureToArray () {
		//check to ensure the array position is valid
		if (arrayPosition < allScores.Length) {
			allScores [arrayPosition] = currentNumber;
			Debug.Log ("The current array position is " + arrayPosition + " with a value of " + allScores [arrayPosition]);
			arrayPosition++;
		}

		if (arrayPosition == allScores.Length) {
			GetComponent<PlayerController_w_Array_Capture>().autoRun = false;
		}

	}

}
