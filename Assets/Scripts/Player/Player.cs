using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(GunController))]
public class Player : LivingEntity
{
    [SerializeField] private float _moveSpeed = 5f;

    private Camera _viewCamera;
    private PlayerController _controller;
    private GunController _gunController;

    protected override void Start()
    {
        base.Start();
        _controller = GetComponent<PlayerController>();
        _viewCamera = Camera.main;
        _gunController = GetComponent<GunController>();
    }

    private void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * _moveSpeed;
        _controller.Move(moveVelocity);

        Ray ray = _viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance));
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            _controller.LookAt(point);
        }

        if (Input.GetMouseButton(0))
        {
            _gunController.Shoot();
        }
    }
}

