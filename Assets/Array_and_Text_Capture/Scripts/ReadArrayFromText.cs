//LMSC-281
//Fall 2016
//reading integer data back into an array
//jcowen

using UnityEngine;
using System.Collections;

//library yo include file operations
using System.IO;

public class ReadArrayFromText : MonoBehaviour {

	//a variable to hold the string data coming from the text file
	string allTextString;

	//boolean used to trigger the read text function
	public bool readText = false;

	//may want to store the "100" value somewhere to be referenced
	public int[] intArray = new int[100];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (readText) {
			ReadTextFromFile ();
			readText = false;
		}
	}

	public void ReadTextFromFile () {
		//assign all of the text into our string
		allTextString = File.ReadAllText (Application.dataPath + "/Resources/Data.txt");
		Debug.Log (allTextString);

		//cycle through the individual values in the larger string to get the individual integer values
		for (int i = 0; i < 100; i++) {

			//temp variable to hold individual values as a string
			string tempString = allTextString[i].ToString();

			intArray [i] = System.Int32.Parse (tempString);
			//Debug.Log (tempString);

		}

	}
}
