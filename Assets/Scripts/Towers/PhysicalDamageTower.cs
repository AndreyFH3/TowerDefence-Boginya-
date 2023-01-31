using System.Collections;
using UnityEngine;

public class PhysicalDamageTower : Tower
{
    [SerializeField] private Bullet _bullet;
    
    void Start()
    {
        StartCoroutine(FindEnemyToAttack());
    }

    private IEnumerator FindEnemyToAttack()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeToNextShoot);
        while (true)
        {
            Transform targetDestination = FindEnemy();
            if (targetDestination == null)
            {
                yield return null;
                continue;
            }
            else
            {
                Attack(targetDestination);
                yield return wait;
            }

        }
    }

    public override void Attack(Transform enemyPosition)
    {
        Bullet b = Instantiate(_bullet, transform.position, Quaternion.identity);
        b.SetDestination(enemyPosition);
    }
}