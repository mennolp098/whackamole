using UnityEngine;
using System.Collections;

public class MolesController : MonoBehaviour {
	public GameObject[] allMoles = new GameObject[0];
	public GameObject whackParticlePrefab;
	public Sprite[] allSprites = new Sprite[0];

	private float[] _allCounters = new float[9];
	private float[] _allMaxY = new float[9];
	private float[] _allMinY = new float[9];
	private bool[] _allActives = new bool[9];
	private bool[] _isDown = new bool[9];

	private bool _molesStarted;
	private float _minSpawnSpeed;
	private float _slowestSpawnSpeed = 5;
	private float _totalMolesActive = 0;
	private float _moleSpeed = 5;
	private float _maxMoles;
	private int _minStayInSeconds;
	private int _maxStayInSeconds;
	void Start()
	{
		_molesStarted = false;
		Invoke ("StartMoles", 3f);
		for (int i = allMoles.Length; i --> 0;)
		{
			_allMaxY[i] = allMoles[i].transform.position.y + 1.5f;
			_allMinY[i] = allMoles[i].transform.position.y;
			_isDown[i] = true;
		}
		float difficulty = PlayerPrefs.GetInt("difficulty");
		if(difficulty == 0)
		{
			_minSpawnSpeed = 1f;
			_moleSpeed = 5f;
			_maxMoles = 3;
			_minStayInSeconds = 4;
			_maxStayInSeconds = 5;
		} else if(difficulty == 1)
		{
			_minSpawnSpeed = 0.5f;
			_moleSpeed = 7.5f;
			_maxMoles = 4;
			_minStayInSeconds = 3;
			_maxStayInSeconds = 4;
		} else {
			_minSpawnSpeed = 0.1f;
			_moleSpeed = 10f;
			_maxMoles = 5;
			_minStayInSeconds = 2;
			_maxStayInSeconds = 3;
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
			//TODO: whack animation
			Instantiate(whackParticlePrefab,allMoles[mole].transform.position,whackParticlePrefab.transform.rotation);
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
					if(_totalMolesActive == _maxMoles)
					{
						break;
					}
				}
			}
			if(_totalMolesActive < _maxMoles-1)
			{
				AddActiveMole();
			}
		}
		Invoke ("AddNewMole",Random.Range(0.25f,_slowestSpawnSpeed));
	}
	void AddActiveMole()
	{
		int startMole = Random.Range(0,9);
		while(_allActives[startMole] || !_isDown[startMole])
		{
			startMole = Random.Range(0,9);
		}
		Sprite newSprite = allSprites[Random.Range(0,allSprites.Length)];
		allMoles[startMole].GetComponent<SpriteRenderer>().sprite = newSprite;
		_allActives[startMole] = true;
		_isDown[startMole] = false;
		_allCounters[startMole] = Random.Range(_minStayInSeconds,_maxStayInSeconds);

		if(_slowestSpawnSpeed > _minSpawnSpeed)
		{
			_slowestSpawnSpeed -= 0.1f;
		}
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
