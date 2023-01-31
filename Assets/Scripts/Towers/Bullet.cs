using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Transform destination;
    [SerializeField] private float _speed;
    private float time = 0;
    private float timeDead = 3;

    void Update()
    {
        if(destination == null)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.TransformDirection(Vector2.up), _speed / 2 * Time.deltaTime);
            time += Time.deltaTime;
            if(time>= timeDead) Destroy(gameObject);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, destination.position, _speed * Time.deltaTime);
        }
    }

    public void SetDestination(Transform destination)
    {
        this.destination = destination;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyMovement em))
        {
            Destroy(em.gameObject);
            Destroy(gameObject);
        }
    }
}
