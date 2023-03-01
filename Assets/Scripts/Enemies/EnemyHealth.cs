using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private void Awake()
    {
        HealthCurrent = _maxHealth;    
    }

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
            Destroy(gameObject);
            //GetComponent<Animator>().SetTrigger("Dead"); анимация смерти
            //Выключение функции атаки
        }
    }
    //public void PlayDestroyAimation() => Destroy(gameobject);
}
