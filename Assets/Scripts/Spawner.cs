using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Wave[] _waves;

    private Wave _currentWave;
    private int _currentWaveNumber;
    private int _enemiesRemainingToSpawn;
    private int _enemiesRemainingAlive;
    private float _nextSpawnTime;

    
    private void Start()
    {
        NextWave();
    }

    private void Update()
    {
        if (_enemiesRemainingToSpawn > 0 && Time.time > _nextSpawnTime)
        {
            _enemiesRemainingToSpawn--;
            _nextSpawnTime = Time.time + _currentWave.TimeBetweenSpawns;

            Enemy spawnedEnemy = Instantiate(_enemy, Vector3.zero, Quaternion.identity);
            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
    }
    
    private void NextWave()
    {
        _currentWaveNumber++;

        if (_currentWaveNumber - 1 < _waves.Length)
        {
            _currentWave = _waves[_currentWaveNumber - 1];
            
            _enemiesRemainingToSpawn = _currentWave.EnemyCount;
            _enemiesRemainingAlive = _enemiesRemainingToSpawn;
        }

    }

    private void OnEnemyDeath()
    {
        _enemiesRemainingAlive--;

        if (_enemiesRemainingAlive == 0)
        {
            NextWave();
        }
    }
    
    [System.Serializable]
    public class Wave
    {
        public int EnemyCount;
        public float TimeBetweenSpawns;
    }

}
