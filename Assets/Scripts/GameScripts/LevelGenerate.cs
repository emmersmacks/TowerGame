using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    [SerializeField] private GameObject _platformPref;
    [SerializeField] private int _rowNumber;
    [SerializeField] private int _columnNumber;

    private Dictionary<int, float[,]> _levelMap;
    private Dictionary<int, List<bool>> _affordablePosition;
    private List<GameObject> _instantiatedPref;

    private void Start()
    {
        _instantiatedPref = new List<GameObject>();
        GenerateNewLevel();
    }

    public void DeleteCreatedPrefab()
    {
        foreach(GameObject prefab in _instantiatedPref)
        {
            Destroy(prefab);
        }
        _instantiatedPref.Clear();
    }

    public void GenerateNewLevel()
    {
        CreateLevelMap();
        CreateAffordablePositions();
        _affordablePosition[4][Random.Range(0, 3)] = false;
        for (int countTier = _rowNumber; countTier > 0; countTier--)
        {
            CreateGrid(countTier);
        }
    }

    private void SpawnPlatforms(float xCoord, float yCoord)
    {
        GameObject prefab = Instantiate(_platformPref, new Vector3(xCoord, yCoord, -1f), Quaternion.identity);
        _instantiatedPref.Add(prefab);
    }

    public void CreateLevelMap()
    {
        _levelMap = new Dictionary<int, float[,]>()
        {
            {1, new float[,]{ { 2, 2.5f }, {5, 2.5f}, {8, 2.5f}}},
            {2, new float[,]{ { 2, 5f }, {5, 5f}, {8, 5f}}},
            {3, new float[,]{ { 2, 7.5f }, {5, 7.5f }, {8, 7.5f }}},
            {4, new float[,]{ { 2, 10f }, {5, 10f }, {8, 10f }}}
        };
    }
    private void CreateAffordablePositions()
    {
        _affordablePosition = new Dictionary<int, List<bool>>(_rowNumber);
        for (int keyCount = 1; keyCount < _rowNumber + 1; keyCount++)
        {
            var creatList = new List<bool>() { };
            for (int count = 0; count < _columnNumber; count++)
                creatList.Add(true);
            _affordablePosition.Add(keyCount, creatList);
        }
    }

    private void CreateGrid(int currentTier)
    {
        for (int countObj = 0; countObj < 3; countObj++)
        {
            if (_affordablePosition[currentTier][countObj])
            {
                SpawnPlatforms(_levelMap[currentTier][countObj, 0], _levelMap[currentTier][countObj, 1]);
                try
                {
                    _affordablePosition[currentTier - 1][countObj] = false;
                }
                catch
                {
                    break;
                }
            }
        }
    }


}
