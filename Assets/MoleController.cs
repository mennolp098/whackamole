using UnityEngine;
using System.Collections;

public class MoleController : MonoBehaviour {
	private float _speed;
	private float _maxY;
	private bool _isActive;

	public float id;
	// Use this for initialization
	void Start () {
		_isActive = false;
		Invoke ("RandomStart", Random.Range(0.5f,10f));
	}
	void RandomStart()
	{
		StartCoroutine("StartMoving");
	}
	IEnumerator StartMoving()
	{
		_isActive = true;
		while(_isActive)
		{
			if(this.transform.position.y < _maxY)
			{
				Vector2 movement = new Vector2(0,1);
				this.transform.position = movement * _speed;
			}
			yield return new WaitForSeconds(0.25f);
		}
	}
}
