using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CameraController : MonoBehaviour
{
    [Header("Change Psoition")]
    [SerializeField] private Transform maxTransform;
    [SerializeField] private Transform minTransform;
    [SerializeField, Range (1, 100)] private float moveSpeed = 1;

    [Header("Change Zoom")]
    [SerializeField] private float maxZoom;
    [SerializeField] private float minZoom;
    [SerializeField, Range(1, 5)] private float zoomChangeSpeed = 1;

    private CameraControlScheme controller;
    private bool isMoving = false;
    private Camera gameCamera;
    private Informator informator;
    [SerializeField] private LayerMask infoLayerMask;
    private Vector2 movePositionStarted;
    private IInformable informable;
    public bool IsInfoSelected { get
        {
            if(informable is EnemyHealth h && h == null) 
                return false;
            else
                return informable != null;
        }
    }
    public CameraControlScheme GetController { 
        get 
        { 
            if(controller == null)
                controller = new CameraControlScheme();
            return controller; 
        } 
    }

    private void Awake()
    {
        informator = FindObjectOfType<Informator>();
        gameCamera = GetComponent<Camera>();
        gameCamera = Camera.main;
        GetController.Movement.StartDeltaDrag.performed += _ =>
        {
            informable = null;
            informator.ShowInfo(null);
            FinInfoObjcet();

            movePositionStarted = gameCamera.ScreenToWorldPoint(GetController.Movement.PositionMouse.ReadValue<Vector2>());

            if (!BuildSystem.IsClickOnUI())
            {
                isMoving = true;
            }
        };
     
        GetController.Movement.StartDeltaDrag.canceled += _ =>
        {
            isMoving = false;
        };
    }

    private void OnEnable()
    {
        GetController.Enable();
    }

    private void OnDisable()
    {
        GetController.Disable();
    }

    private void Update()
    {
        MoveCamera();
        Zoom();
        if (IsInfoSelected)
        {
            informator.ShowInfo(informable.GetInfo());
        }
        else
            informator.ShowInfo(null);
    }

    private void Zoom()
    {
        float delta = Clamper101(-GetController.Movement.ZoomMouse.ReadValue<float>());
        if(delta == 0) return;
        else if(maxZoom >= gameCamera.orthographicSize + delta && gameCamera.orthographicSize + delta >= minZoom)
            gameCamera.orthographicSize += delta * zoomChangeSpeed;

    }

    private float Clamper101(float delta)
    {
        if (delta < -1) return -1;
        else if (delta > 1) return 1;
        return delta;
    }

    private void MoveCamera()
    {
        if (isMoving)
        {
            Vector2 delta = Camera.main.ScreenToWorldPoint(GetController.Movement.PositionMouse.ReadValue<Vector2>());
            
            Vector2 moveDirection = movePositionStarted - delta;

            transform.Translate(moveDirection);

            transform.position = new Vector3 (Mathf.Clamp(transform.position.x, minTransform.position.x, maxTransform.position.x), Mathf.Clamp(transform.position.y, minTransform.position.y, maxTransform.position.y), -10);
        }
    }

    private void FinInfoObjcet()
    {
        Vector2 r = Camera.main.ScreenToWorldPoint(GetController.Movement.PositionMouse.ReadValue<Vector2>());
        RaycastHit2D hit = Physics2D.Raycast(r, Vector2.zero, infoLayerMask);

        if (hit.collider != null)
        {
            if (hit.transform.TryGetComponent(out IInformable s))
            {
                informable = s;
            }
        }
    }
}
