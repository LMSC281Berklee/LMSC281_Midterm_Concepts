/*
 LMSC-281 Midterm examples built onto the 2D UFO Unity Tutorial project
 Fall 2016 - Jeanine Cowen
 */


using UnityEngine;
using System.Collections;

//JC need to add the SceneManagement library to be able to restart the level
using UnityEngine.SceneManagement;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class PlayerController_w_Array_Capture : MonoBehaviour {

	//JC - existing float for midterm requirement
	public float speed;				//Floating point variable to store the player's movement speed.

	private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.

	float moveHorizontal = 0.0f;
	float moveVertical = 0.0f;

	//JC need to have a boolean variable for the midterm requirements
	bool restart = false;

	//to allow for autorun functionality
	public bool autoRun = false;

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();
}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{
		if (autoRun) {
			//move player with ai
			moveHorizontal = Random.Range (-10.0f, 10.0f);
			moveVertical = Random.Range (-10.0f, 10.0f);

		} else {

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

	//JC add enemy prefabs to make the highscores function make sense
	void OnCollisionEnter2D (Collision2D other) {
		//add in a losing condition so that the high scores table makes sense
		if (other.gameObject.CompareTag ("Enemy")) {
			GetComponent<CaptureValue>().currentNumber = other.collider.GetComponent<EnemyValue> ().value;
			GetComponent<CaptureValue> ().CaptureToArray ();
		}
	}


	//JC - we can use OnGUI to ask player to restart the game
	void OnGUI () {
		if (restart) {
			if (GUI.Button(new Rect(10, 70, 500, 30), "Restart")) {
				restart = false;
				SceneManager.LoadScene ("Main_Single_HighScore_and_Username");
			}
		}
	}
}
