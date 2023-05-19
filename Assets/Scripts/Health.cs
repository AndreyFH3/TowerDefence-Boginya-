using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;

public abstract class Health : MonoBehaviour, IInformable, IDamagable
{
    [SerializeField] private protected DamageType enemyType;
    public DamageType GetDamageType { get => enemyType; }
    public bool IsDead { get; private protected set; } = false;


    [SerializeField, Range(10,250)] 
    private protected int _maxHealth = 100;
    private int _health;
    
    public int HealthCurrent { 
        private protected set 
        {
            _health = value;
            if (_health <= 0)
            {
                if (IsDead) return;
                OnDead();
                IsDead = true;
            }
            else if (_health >= _maxHealth)
                _health = _maxHealth;
        } 
        get => _health;
    }

    protected Animator _animator;

    public abstract void GetDamage(int damage);

    public abstract void Heal(int hp);

    public abstract void OnDead();

    public abstract DataToShow GetInfo();
}

public static class Extencion
{
    public static string ConvertTypeObject(this DamageType type) => type switch
    {
        DamageType.Physical => "Физический",
        DamageType.Fire => "Огненный",
        DamageType.Water => "Водный",
        DamageType.Electricity => "Электрический",
        _ => "default"
    };
}