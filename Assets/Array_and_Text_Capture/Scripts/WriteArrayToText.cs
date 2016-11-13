/*LMSC-281
 * Fall 2016
 * example text write function
 * jcowen
 * */

using UnityEngine;
using System.Collections;

//add the library that contains File functionality
using System.IO;

public class WriteArrayToText : MonoBehaviour {

	//
	public string stringOfValues = "";
	public bool runWriteArray = false;

	//the array of values is currently a component on the player object so we will need to create a place to store a reference to that object
	GameObject playerObject;

	//identify the text/data file we will be using
	string fileToWriteTo;

	// Use this for initialization
	void Start () {

		//we need to locate the player object in order to work with its components
		playerObject = GameObject.Find("Player");

		//if we want to keep our text file inside the application directory we can use the following
		fileToWriteTo = Application.dataPath + "/Resources/Data.txt";

	}

	void Update () {
		if (runWriteArray) {
			WriteArray();
			runWriteArray = false;
		}
	}
		
	public void WriteArray () {
		//read in the values from the array one at a time, and append to a simple text string variable
		for (int i = 0; i<playerObject.GetComponent<Capture_Value>().allScores.Length; i++) {
			//using a tempString variable to make it easier to read
			string tempString = playerObject.GetComponent<Capture_Value>().allScores[i].ToString();
			stringOfValues = stringOfValues + tempString;
		}
		//need to append a final caraige return and new line in order to seperate each individual pass of data
		stringOfValues = stringOfValues + "\r\n";
		//If I want to save multiple runs of this data capture, I can write where the last text leaves off in the file
		//This is why you would use the Append function, this is persistent across multiple plays of the game
//		File.AppendAllText(fileToWriteTo, stringOfValues);
		//If I only want to save the data from the last time the application is run, I would use the WriteAllText function
		File.WriteAllText(fileToWriteTo, stringOfValues);
		Debug.Log (stringOfValues);
	}
}
