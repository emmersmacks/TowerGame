using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPref;
    private List<float> _enemySpawnPosition;
    public List<GameObject> _instantiateEnemys;

    private void Start()
    {
        _instantiateEnemys = new List<GameObject>();
        GenerateEnemyPosition();
    }

    public void DeleteCreatedEnemy()
    {
        _instantiateEnemys.Clear();
    }

    private void CreateEnemyPosition()
    {
        _enemySpawnPosition = new List<float>()
        {
            11f, 8.5f, 6f, 3.2f
        };
    }

    public void GenerateEnemyPosition()
    {
        CreateEnemyPosition();
        for(int count = 0; count < Random.Range(1, _enemySpawnPosition.Count); count++)
        {
            var randomPosition = Random.Range(0, _enemySpawnPosition.Count);
            InstantiateEnemyPref(randomPosition);
            _enemySpawnPosition.Remove(_enemySpawnPosition[randomPosition]);
        }
    }

    private void InstantiateEnemyPref(int yCoord)
    {
        GameObject prefab = Instantiate(_enemyPref, new Vector3(1f, _enemySpawnPosition[yCoord], -1f), Quaternion.identity);
        _instantiateEnemys.Add(prefab);
    }
}
