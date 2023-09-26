using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class BattalefieldBuilder : MonoBehaviour
{
    [SerializeField] private BuildS[] buildable;
    private CameraControlScheme controller;
    private bool selected;
    public bool Selected { get
        {
            foreach (var s in buildable)
                selected = selected || s.IsSelected;
            return selected;
        }
    }
    private void Start()
    {
        controller = GetComponent<CameraController>().GetController;

        foreach (var builds in buildable)
        {
            controller.Building.Build.performed += _ =>
            {
                if (BuildSystem.IsClickOnUI())
                {
                    builds.StopBuild();

                }
                else if (builds.IsSelected)
                {
                    builds.Build();
                    
                }
            };
            controller.Building.RemoveBuilding.performed += _ => 
            { 
                builds.StopBuild(); 
            };
        }
    }
}
