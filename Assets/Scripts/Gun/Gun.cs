using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private float _msBetweenShoot = 100f;
    [SerializeField] private float _muzzleVelocity = 35f;

    private float _nextShootTime;
    private float _ms = 1000f;
    public void Shoot()
    {
        if (Time.time > _nextShootTime)
        {
            _nextShootTime = Time.time + _msBetweenShoot / _ms;
        }
        
        Projectile projectile = Instantiate(_projectile, _muzzle.position, _muzzle.rotation);
        projectile.SetSpeed(_muzzleVelocity);
    }
}