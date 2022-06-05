using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 10f;

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
