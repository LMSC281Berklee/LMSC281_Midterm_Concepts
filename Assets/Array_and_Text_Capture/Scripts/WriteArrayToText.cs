//Fall 2016
//LMSC-281
//jcowen
//file write class

using UnityEngine;
using System.Collections;

//include library for file functions
using System.IO;

public class WriteArrayToText : MonoBehaviour {

	//will be used to indentify the file we will be writing to at runtime
	string fileToWriteTo;

	public string stringOfValues;

	public bool runWriteArray = false;

	GameObject playerObject;


	// Use this for initialization
	void Start () {

		//we want to write out tp the same folder where this project is located, specifically into a folder called "reourcses"
		fileToWriteTo = Application.dataPath + "/Resources/Data.txt";

		playerObject = GameObject.Find("Player");
	
	}
	
	// Update is called once per frame
	void Update () {
		if (runWriteArray) {
			WriteArray ();
			runWriteArray = false;
		}
	}

	public void WriteArray () {
		for (int i = 0; i < playerObject.GetComponent<CaptureValue>().allScores.Length; i++) {
			int intArrayValue = playerObject.GetComponent<CaptureValue>().allScores[i];
			stringOfValues = stringOfValues + intArrayValue.ToString ();
		}

		stringOfValues = stringOfValues + "\r\n";

		//Using the append function to write the data out to a text file
//		File.AppendAllText (fileToWriteTo, stringOfValues);

		//overwrite the informtaion in the text file
		File.WriteAllText (fileToWriteTo, stringOfValues);

	}

}
