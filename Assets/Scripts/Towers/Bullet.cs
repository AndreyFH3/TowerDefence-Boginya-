using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Transform destination;
    [SerializeField] private float _speed;
    [SerializeField] private DamageType type;
    internal DamageType GetDamageType { get => type; }
    private int _damage;
    public int Damage { get => _damage; }

    void Update()
    {
        if(destination == null)
        {
            Destroy(gameObject);
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

    public  void SetDamage(int damage)
    {
        _damage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyHealth health))
        {
            CombatEngine.DamageObject(this, health);
            Destroy(gameObject);
        }
    }

    public void SetType(DamageType type) => this.type = type;
}
