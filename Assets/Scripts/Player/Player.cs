using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private PlayerController _controller;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * _moveSpeed;
        _controller.Move(moveVelocity);
    }
}

