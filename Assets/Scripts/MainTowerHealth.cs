using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainTowerHealth : Health , IRepairable
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Slider _healthSlider;
   
    private void Awake()
    {
        _animator = GetComponent<Animator>();    
        _animator.SetInteger("Condition", HealthCurrent/_maxHealth);
        _healthText.text = $"TowerHP: {HealthCurrent}";
        HealthCurrent = _maxHealth;
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _maxHealth;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyAttack ea))
        {
            GetDamage(ea.damage);
            Destroy(ea.gameObject);
        }
    }

    public override void GetDamage(int damage)
    {
        HealthCurrent -= damage;
        
        _healthSlider.value = HealthCurrent;
        _animator.SetFloat("Condition", HealthCurrent/_maxHealth);
        _healthText.text = $"{HealthCurrent}";
    }

    public override void Heal(int hp)
    {
        HealthCurrent += hp;
        _healthSlider.value = HealthCurrent;
    }

    public override void OnDead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //по хорошему - сделать меню проигрыша с информаированием о том, что да как
        //добавить на процессе создания UI
    }

    public void Repair()
    {
        //some Code To Reapir Tower
        //Add after creating UI and Earn System
    }
}
