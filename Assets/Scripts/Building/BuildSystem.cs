using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CameraController),typeof(BuildingCreator))]
public class BuildSystem : MonoBehaviour
{
    [SerializeField] private Transform parentObject;
    [SerializeField] private Wallet wallet;
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
    private BuildingCreator creator;
    //вернуть как настрою анимации на канвасе с игровым UI
    //[SerializeField] private Animator animator;

    private void Start()
    {
        controller = GetComponent<CameraController>().GetController;
        controller.Building.Build.canceled += _ => BuildTower();
        controller.Building.RemoveBuilding.performed += _ => StopBuild();
        foreach (BuildTower tower in buildTower)
        {
            tower.creatButton.onClick.AddListener(delegate
            {
                if(currentBuilding == null)
                    CreateTowers(tower.tower);
                else
                    StopBuild();

            });
            creator = GetComponent<BuildingCreator>();
        }
    }

    internal void StopBuild()
    {
        if (currentBuilding == null)
        {
            return;
        }
        Destroy(currentBuilding.gameObject);
        currentBuilding = null;
    }

    private void Update()
    {
        if(currentBuilding == null) return;

        Vector2 position = Camera.main.ScreenToWorldPoint(controller.Building.SetPosition.ReadValue<Vector2>());
        currentBuilding.transform.position = position; 
    }

    private void CreateTowers(Building building)
    {
        creator.Deselect();
        if(wallet.CanBuy(building.Price))
            currentBuilding = Instantiate(building, parentObject);
    }

    private void BuildTower()
    {
        if (IsClickOnUI())
        {
            return;
        }
        if (canSetTower && wallet.Buy(currentBuilding.Price))
        {
            currentBuilding.SetTower();
            currentBuilding = null;
        }
        else
        {
            //animator.SetTrigger("CantBuild");
            Debug.Log("Тут надо анимацию, типа НЕ СТРОИЦА!!1!");
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