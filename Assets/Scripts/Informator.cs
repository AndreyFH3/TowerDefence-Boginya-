using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Informator : MonoBehaviour
{
    [Header("Data Showing")]
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _type;
    [SerializeField] private TextMeshProUGUI _lifes;
    [SerializeField] private TextMeshProUGUI _damage;

    [Header("Show On Gameobject")]
    [SerializeField] private GameObject ShowObject;

    public void ShowInfo(DataToShow dts)
    {
        if (dts == null)
        {
            ShowObject.SetActive(false);
            return;
        }
        _name.text = $"{dts.Name}";
        _image.sprite = dts.MainSprite;
        _lifes.text = $"Жизней: {dts.Lifes}";
        _type.text = $"Тип: {dts.Type}";
        _damage.text = $"Урон: {dts.Damage}";

        ShowObject.SetActive(true);

    }
}
