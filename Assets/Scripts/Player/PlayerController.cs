using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);
    }

    public void Move(Vector3 moveVelocity)
    {
        _velocity = moveVelocity;
    }
}
