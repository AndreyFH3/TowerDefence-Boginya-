using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private int damage;
    public void Attack()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, 3, LayerMask.GetMask("Player"));

        if (col != null)
        {
            Bullet b = Instantiate(bullet);
            b.SetDamage(damage);
            if (col.transform.TryGetComponent(out TowerHealth pdt))
            {
                b.DamageObject(pdt);
            }
            else if(col.transform.TryGetComponent(out MainTowerHealth mth))
            {
                b.DamageObject(mth);
            }
            Destroy(b.gameObject);
        } 
    }
}
