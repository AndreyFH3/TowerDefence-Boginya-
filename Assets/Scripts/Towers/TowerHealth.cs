using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : Health, IRepairable
{
    public bool IsBroken { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public override void GetDamage(int damage)
    {
        HealthCurrent -= damage;
        _animator.SetFloat("Health", HealthCurrent/_maxHealth);
    }

    public override void Heal(int hp)
    {
        HealthCurrent += hp;
        _animator.SetFloat("Health", HealthCurrent / _maxHealth);
    }

    public override void OnDead()
    {
        if(TryGetComponent(out Tower t))
        {
            t.enabled = false;
            IsBroken = true;
        }
    }

    public void Repair()
    {
        //some Code To Reapir Tower
        //Add after creating UI and Earn System
    }

}
