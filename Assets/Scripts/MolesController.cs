using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MolesController : MonoBehaviour {
	public GameObject[] allMoles = new GameObject[0];
	public GameObject whackParticlePrefab;
	public GameObject[] fireWorks = new GameObject[0];
	public Text comboText;
	
	private float[] _allCounters = new float[9];
	private float[] _allMaxY = new float[9];
	private float[] _allMinY = new float[9];
	private bool[] _allActives = new bool[9];
	private bool[] _isDown = new bool[9];
	private bool[] _justWhacked = new bool[9];
	
	private bool _molesStarted;
	private bool _isComboStarted;
	private float _comboCounter = 0;
	private float _minSpawnSpeed;
	private float _slowestSpawnSpeed = 5;
	private float _totalMolesActive = 0;
	private float _moleSpeed = 5;
	private float _moleDownSpeed = 5;
	private float _maxMoles;
	private int _minStayInSeconds;
	private int _maxStayInSeconds;
	void Start()
	{
		ClearComboText();
		_molesStarted = false;
		Invoke ("StartMoles", 3f);
		for (int i = allMoles.Length; i --> 0;)
		{
			_allMaxY[i] = allMoles[i].transform.position.y + 1.5f;
			if(i <= 2)
			{
				_allMaxY[i] -= 0.5f;
			}
			_allMinY[i] = allMoles[i].transform.position.y;
			_isDown[i] = true;
			allMoles[i].particleSystem.enableEmission = false;
			allMoles[i].SetActive(false);
		}
		float difficulty = PlayerPrefs.GetInt("difficulty");
		if(difficulty == 0)
		{
			_minSpawnSpeed = 1f;
			_moleSpeed = 5f;
			_moleDownSpeed = 2.5f;
			_maxMoles = 3;
			_minStayInSeconds = 4;
			_maxStayInSeconds = 5;
		} else if(difficulty == 1)
		{
			_minSpawnSpeed = 0.5f;
			_moleSpeed = 7.5f;
			_moleDownSpeed = 2.5f;
			_maxMoles = 4;
			_minStayInSeconds = 3;
			_maxStayInSeconds = 4;
		} else {
			_minSpawnSpeed = 0.1f;
			_moleSpeed = 10f;
			_moleDownSpeed = 2.5f;
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
	/*
	int getIndexOf(Sprite[] spriteArray, Sprite currentSprite)
	{
		for (int i = spriteArray.Length; i --> 0; )
		{
			if(spriteArray[i] == currentSprite)
			{
				return i;
				break;
			}
		}
		return 0;
	} */
	public void WhackMole(int mole)
	{
		if(_allActives[mole])
		{
			allMoles[mole].GetComponent<Animator>().SetTrigger("whack");
			_allActives[mole] = false;
			GetComponent<ScoreController>().AddScore(100 + (int)(_comboCounter*10));
			Instantiate(whackParticlePrefab,allMoles[mole].transform.position,whackParticlePrefab.transform.rotation);
			_justWhacked[mole] = true;
			if(_isComboStarted)
			{
				_comboCounter++;
				string comboString = _comboCounter.ToString();
				string newString = "";
				for(int i = 0; i < comboString.Length;i++)
				{
					if(comboString[i] == '0')
					{
						newString += 'o';
					}
					else
					{
						newString += comboString[i];
					}
				}
				comboText.text = "Combo: " + newString;
				int random = Random.Range(0,fireWorks.Length);
				Instantiate(fireWorks[random],fireWorks[random].transform.position,fireWorks[random].transform.rotation);
				Invoke ("ClearComboText", 2f);
			}
		} else {
			GetComponent<ScoreController>().AddScore(-50);
			GetComponent<HealthController>().AddHealth(-1);
			if(_isComboStarted)
			{
				_isComboStarted = false;
				_comboCounter = 0;
			}
		}
	}
	void ClearComboText()
	{
		comboText.text = "";
	}
	void Update()
	{
		if(_molesStarted)
		{
			CheckMolesMovement();
		}
		if(comboText.text != "")
		{
			Color newColor = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
			comboText.color = newColor;
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
		float randomAnim = Random.Range(0,3);
		allMoles[startMole].gameObject.SetActive(true);
		allMoles[startMole].GetComponent<Animator>().SetTrigger(""+randomAnim);

		_allActives[startMole] = true;
		_isDown[startMole] = false;
		_allCounters[startMole] = Random.Range(_minStayInSeconds,_maxStayInSeconds);

		if(_slowestSpawnSpeed > _minSpawnSpeed)
		{
			_slowestSpawnSpeed -= 0.1f;
		}
		if(_isComboStarted)
		{
			allMoles[startMole].particleSystem.enableEmission = true;
		}
		else 
		{
			if(Random.Range(0,100) >= 75)
			{
				_isComboStarted = true;
			}
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
					allMoles[i].transform.position += movement * _moleDownSpeed * Time.deltaTime;
				} else if(!_isDown[i]){
					_isDown[i] = true;
					allMoles[i].gameObject.SetActive(false);
					if(!_justWhacked[i])
					{
						_isComboStarted = false;
						_comboCounter = 0;
						GetComponent<HealthController>().AddHealth(-1);
						GetComponent<ScoreController>().AddScore(-50);
					} else {
						_justWhacked[i] = false;
					}
				}
			}
		}
	}
}
