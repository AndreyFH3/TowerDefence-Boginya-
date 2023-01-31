using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTowerHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private int _health;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();    
        _animator.SetInteger("Condition", _health);
        _healthText.text = $"TowerHP: {_health}";
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _animator.SetInteger("Condition", _health);
        _healthText.text = $"TowerHP: {_health}";

        if (_health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.TryGetComponent(out EnemyAttack ea))
        {
            TakeDamage(ea.damage);
            Destroy(ea.gameObject);
        }
    }
}
