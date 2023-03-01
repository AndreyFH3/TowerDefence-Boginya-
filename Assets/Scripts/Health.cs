using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private protected DamageType enemyType;
    public DamageType GetEnemyType { get => enemyType; }


    [SerializeField, Range(10,250)] 
    private protected int _maxHealth = 100;
    private int _health;
    
    public int HealthCurrent { 
        private protected set 
        {
            _health = value;
            if (_health <= 0)
                OnDead();
            else if (_health >= _maxHealth)
                _health = _maxHealth;
        } 
        get => _health;
    }

    protected Animator _animator;

    public abstract void GetDamage(int damage);

    public abstract void Heal(int hp);

    public abstract void OnDead();

}
