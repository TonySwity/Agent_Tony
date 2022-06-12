using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask _collisionMask;
    
    private float _speed = 10f;
    private float _damage = 1f;
    
    private float _lifeTime = 3;
    private float _skinWidth = 0.1f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 0.1f, _collisionMask);
        if (initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0]);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    private void Update()
    {
        float moveDistance = _speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance + _skinWidth,_collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    private void OnHitObject(RaycastHit hit)
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            damageableObject.TakeHit(_damage, hit);
        }
        
        Destroy(gameObject);
    }
    
    private void OnHitObject(Collider c)
    {
        IDamageable damageableObject = c.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            damageableObject.TakeDamage(_damage);
        }
        
        Destroy(gameObject);
    }
}
