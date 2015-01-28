using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	public GameObject menuCanvas;
	public GameObject creditsCanvas;
	public GameObject difficultyCanvas;

	public void StartButton () {
		menuCanvas.SetActive(false);
		difficultyCanvas.SetActive(true);
	}
	public void StartGame(int difficulty)
	{
		PlayerPrefs.SetInt("difficulty", difficulty);
		Application.LoadLevel(1);
	}
	public void ShowCredits () {
		menuCanvas.SetActive(false);
		creditsCanvas.SetActive(true);
	}
	public void ShowMenu()
	{
		menuCanvas.SetActive(true);
		creditsCanvas.SetActive(false);
	}
}
