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
		if(score < 0)
		{
			score = 0;
		}
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		string scoreString = score.ToString();
		string newString = "";
		for(int i = 0; i < scoreString.Length;i++)
		{
			if(scoreString[i] == '0')
			{
				newString += 'o';
			}
			else
			{
				newString += scoreString[i];
			}
		}
		scoreText.text = "Score: " + newString;
	}
}