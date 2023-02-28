using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Health : MonoBehaviour
{
    [SerializeField, Range(10,250)] 
    private protected int _maxHealth = 100;
    
    private void Awake()
    {
        HealthCurrent = _maxHealth;
    }

    public int HealthCurrent { 
        private protected set 
        { 
            HealthCurrent = value;

            if (HealthCurrent <= 0)
                OnDead();
            else if (HealthCurrent >= _maxHealth)
                HealthCurrent = _maxHealth;
        } 
        get => HealthCurrent;
    }

    protected Animator _animator;

    public abstract void GetDamage(int damage);

    public abstract void Heal(int hp);

    public abstract void OnDead();

}
