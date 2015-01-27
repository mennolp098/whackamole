using UnityEngine;
using System.Collections;

public class WhackController : MonoBehaviour 
{
	private MolesController _molesController;
	void Start()
	{
		_molesController = GetComponent<MolesController>();
	}
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			_molesController.WhackMole(0);
		}
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			_molesController.WhackMole(1);
		}
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			_molesController.WhackMole(2);
		}
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			_molesController.WhackMole(3);
		}
		if(Input.GetKeyDown(KeyCode.W))
		{
			_molesController.WhackMole(4);
		}
		if(Input.GetKeyDown(KeyCode.A))
		{
			_molesController.WhackMole(5);
		}
		if(Input.GetKeyDown(KeyCode.S))
		{
			_molesController.WhackMole(6);
		}
		if(Input.GetKeyDown(KeyCode.D))
		{
			_molesController.WhackMole(7);
		}
		if(Input.GetKeyDown(KeyCode.F))
		{
			_molesController.WhackMole(8);
		}

	}
}
