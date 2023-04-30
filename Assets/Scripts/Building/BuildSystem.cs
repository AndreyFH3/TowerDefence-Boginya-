using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildSystem : BuildS, IBuildable
{
    [SerializeField] private Transform parentObject;
    [SerializeField] private Wallet wallet;
    private Building currentBuilding;
    public override bool IsSelected { get => currentBuilding != null; }

    private bool CanSetTower { get
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
        foreach (BuildTower tower in buildTower)
        {
            tower.creatButton.onClick.AddListener(delegate
            {
                if(currentBuilding == null)
                    Select(tower.tower);
                else
                    StopBuild();

            });
            creator = GetComponent<BuildingCreator>();
        }
    }
    public override void Select(object objectToCreate)
    {
        if(objectToCreate is Building building)
        {
            creator.StopBuild();
            if(wallet.CanBuy(building.Price))
                currentBuilding = Instantiate(building, parentObject);
        }
    }

    public override void StopBuild()
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

    public override void Build()
    {
        if (IsClickOnUI())
        {
            return;
        }
        if (CanSetTower && wallet.Buy(currentBuilding.Price))
        {
            currentBuilding.SetTower();
            currentBuilding = null;
        }
        else
        {
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