using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateHero : MonoBehaviour
{
    private void Awake()
    {
        if(PlayerPrefs.GetInt("player", 1) == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
