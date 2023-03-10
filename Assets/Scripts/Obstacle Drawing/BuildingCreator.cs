using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[RequireComponent(typeof(CameraController), typeof(BuildSystem))]
public class BuildingCreator : MonoBehaviour
{
    [SerializeField] private Tilemap _previeMap;
    [SerializeField] private Tilemap _setableMap;
    private TileBase _tileBase { 
        get 
        { 
            if (_item == null) 
                return null; 
            else 
                return _item.Tile;
        }
    }

    private CameraControlScheme _controller;
    private BuildingObectbase _item;

    private Vector2 _mousePosition;
    private Vector3Int _currentGridPosition;
    private Vector3Int _lastGridPosition;

    private Camera _camera;

    public bool IsTileSelected => _item != null;
    public BuildingObectbase GetSelectedObstacle => _item;
    private BuildSystem buildSystem;

    private void Start()
    {
        _camera = Camera.main;
        _controller = GetComponent<CameraController>().GetController;

        _controller.Building.Build.performed += _ => Build();

        buildSystem = GetComponent<BuildSystem>();
    }

    private void Build()
    {
        if (BuildSystem.IsClickOnUI()) 
        { 
            _previeMap.SetTile(_currentGridPosition, null);
        }
        else if(_setableMap.GetTile(_currentGridPosition) != null) 
        { 
            _previeMap.SetTile(_currentGridPosition, null);
        }
        else
            _setableMap.SetTile(_currentGridPosition, _tileBase);

    }

    public void SelectObject(BuildingObectbase obj)
    {
        _item = obj;
        buildSystem.StopBuild();
    }

    public void Deselect()
    {
        _item = null;
        _previeMap.SetTile(_lastGridPosition, null);
    }

    private void Update()
    {
        if (_item == null) return;
        
        _mousePosition = _controller.Building.SetPosition.ReadValue<Vector2>();

        Vector3 position = _camera.ScreenToWorldPoint(_mousePosition);
        Vector3Int gridPosition = _previeMap.WorldToCell(position);

        if (gridPosition == _currentGridPosition)
            return;

        _lastGridPosition = _currentGridPosition;
        _currentGridPosition = gridPosition;

        UpdatePreview();
    }

    private void UpdatePreview()
    {
        _previeMap.SetTile(_lastGridPosition, null);
        _previeMap.SetTile(_currentGridPosition, _tileBase);
    }
}
