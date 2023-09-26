using UnityEngine;

public enum FindEnemyTyepe { First = 0, Last = 1, MaxHealth = 2 }
public enum DamageType { Physical = 0, Water = 1, Fire = 2, Electricity = 3 }
public abstract class Tower : MonoBehaviour
{

    [Header("enemies")]
    [SerializeField] private FindEnemyTyepe _enemyType;
    [SerializeField] private protected LayerMask enemyMask;
    
    [Header("Main Tower")]
    [SerializeField] private Transform _mainTowerTrnaform;

    [Header("Tower Information")]
    [SerializeField] private protected float _timeToNextShoot;
    [SerializeField] private protected float _shootTime;
    [SerializeField] private protected float _range;
    [SerializeField] private protected int _damage;
    public int Damage => _damage;

    public abstract void Attack(Transform enemyPosition);

    public Transform FindEnemy()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, _range, enemyMask);
        if (cols.Length <= 0) return null;

        if (_enemyType == FindEnemyTyepe.First) 
        {
            return FirstEnemy(cols);
        }
        else if (_enemyType == FindEnemyTyepe.Last)
        {
           return LastEnemy(cols);
        }
        else
        {
            return MaxHealthEnemy(cols);
        }
    }

    public Transform FirstEnemy(Collider2D[] colliders)
    {
        Transform tempTransform = colliders[0].transform;
        float distance = 100;

        foreach (Collider2D col in colliders)
        {
            if (col.transform.TryGetComponent(out Health h) & h.IsDead) continue;

            float tempDistance = Vector3.Distance(col.transform.position, transform.position);
            if (distance > tempDistance)
            {
                distance = tempDistance;
                tempTransform = col.transform;
            }
        }
        return tempTransform;
    }

    public Transform LastEnemy(Collider2D[] colliders)
    {
        Transform tempTransform = colliders[0].transform;
        float distance = 0;

        foreach (Collider2D col in colliders)
        {
            if (col.transform.TryGetComponent(out Health h) & h.IsDead) continue;
            float tempDistance = Vector3.Distance(col.transform.position, transform.position);
            if (distance < tempDistance)
            {
                distance = tempDistance;
                tempTransform = col.transform;
            }
        }
        return tempTransform;
    }

    public Transform MaxHealthEnemy(Collider2D[] colliders) 
    {
        Transform tempTransform = colliders[0].transform;
        int tempHealth = 0;

        foreach (Collider2D col in colliders)
        {
            if (col.transform.TryGetComponent(out Health h) & h.IsDead) continue;
            if(h.HealthCurrent > tempHealth)
            {
                tempHealth = h.HealthCurrent;
                tempTransform = h.transform;
            }
        }
        return tempTransform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
