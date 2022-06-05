using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent _pathfinder;
    private Transform _target;

    private void Start()
    {
        _pathfinder = GetComponent<NavMeshAgent>();
        _target = GameObject.FindObjectOfType<Player>().transform;
        StartCoroutine(UpdatePath());
    }
    
    private IEnumerator UpdatePath()
    {
        float refreshRate = .25f;

        while (_target != null)
        {
            Vector3 targetPosition = new Vector3(_target.position.x, 0f, _target.position.z);
            _pathfinder.SetDestination(targetPosition);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
