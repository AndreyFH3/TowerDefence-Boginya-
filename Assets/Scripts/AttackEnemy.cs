using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask objectsLayerMask;
    public int Damage { get => _damage; private set => _damage = value; }
    public void Attack()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, 3f, objectsLayerMask);

        if (col == null) return;

        if (col.transform.TryGetComponent(out IDamagable health))
        {
            Bullet b = Instantiate(bullet);
            b.SetDamage(Damage);
            if (health is Health pdt)
               b.DamageObject(pdt);
            
            Destroy(b.gameObject);
        } 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}
