/*
 LMSC-281 Midterm examples built onto the 2D UFO Unity Tutorial project
 Fall 2017 - Jeanine Cowen
 */

using UnityEngine;
using System.Collections;

//JC need to add the SceneManagement library to be able to restart the level
using UnityEngine.SceneManagement;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class PlayerController_w_Array_HighScores : MonoBehaviour {

	//JC - existing float for midterm requirement
	public float speed;				//Floating point variable to store the player's movement speed.
	public Text countText;			//Store a reference to the UI Text component which will display the number of pickups collected.
	public Text winText;			//Store a reference to the UI Text component which will display the 'You win' message.

	private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.

	//JC - existing int for midterm requirement
	private int count;				//Integer to store the number of pickups collected so far.

	//JC - from the Unity_Tutorial_Roller_Ball playprefs player controller script
	//for the highscore leaderboard
	public int highScore = 0;
	string highScoreKey = "HighScore";

	//JC since we want to capture a highscores table, we will need an array to do that
	int[] highScores = new int[10];

	//JC need to have a string variable for the midterm requirements
	string lostGame = "Play Again?";

	//JC need to have a boolean variable for the midterm requirements
	bool restart = false;

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();

		//Initialize count to zero.
		count = 0;

		//Initialze winText to a blank string since we haven't won yet at beginning.
		winText.text = "";

		//Call our SetCountText function which will update the text with the current value for count.
		SetCountText ();

		//JC - from the Unity_Tutorial_Roller_Ball playprefs player controller script
		//Load the highScores array from player prefs if it is there, 0 otherwise.
		//use this for a leaderboard style where you save several highscores

		for (int i = 0; i<highScores.Length; i++){

			//Get the highScore from 1 - length of highScores array length
			highScoreKey = "HighScore"+(i+1).ToString();
			highScore = PlayerPrefs.GetInt(highScoreKey,0);
			Debug.Log (highScoreKey + " is " + highScore);
		}

		//JC quick reset of the highscore... for testing
//		for (int i = 0; i<highScores.Length; i++){
//			highScoreKey = "HighScore"+(i+1).ToString();
//			PlayerPrefs.SetInt(highScoreKey, 0);
//		}

	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{
		//Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis ("Horizontal");

		//Store the current vertical input in the float moveVertical.
		float moveVertical = Input.GetAxis ("Vertical");

		//Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
		rb2d.AddForce (movement * speed);
	}

	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerEnter2D(Collider2D other) 
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag ("PickUp")) 
		{
			//... then set the other object we just collided with to inactive.
			other.gameObject.SetActive(false);
			
			//Add one to the current value of our count variable.
			count = count + 1;
			
			//Update the currently displayed count by calling the SetCountText function.
			SetCountText ();
		}
	}

	//JC add enemy prefabs to make the highscores function make sense
	void OnCollisionEnter2D (Collision2D other) {
		//add in a losing condition so that the high scores table makes sense
		if (other.gameObject.CompareTag ("Enemy")) {
			LoseGame();
		}
	}

	//This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
	void SetCountText()
	{
		//Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
		countText.text = "Count: " + count.ToString ();

		//Check if we've collected all 12 pickups. If we have...
		if (count >= 12) {
			//... then set the text property of our winText object to "You win!"
			winText.text = "You win!";
			//JC and set the highscore value
			HighScoreProcess();
		}
	}

	//JC - This function handles the high scores and can be used when we win and when we lose
	void HighScoreProcess () {
		Debug.Log ("highscore here");

		//use this for a leaderboard style where you save several highscores
		for (int i = 0; i<(highScores.Length+1); i++){

			//Get the highScore from 1 - length of highScores array length
			highScoreKey = "HighScore"+(i+1).ToString();
			highScore = PlayerPrefs.GetInt(highScoreKey,0);

			//if score is greater, store previous highScore Set a new highScore
			//set score to previous highScore, and try again
			//Once score is greater, it will always be for the
			//remaining list, so the top will always be 
			//updated
			if(count>highScore){
				int temp = highScore;
				PlayerPrefs.SetInt(highScoreKey,count);
				count = temp;
			}
		}
	}

	//JC - This function will allow the player to restart the game
	void LoseGame () {
		Debug.Log ("Lost the game");
		HighScoreProcess();
		restart = true;
	}

	//JC - we can use OnGUI to ask player to restart the game
	void OnGUI () {
		if (restart) {
			if (GUI.Button(new Rect(10, 70, 500, 30), lostGame)) {
				restart = false;
				SceneManager.LoadScene ("Main_Ten_HighScores");
			}
		}
	}
}
