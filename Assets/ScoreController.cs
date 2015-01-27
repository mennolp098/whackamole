using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	public int scoreValue;
	public int score;
	public Text scoreText;
	
	private ScoreController scoreController;
	
	void Start ()
	{
		score = 0;
		UpdateScore ();
	}
	
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
}