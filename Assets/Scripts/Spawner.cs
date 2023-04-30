using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _timeToSpawn = 1f;
    [SerializeField] private List<Agent2D> _enemy;
    [SerializeField, Range(10, 100)] private int timeBtwWaves;

    [SerializeField] private Transform _pointsList;
    private List<Vector3> _points = new List<Vector3>();
    [SerializeField] private int[] WaveSize;

    [SerializeField] private Wallet _wallet;

    private int _enemiesSpawned;
    private bool isGameEnded = false;
    [SerializeField] public Canvas winCanvas;
    [SerializeField] private TextMeshProUGUI waveInfoText;

    void Start()
    {
        foreach (Transform way in _pointsList)
        {
            _points.Add(way.position);
        }
        StartCoroutine(SpawnWave());
        
        winCanvas.gameObject.SetActive(!true);
    }

    public int CurrentWave 
    { 
        get => _currentWave;
        set
        { 
            if (_currentWave >= WaveSize.Length)
            {
                isGameEnded = true;
            }
            else
                _currentWave = value;
        }
    }


    private int _currentWave = 0;

    private void GameWon()
    {
        winCanvas.gameObject.SetActive(true);
    }

    private IEnumerator SpawnWave()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeToSpawn);
        waveInfoText.text = $"Wave: {CurrentWave} Enemies: {WaveSize[_currentWave]}";
        while (_enemiesSpawned <= WaveSize[_currentWave])
        {
            {
                _enemiesSpawned++;
                Agent2D aiEnemy = Instantiate(_enemy[Random.Range(0, _enemy.Count)], _points[Random.Range(0, _points.Count)], Quaternion.identity, transform);

                aiEnemy.GetComponent<EnemyHealth>().RegisterEvent(_wallet.AddMoney);

                aiEnemy.GetComponent<EnemyHealth>().RegisterEvent(
                    (int num) =>
                    {
                        Spawner spwn = this;
                        spwn._enemiesSpawned--;
                        waveInfoText.text = $"Wave: {CurrentWave} Enemies: {spwn._enemiesSpawned}";
                        if (spwn._enemiesSpawned <= 0)
                        {
                            if (spwn._currentWave >= WaveSize.Length)
                            {
                                GameWon();
                            }
                            else
                            {
                                StartCoroutine(SpawnWave());
                                spwn.CurrentWave++;
                            }

                        }
                    });

                yield return wait;
            }
        }
    }
}