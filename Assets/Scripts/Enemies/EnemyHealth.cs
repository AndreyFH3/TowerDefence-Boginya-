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

    public override DataToShow GetInfo()
    {
        DataToShow dts = new DataToShow(GetComponent<SpriteRenderer>().sprite, HealthCurrent, 10, name, enemyType.ToString());
        return dts;
    }

    public override void OnDead()
    {
        if(TryGetComponent(out Agent2D e))
        {
            e.DisableAgent();
            e.enabled = false;
            onDeath?.Invoke(priceForKill);
            GetComponent<Animator>().SetTrigger("Dead");
        }
    }


    public void PlayDestroyAimation() => Destroy(gameObject);
}
