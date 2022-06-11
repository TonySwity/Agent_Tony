using System;
using System.Collections;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State
    {
        Idle,
        Chasing,
        Attacking
    };

    private State _currentState;
    private NavMeshAgent _pathfinder;
    private Transform _target;

    private float _attackDistanceThreshold = 1.5f;
    private float _timeBetweenAttacks = 1f;

    private float _nextAttackTime;
    private float _myCollisionRadius;
    private float _targetCollisionRadius;

    [SerializeField]
    protected override void Start()
    {
        base.Start();
        _pathfinder = GetComponent<NavMeshAgent>();

        _currentState = State.Chasing;
        _target = GameObject.FindObjectOfType<Player>().transform;
        _myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        _targetCollisionRadius = _target.GetComponent<CapsuleCollider>().radius;

        StartCoroutine(UpdatePath());
    }

    private void Update() 
    {
        if (Time.time > _nextAttackTime)
        {
            float sqrDistanceToTarget = (_target.position - transform.position).sqrMagnitude;

            if (sqrDistanceToTarget < Mathf.Pow(_attackDistanceThreshold, 2f))
            {
                _nextAttackTime = Time.time + _timeBetweenAttacks;
                //StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        _currentState = State.Attacking;
        _pathfinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 attackPosition = _target.position;

        float attackSpeed = 3f;
        float percent = 0;

        while (percent <= 1)
        {
            percent += Time.deltaTime + attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;

            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }

        _currentState = State.Chasing;
        _pathfinder.enabled = true;
    }

    private IEnumerator UpdatePath()
    {
        float refreshRate = .25f;

        while (_target != null)
        {
            if (_currentState == State.Chasing)
            {
                Vector3 directionToTarget = (_target.position - transform.position).normalized;
                Vector3 targetPosition = _target.position - directionToTarget * (_myCollisionRadius + _targetCollisionRadius);

                if (!Died)
                {
                    _pathfinder.SetDestination(targetPosition);
                }
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }
}