/*
 LMSC-281 Capturing values to an array
 Fall 2016 - Jeanine Cowen
 */

using UnityEngine;
using System.Collections;

//JC need to add the SceneManagement library to be able to restart the level
using UnityEngine.SceneManagement;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class PlayerController_w_Array_Capture : MonoBehaviour {

	//how fast the player moves
	public float speed;				//Floating point variable to store the player's movement speed.

	private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.

	//boolean to allow for a player restart
	bool restart = false;

	//boolean to allow for an autorun function
	public bool autoRun = true;

	//we also need to declare the moveHorizontal and moveVertical at the top of the script for autoRun capability
	float moveHorizontal = 0.0f;
	float moveVertical = 0.0f;

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();
	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{
		//allow for autorun function, or player control
		if (autoRun) { //an autoRun will control the player randomly
			moveHorizontal = Random.Range (-10.0f, 10.0f);
			moveVertical = Random.Range (-10.0f, 10.0f);
		}
		else { //allow the player to control 
			//Store the current horizontal input in the float moveHorizontal.
			moveHorizontal = Input.GetAxis ("Horizontal");

			//Store the current vertical input in the float moveVertical.
			moveVertical = Input.GetAxis ("Vertical");
		}

		//Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
		rb2d.AddForce (movement * speed);
	}
		
	//JC add enemy prefabs to capture some values
	void OnCollisionEnter2D (Collision2D other) {
		//if we hit an enemy object, we want to capture that object's value into our value array
		if (other.gameObject.CompareTag ("Enemy")) {
			//first, we access the public variable to update the currentNumber with the value on the enemy
			GetComponent<Capture_Value>().currentNumber = other.collider.GetComponent<EnemyValue>().value;
			//then we run the function/method in the other script(component) to push the value into the array
			GetComponent<Capture_Value>().CaptureToArray();
//			Debug.Log (GetComponent<Capture_Value>().currentNumber);
		}
	}

	//JC - we can use OnGUI to ask player to restart the game
	void OnGUI () {
		if (restart) {
			if (GUI.Button(new Rect(10, 70, 500, 30), "Restart?")) {
				restart = false;
				SceneManager.LoadScene ("Main_Ten_HighScores");
			}
		}
	}
}
