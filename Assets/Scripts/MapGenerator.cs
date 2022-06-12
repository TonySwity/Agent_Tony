using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Transform _tilePrefab;
    [SerializeField] private Vector2 _mapSize;
    [Range(0, 1)]
    [SerializeField] private float _outlinePercent;

    private void Start()
    {
        GeneratorMap(); 
    }

    public void GeneratorMap()
    {
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
                Vector3 tilePosition = new Vector3(-_mapSize.x / 2 + .5f + x, 0, -_mapSize.y / 2 + .5f + y);
                Transform newTile = Instantiate(_tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90f));
                newTile.localScale = Vector3.one * (1f - _outlinePercent);
                newTile.parent = mapHolder;
            } 
        } 
    }
}
