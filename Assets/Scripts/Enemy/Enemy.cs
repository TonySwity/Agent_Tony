using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent _pathfinder;

    private void Start()
    {
        _pathfinder = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        
    }
}
