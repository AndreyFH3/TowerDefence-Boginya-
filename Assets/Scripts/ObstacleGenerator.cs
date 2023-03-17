using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleGenerator : MonoBehaviour
{
    [Header("Max size obstacle")]
    [SerializeField] private int forestMaxSize = 10;
    [SerializeField] private int riverMaxSize = 10;

    [Header("Amount of obstacle ")]
    [SerializeField] private int _forestsAmount = 3;
    [SerializeField] private int _riversAmount = 3;

    [Header("Positions")]
    [SerializeField] private Transform _minPosition;
    [SerializeField] private Transform _maxPosition;

    [Header("Data")]
    [SerializeField] private Tilemap obstacleMap;
    [SerializeField] private BuildingObectbase _forestObstacle;
    [SerializeField] private BuildingObectbase _riverObstacle;


    private Vector3Int[] directions = {
        new Vector3Int(-1,-1,0),
        new Vector3Int(0,-1,0),
        new Vector3Int(1,-1,0),

        new Vector3Int(-1,0,0),
        new Vector3Int(1,0,0),

        new Vector3Int(-1,1,0),
        new Vector3Int(0,1,0),
        new Vector3Int(1,1,0)
    };
    void Start()
    {
        GenerateObstacle(obstacleMap, _forestObstacle, _forestsAmount, forestMaxSize);
        GenerateObstacle(obstacleMap, _riverObstacle, _riversAmount, riverMaxSize, true);
    }

    private void GenerateObstacle(Tilemap tilemap, BuildingObectbase obstacle,int amount = 1, int size = 1,bool straight = false)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3Int startPoint = GetStartPoint();
            int direction = Random.Range(0,2);
            int rotation = Random.Range(0,2);
            for (int j = 0; j < size; j++)
            {
                tilemap.SetTile(startPoint, obstacle.Tile);

                if (straight)
                    startPoint = StraightBuild(startPoint, direction, rotation);

                else
                    startPoint = NearBuild(startPoint);

                if (Random.Range(0, 100) > 98 || startPoint.magnitude < 2)
                {
                    break;
                }
            }
        }
    }

    private Vector3Int NearBuild(Vector3Int dataVector3) =>
        dataVector3 += directions[Random.Range(0, directions.Length)];



    private Vector3Int StraightBuild(Vector3Int dataVctor3, int direction, int rotation)
    {
        if (direction == 0)
        {
            if(rotation == 0)
            {
                dataVctor3.x++;
            }
            else
            {
                dataVctor3.x--;
            }
        }
        else
        {
            if (rotation == 0)
            {
                dataVctor3.y++;
            }
            else
            {
                dataVctor3.y--;
            }
        }
        return dataVctor3;
    }

    private Vector3Int GetStartPoint()
    {
        Vector3Int startPostion = new Vector3Int((int)Random.Range(_minPosition.position.x, _maxPosition.position.x), (int)Random.Range(_minPosition.position.y, _maxPosition.position.y), 0);
        if(startPostion.magnitude < 2)
            return GetStartPoint();
        return startPostion;
    }
}
