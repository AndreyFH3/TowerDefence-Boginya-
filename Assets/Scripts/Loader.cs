using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [Header("Loading Window")]
    [SerializeField] private GameObject window;
    [Header("Load Objects")]
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _percents;
    [SerializeField] private Button _endButton;
    [Header("Data")]
    [SerializeField] private Button _startButton;
    [SerializeField] private int _levelIndex;
    [SerializeField] private EventSystem _eventSystem;
    private bool _canLoad = false;

    private void Start()
    {
        _startButton.onClick.AddListener(
            delegate
            {
                StartCoroutine(Load(_levelIndex));
            });
    }

    private IEnumerator Load(int index)
    {
        _eventSystem.firstSelectedGameObject = null;
        window.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        //operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            _slider.value = operation.progress / .9f;
            _percents.text = $"{operation.progress / .9f * 100}%";
            if (operation.isDone)
            {
                _slider.gameObject.SetActive(false);
                _percents.gameObject.SetActive(false);
            }
            yield return null;
        }

    }

}
