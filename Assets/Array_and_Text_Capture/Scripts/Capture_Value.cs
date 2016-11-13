//Fall2016
//LMSC-281
//concepts for capturing values to an array
//modified Unity 2D Ufo Tutorial project
//jcowen

using UnityEngine;
using System.Collections;

public class Capture_Value : MonoBehaviour {

	//creating an array to hold a user defined number of values
//	public int numOfValues = 100;
	//we need to declare the array but not initialize it in order to have it updated at runtime
	public int[] allScores = new int[100];

	//to have a declaration of an array with a fixed number of values
//	private int[] allScores = new int[100];

	private int arrayPosition = 0;
	public int currentNumber = 1;


	// Use this for initialization
	void Start () {
//		Debug.Log ("From CaptureValues script, the array size is " + allScores.Length);
		//when the object is created, we allow for the number of values in the array to be initialized
//		allScores = new int[numOfValues];

		//for testing
//		for (int i = 0; i<allScores.Length; i++) {
//			CaptureToArray();
//			currentNumber++;
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CaptureToArray (){
		//so that we do not try to write to an index that doesn't exist in the array, we check to make sure we are within the bounds
		if (arrayPosition < allScores.Length) {
			//assign the current number to be stored in the array
			//this number will be updated from another script when the player collides with an enemy object
			allScores[arrayPosition] = currentNumber;
			//checking our logic
			Debug.Log ("The current array position is " + arrayPosition + " with a value of " + allScores[arrayPosition]);
			//get ready to capture the next number
			arrayPosition++;
		}
		if (arrayPosition == allScores.Length) {
//			Debug.Log ("From CaptureValues script, the array size is " + allScores.Length);
			//once we've captured as many values as we can hold, stop the autorun process
			GetComponent<PlayerController_w_Array_Capture>().autoRun = false;
		}
	}
}
