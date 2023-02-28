using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private DamageType enemyType;
    public DamageType GetEnemyType { get => enemyType; }
    public override void GetDamage(int damage)
    {
        HealthCurrent -= damage;
    }

    public override void Heal(int hp)
    {
        HealthCurrent += hp;
    }

    public override void OnDead()
    {
        if(TryGetComponent(out EnemyAI e))
        {
            e.enabled = false;
            //GetComponent<Animator>().SetTrigger("Dead"); анимация смерти
            //Выключение функции атаки
        }
    }
    //public void PlayDestroyAimation() => Destroy(gameobject);
}
