using UnityEngine.UI;
using UnityEngine;
using System;

public class DataController : MonoBehaviour
{
    [SerializeField] Text _soulsNumber;

    private EnemyGenerate _enemyGenerateScript;
    private int _currentSouls;
    public int _currentFloor;

    private int _lvlSouls;
    public int _soulsSum;

    const string floorKeyName = "recordFloor";
    const string soulsKeyName = "souls";

    public event Action EndLevel;

    void Start()
    {
        _currentSouls = 0;
        _currentFloor = 1;

        _lvlSouls = 0;
        _soulsSum = 0;

        _enemyGenerateScript = GetComponent<EnemyGenerate>();
        FlyingEye.Dead += CounterEnemyDead;
        UpdateSoulsNumber();
    }

    public void SaveData()
    {
        int soulsCount = PlayerPrefs.GetInt(soulsKeyName);
        int soulsSum = soulsCount + (_lvlSouls + _currentSouls);
        PlayerPrefs.SetInt(soulsKeyName, soulsSum);

        int saveFloorNumber = PlayerPrefs.GetInt(floorKeyName);
        if (_currentFloor > saveFloorNumber)
            PlayerPrefs.SetInt(floorKeyName, _currentFloor);
    }

    private void CounterEnemyDead()
    {
        _currentSouls++;
        UpdateSoulsNumber();
        if(_currentSouls == _enemyGenerateScript._instantiateEnemys.Count)
        {
            EndLevel();
            _lvlSouls += _currentSouls;
            _currentSouls = 0;
        }
    }

    private void UpdateSoulsNumber()
    {
        _soulsSum = _lvlSouls + _currentSouls;
        if(_soulsNumber != null)
            _soulsNumber.text = (_lvlSouls + _currentSouls).ToString();
    }
}
