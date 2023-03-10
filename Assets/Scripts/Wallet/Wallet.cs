using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _money = 100;
    [Header("UI Objects")]
    [SerializeField] private TextMeshProUGUI _moneyText;

    public int MoneyAmount { 
        get => _money; 
        private set 
        { 
            _money = value;  
            UIUpdate();
        } 
    }

    private void Awake()
    {
        MoneyAmount = _money;    
    }

    public bool CanBuy(int price) => MoneyAmount - price >= 0;

    public bool Buy(int price)
    {
        if (CanBuy(price))
        {
            MoneyAmount -= price;
            return true;
        }
        else return false;
    }

    private void UIUpdate()
    {
        _moneyText.text = $"Золотых: {MoneyAmount}";
    }

    public void AddMoney(int amount)
    {
        MoneyAmount += amount;
    }
}
