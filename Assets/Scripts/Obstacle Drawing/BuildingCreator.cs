using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(CameraController), typeof(BuildSystem))]
public class BuildingCreator : BuildS, IBuildable
{
    [SerializeField] private Tilemap _previeMap;
    [SerializeField] private Tilemap _setableMap;

    public override bool IsSelected { get => _item != null; }
    private TileBase TileBase { 
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

    public BuildingObectbase GetSelectedObstacle => _item;

    private void Start()
    {
        _camera = Camera.main;
        _controller = GetComponent<CameraController>().GetController;


        //buildSystem = GetComponent<BuildSystem>();
    }

    public override void Build()
    {
        if (BuildSystem.IsClickOnUI()) 
        {
            StopBuild();
        }
        else if(_setableMap.GetTile(_currentGridPosition) != null) 
        { 
            _previeMap.SetTile(_currentGridPosition, null);
        }
        else
            _setableMap.SetTile(_currentGridPosition, TileBase);

    }

    public override void Select(object obj)
    {
        if (obj is BuildingObectbase o)
        {
            _item = o;
            //buildSystem.StopBuild();
        }
    }

    public override void StopBuild()
    {
        _previeMap.SetTile(_lastGridPosition, null);
        _previeMap.SetTile(_currentGridPosition, null);
        _item = null;
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
        _previeMap.SetTile(_currentGridPosition, TileBase);
    }

    public void Deselect()
    {
        StopBuild();
    }

}
