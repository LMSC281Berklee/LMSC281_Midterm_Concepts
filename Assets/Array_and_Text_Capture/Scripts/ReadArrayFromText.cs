/* LMSC-281
 * Fall 2016
 * example text read function
 * jcowen
 * */

using UnityEngine;
using System.Collections;

//include the file based library
using System.IO;

public class ReadArrayFromText : MonoBehaviour {

	//a variable to read in the entire line of values into a string
	public string allTextString;

	//boolean used to trigger reading in the string array from a text file
	public bool readText = false;

	//now that we are using the "100" number in two places we should consider holding a global variable to reference
	public int[] intArray = new int[100];
	
	// Update is called once per frame
	void Update () {
		//a boolean value allows us to control the reading of the text file
		if (readText) {
			ReadTextFromFile();
			//after we've read the file, we stop
			readText = false;
		}
	}

	public void ReadTextFromFile () {
		//read in all the characters from the text file
		allTextString = File.ReadAllText(Application.dataPath + "/Resources/Data.txt");
		Debug.Log(allTextString);
		//now we need to parse the text string out into the individual array position values
		for (int i = 0; i<100; i++) {
			string tempString = allTextString[i].ToString(); 	//we use a temporary string variable to hold the individual characters
			intArray[i] = System.Int32.Parse(tempString);		//then we convert the string to an integer, and store in an array
		}
	}
}
