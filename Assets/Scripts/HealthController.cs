using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {
	
	public int healthValue;
	public int health;
	public Text healthText;
	
	//private HealthController healthController;
	
	void Start ()
	{
		health = 10;
		UpdateHealth ();
	}
	
	
	public void AddHealth (int newHealthValue)
	{
		health += newHealthValue;
		UpdateHealth ();
		if(health <= 0)
		{
			Application.LoadLevel(0);
		}
	}
	
	void UpdateHealth ()
	{
		healthText.text = "Health: " + health;
	}
	
}