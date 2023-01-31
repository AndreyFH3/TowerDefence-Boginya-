using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _maxWave = 0;
    [SerializeField] private float _timeToSpawn = 1f;
    [SerializeField] private EnemyAI _enemy;

    [SerializeField] private Transform _target;

    [SerializeField] private Transform _pointsList;
    private List<Vector3> _points = new List<Vector3>();

    private IEnumerator corutine;

    private int _currentWave = 0;
    private int _currentWaveEnemy = 0;

    private Vector3 towerPosition => _target.position;

    void Start()
    {
        foreach(Transform way in _pointsList)
        {
            _points.Add(way.position);
        }
        corutine = spawn();
        StartCoroutine(corutine);
    }

    private void CountEnemy(int amountEnemy)
    {
        _currentWaveEnemy = amountEnemy;
    }

    public void DeleteEnemy() => _currentWaveEnemy--;

    private IEnumerator spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeToSpawn);
        while (true)
        {
            EnemyAI aiEnemy = Instantiate(_enemy, _points[Random.Range(0, _points.Count)], Quaternion.identity, transform);
            aiEnemy.GetComponent<AIData>().currentTarget = _target;
            yield return wait;
        }
    }
    

}
