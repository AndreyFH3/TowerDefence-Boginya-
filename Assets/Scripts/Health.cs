using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private protected int _health = 100;
    public int HealthCurrent { get => _health; }

    public abstract void GetDamage(int damage);

    
}
