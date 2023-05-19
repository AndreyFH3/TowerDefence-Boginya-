using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainTowerHealth : TowerHealth, IRepairable
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Canvas onDeadShow;
   
    private void Awake()
    {
        Damage = 0;
        _animator = GetComponent<Animator>();    
        HealthCurrent = _maxHealth;
        _animator.SetInteger("Health", HealthCurrent/_maxHealth);
        _healthText.text = $"{HealthCurrent}/{_maxHealth}";
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _maxHealth;
        IsSet = true;
    }

    public override void GetDamage(int damage)
    {
        HealthCurrent -= damage;
        
        _healthSlider.value = HealthCurrent;
        _animator.SetFloat("Health", HealthCurrent/_maxHealth);
        _animator.SetTrigger("Red");
        _healthText.text = $"{HealthCurrent}/{_maxHealth}";
    }

    public override void Heal(int hp)
    {
        HealthCurrent += hp;
        _healthSlider.value = HealthCurrent;
    }

    public override void OnDead()
    {
        onDeadShow.gameObject.SetActive(true);
    }

    public override void Repair()
    {
        //some Code To Reapir Tower
        //Add after creating UI and Earn System
    }

    public override DataToShow GetInfo()
    {
        DataToShow dts = new (GetComponent<SpriteRenderer>().sprite, HealthCurrent, Damage, Name, enemyType.ToString());
        return dts;
    }

}
