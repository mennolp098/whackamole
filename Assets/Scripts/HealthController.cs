using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {
	
	public int healthValue;
	public int health;
	public Image healthBar;
	public Sprite[] allHealthBars = new Sprite[0];
	
	private HealthController healthController;
	
	void Start ()
	{
		health = 3;
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
		healthBar.sprite = allHealthBars[health];
	}
}