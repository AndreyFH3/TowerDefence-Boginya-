using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void onDeathEvent(int amount);
public class EnemyHealth : Health
{

    [SerializeField] private int priceForKill;
    private onDeathEvent onDeath;

    public void RegisterEvent(onDeathEvent action)
    {
        onDeath += action; 
    }

    public void UnregisterEvent()
    {
        onDeath -= null;
    }

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
            onDeath?.Invoke(priceForKill);
            Destroy(gameObject);
            //GetComponent<Animator>().SetTrigger("Dead"); анимация смерти
            //Выключение функции атаки
        }
    }
    //public void PlayDestroyAimation() => Destroy(gameobject);
}
