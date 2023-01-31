using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private void Awake()
    {
        gameCamera = GetComponent<Camera>();
        controller = new CameraControlScheme();
        controller.Movement.StartDeltaDrag.performed  += _ => isMoving = true;
        controller.Movement.StartDeltaDrag.canceled  += _ => isMoving = false;
    }

    private void OnEnable()
    {
        controller.Enable();
    }

    private void OnDisable()
    {
        controller.Disable();
    }

    private void Update()
    {
        MoveCamera();
        Zoom();
    }

    private void Zoom()
    {
        float delta = Clamper101(-controller.Movement.ZoomMouse.ReadValue<float>());
        //if (delta < minZoom)
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
            Vector2 moveDirection = controller.Movement.DeltaMove.ReadValue<Vector2>().normalized * (-moveSpeed * Time.deltaTime);
            if(moveDirection.magnitude == 0) return ;
               transform.Translate(moveDirection);

            transform.position = new Vector3 (Mathf.Clamp(transform.position.x, minTransform.position.x, maxTransform.position.x), Mathf.Clamp(transform.position.y, minTransform.position.y, maxTransform.position.y), -10);
        }
    }
}
