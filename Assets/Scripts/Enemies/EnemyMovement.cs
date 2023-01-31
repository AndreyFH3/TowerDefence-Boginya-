using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Vector3 _way;
    private Spawner _spawner;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _way, movementSpeed * Time.deltaTime);
    }

    public void SetSpawner(Spawner spwn)
    {
        if (_spawner != null) return;
        _spawner = spwn;
    }

    public void SetPointToMove(Vector3 towerPosition)
    {
        _way = towerPosition;
    }
}
