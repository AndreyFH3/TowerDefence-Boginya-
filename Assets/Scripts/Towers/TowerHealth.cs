using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TowerHealth : Health, IRepairable
{
    public bool IsBroken { get; private set; } = false;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        HealthCurrent = _maxHealth;
    }

    public override void GetDamage(int damage)
    {
        HealthCurrent -= damage;
        //_animator?.SetFloat("Health", HealthCurrent/_maxHealth);
    }

    public override void Heal(int hp)
    {
        HealthCurrent += hp;
        //_animator?.SetFloat("Health", HealthCurrent / _maxHealth);
    }

    public override void OnDead()
    {
        if(TryGetComponent(out Tower t))
        {
            t.enabled = false;
            IsBroken = true;
            GetComponent<NavMeshObstacle>().enabled = false;
            gameObject.SetActive(false);
        }
    }

    public virtual void Repair()
    {
        //some Code To Reapir Tower
        //Add after creating UI and Earn System
    }

    public override DataToShow GetInfo()
    {
        DataToShow dts = new DataToShow(GetComponent<SpriteRenderer>().sprite, HealthCurrent, 10, name, enemyType.ToString());
        return dts;
    }


}
