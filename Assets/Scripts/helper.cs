using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class helper : MonoBehaviour
{
    public void IsHeroActive(bool isActive)
    {
        PlayerPrefs.SetInt("player", isActive ? 1 : 0);
    }
}
