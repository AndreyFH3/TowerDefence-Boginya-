using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CharacterHealth : Health
{
    [SerializeField] private float _timeToReborn = 7.5f;
    [SerializeField] private Image _rebornImage;

    private void Awake()
    {
        HealthCurrent = _maxHealth;
        _rebornImage.fillAmount = _timeToReborn;
        _animator = GetComponent<Animator>();
        _rebornImage.gameObject.SetActive(false);
    }

    public override void GetDamage(int damage)
    {
        HealthCurrent -= damage;
    }

    public override void Heal(int hp)
    {
        HealthCurrent += hp;
    }

    public override DataToShow GetInfo()
    {
        DataToShow dts = new DataToShow(GetComponent<SpriteRenderer>().sprite, HealthCurrent, 10, name, enemyType.ToString());
        return dts;
    }

    public override void OnDead()
    {
        if (TryGetComponent(out Agent2D e))
        {
            e.DisableAgent();
            e.enabled = false;
            GetComponent<Animator>().SetTrigger("Dead");
            StartCoroutine(Reborn());
        }
    }


    private IEnumerator Reborn()
    {
        float rebornTime = _timeToReborn;
        _rebornImage.gameObject.SetActive(true);
        while (rebornTime > 0)
        {
            rebornTime -= Time.deltaTime;
            _rebornImage.fillAmount = 1 - rebornTime / _timeToReborn;
            yield return null;
        }
        
        IsDead = false;
        HealthCurrent = _maxHealth;
        
        if(TryGetComponent(out Agent2D e))
            e.EnableAgent();
        GetComponent<Animator>().SetTrigger("Alive");

        _rebornImage.gameObject.SetActive(false);
    }
}
