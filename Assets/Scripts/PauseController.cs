using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] private Button activatePause;
    [SerializeField] protected Button disablerPause;
    [SerializeField] private Image pauseCanvas;
    public bool IsPauseActive => pauseCanvas.gameObject.activeSelf;

    private void SetPauseCondition()
    {
        pauseCanvas.gameObject.SetActive(!IsPauseActive);
        Time.timeScale = IsPauseActive ? 0 : 1;
    }

    private void OnEnable()
    {
        activatePause.onClick.AddListener(SetPauseCondition);
        disablerPause.onClick.AddListener(SetPauseCondition);
    }

    private void OnDisable()
    {
        activatePause.onClick.RemoveAllListeners();
        disablerPause.onClick.RemoveAllListeners();
    }
}
