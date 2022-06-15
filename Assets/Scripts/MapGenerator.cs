using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Transform _tilePrefab;
    [SerializeField] private Transform _obstaclePrefab;
    [SerializeField] private Vector2 _mapSize;
    [SerializeField]private int seed = 10;
    [Range(0, 1)]
    [SerializeField] private float _outlinePercent;

    private List<Coord> _allTileCoords;
    private Queue<Coord> _shuffleTileCoords;
    private void Start()
    {
        GeneratorMap(); 
    }

    public void GeneratorMap()
    {
        _allTileCoords = new List<Coord>();
        
        for (int x = 0; x < _mapSize.x; x++)
        {
            for (int y = 0; y < _mapSize.y; y++)
            {
                _allTileCoords.Add(new Coord(x,y));
            }
        }

        _shuffleTileCoords = new Queue<Coord>(Utility.ShuffleArray(_allTileCoords.ToArray(),seed));

        string holderName = "Generated Map";

        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;
        
        for (int x = 0; x < _mapSize.x; x++)
        {
            for (int y = 0; y < _mapSize.y; y++)
            {
                Vector3 tilePosition = CoordToPosition(x, y);
                Transform newTile = Instantiate(_tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90f));
                newTile.localScale = Vector3.one * (1f - _outlinePercent);
                newTile.parent = mapHolder;
            } 
        }

        bool[,] obstacleMap = new bool[(int)_mapSize.x, (int)_mapSize.y];
        

        int obstacleCount = 10;
        
        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            Vector3 obstaclePosition = CoordToPosition(randomCoord.X, randomCoord.Y);
            Transform newObstacle = Instantiate(_obstaclePrefab, obstaclePosition + Vector3.up *.5f, Quaternion.identity);
            newObstacle.parent = mapHolder;
        }
        
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = _shuffleTileCoords.Dequeue();
        _shuffleTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    private Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-_mapSize.x / 2 + .5f + x, 0, -_mapSize.y / 2 + .5f + y);
    }
    
    public struct Coord
    {
        public int X;
        public int Y;
        
        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
