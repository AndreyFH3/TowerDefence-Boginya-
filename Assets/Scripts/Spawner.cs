using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _timeToSpawn = 1f;
    [SerializeField] private List<EnemyAI> _enemy;
    [SerializeField, Range(10, 100)] private int timeBtwWaves;

    [SerializeField] private Transform _pointsList;
    private List<Vector3> _points = new List<Vector3>();
    [SerializeField] private int[] WaveSize;

    [SerializeField] private Wallet _wallet;

    private int _enemiesSpawned;
    private bool isGameEnded = false;

    void Start()
    {
        foreach (Transform way in _pointsList)
        {
            _points.Add(way.position);
        }
        StartCoroutine(SpawnWave());
    }

    public int CurrentWave 
    { 
        get => _currentWave;
        set
        { 
            _currentWave = value;
            if (_currentWave > WaveSize.Length)
            {
                isGameEnded = true;
                GameWon();
            }
        }
    }

    private int _currentWave = 0;

    private void GameWon()
    {
        Debug.Log("Уровень пройден! Необходимо доделать победный канвас позже!");
    }

    private IEnumerator SpawnWave()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeToSpawn);
        while (_enemiesSpawned < WaveSize[_currentWave])
        {
            if (!isGameEnded)
            {
                _enemiesSpawned++;
                EnemyAI aiEnemy = Instantiate(_enemy[0], _points[Random.Range(0, _points.Count)], Quaternion.identity, transform);
                aiEnemy.GetComponent<EnemyHealth>().RegisterEvent(_wallet.AddMoney);
                
                aiEnemy.GetComponent<EnemyHealth>().RegisterEvent(
                    (int num) => {
                        _enemiesSpawned--;
                        if (_enemiesSpawned <= 0)
                        {
                            StartCoroutine(SpawnWave());
                            CurrentWave++;
                            Debug.Log($"{_currentWave} волна!");
                        }
                });
            }

            yield return wait;
        }
    }
}