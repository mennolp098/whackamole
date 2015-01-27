using UnityEngine;
using System.Collections;

public class MolesController : MonoBehaviour {
	public GameObject[] allMoles = new GameObject[0];

	private float[] _allCounters = new float[9];
	private float[] _allMaxY = new float[9];
	private float[] _allMinY = new float[9];
	private bool[] _allActives = new bool[9];
	private bool[] _isDown = new bool[9];

	private bool _molesStarted;
	private float _totalMolesActive = 0;
	private float _moleSpeed = 5;
	void Start()
	{
		_molesStarted = false;
		Invoke ("StartMoles", 3f);
		for (int i = allMoles.Length; i --> 0;)
		{
			_allMaxY[i] = allMoles[i].transform.position.y + 1.5f;
			_allMinY[i] = allMoles[i].transform.position.y;
		}
	}
	void StartMoles () 
	{
		_molesStarted = true;
		AddNewMole();
		StartCoroutine("CheckMolesActive");
	}
	public void WhackMole(int mole)
	{
		if(_allActives[mole])
		{
			_allActives[mole] = false;
			//TODO: score += 100;
		} else {
			//TODO: lose score
			//TODO: lose live
		}
	}
	void Update()
	{
		if(_molesStarted)
		{
			CheckMolesMovement();
		}
	}
	void AddNewMole()
	{
		if(_molesStarted)
		{
			_totalMolesActive = 0;
			foreach(bool active in _allActives)
			{
				if(active)
				{
					_totalMolesActive++;
					if(_totalMolesActive == 4)
					{
						break;
					}
				}
			}
			if(_totalMolesActive < 3)
			{
				AddActiveMole();
			}
		}
		Invoke ("AddNewMole",Random.Range(0.25f,1f));
	}
	void AddActiveMole()
	{
		int startMole = Random.Range(0,8);
		while(_allActives[startMole] && !_isDown[startMole])
		{
			startMole = Random.Range(0,8);
		}
		_allActives[startMole] = true;
		_isDown[startMole] = false;
		_allCounters[startMole] = Random.Range(2,3);
	}
	IEnumerator CheckMolesActive()
	{
		while(_molesStarted)
		{
			for (int i = _allActives.Length; i --> 0; )
			{
				if(_allActives[i])
				{
					_allCounters[i]--;
					if(_allCounters[i] == 0)
					{
						_allActives[i] = false;
					}
				}
			}
			yield return new WaitForSeconds(1);
		}
	}
	void CheckMolesMovement()
	{
		for (int i = allMoles.Length; i --> 0; )
		{
			if(_allActives[i])
			{
				if(allMoles[i].transform.position.y < _allMaxY[i])
				{
					Vector3 movement = new Vector3(0,1,0);
					allMoles[i].transform.position += movement * _moleSpeed * Time.deltaTime;
				}
			} else {
				if(allMoles[i].transform.position.y > _allMinY[i])
				{
					Vector3 movement = new Vector3(0,-1,0);
					allMoles[i].transform.position += movement * _moleSpeed * Time.deltaTime;
				} else {
					_isDown[i] = true;
				}
			}
		}
	}
}
