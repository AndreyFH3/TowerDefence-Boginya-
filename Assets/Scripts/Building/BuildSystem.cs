using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CameraController))]
public class BuildSystem : MonoBehaviour
{
    [SerializeField] private Transform parentObject;
    private Building currentBuilding;
    private bool canSetTower { get
        {
            if(currentBuilding == null)
                return false;

            return currentBuilding.CanSet;
        }
    }
    private CameraControlScheme controller;
    [SerializeField] private BuildTower[] buildTower;

    //вернуть как настрою анимации на канвасе с игровым UI
    //[SerializeField] private Animator animator;

    private void Start()
    {
        controller = GetComponent<CameraController>().GetController;
        controller.Building.Build.performed += _ => BuildTower();
        foreach (BuildTower tower in buildTower)
        {
            tower.creatButton.onClick.AddListener(delegate
            {
                CreateTowers(tower.tower);
            });
        }
    }

    private void Update()
    {
        if(currentBuilding == null) return;

        Vector2 position = Camera.main.ScreenToWorldPoint(controller.Building.SetPosition.ReadValue<Vector2>());
        currentBuilding.transform.position = position; 
    }

    private void CreateTowers(Building building)
    {
        currentBuilding = Instantiate(building, parentObject);
    }

    private void BuildTower()
    {
        if (IsClickOnUI())//EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (canSetTower)
        {
            currentBuilding.SetTower();
            currentBuilding = null;
        }
        else
        {
            //animator.SetTrigger("CantBuild");
        }
    }

    public static bool IsClickOnUI()
    {
        var pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };

        var raycastResultsList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);

        return raycastResultsList.Any(result => result.gameObject is GameObject);
    }
}