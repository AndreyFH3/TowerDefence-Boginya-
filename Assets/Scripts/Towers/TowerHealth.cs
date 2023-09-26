using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TowerHealth : Health, IRepairable
{
    public bool IsBroken { get; private set; } = false;
    public bool IsSet { get; set; } = false;
    [SerializeField] private protected string Name;
    public int Damage { private protected set; get; }
    private void Awake()
    {
        Damage = GetComponent<PhysicalDamageTower>().Damage;
        _animator = GetComponent<Animator>();
        HealthCurrent = _maxHealth;
        _animator?.SetFloat("Health", (float)(HealthCurrent / _maxHealth));

    }

    public override void GetDamage(int damage)
    {
        if (IsSet)
        {
            HealthCurrent -= damage;
            _animator?.SetFloat("Health", (float)(HealthCurrent / _maxHealth));
        }
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
        DataToShow dts = new DataToShow(GetComponent<SpriteRenderer>().sprite, HealthCurrent, Damage, Name, enemyType.ConvertTypeObject());
        return dts;
    }
}