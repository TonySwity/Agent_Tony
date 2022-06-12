using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] private float _startingHealth;

    protected float Health { get; private set; }
    protected bool Died { get; private set; }

    public event UnityAction OnDeath;

    protected virtual void Start()
    {
        Health = _startingHealth;
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0 && !Died)
        {
            Die();
        }
    }

    private void Die()
    {
        Died = true;

        OnDeath?.Invoke();

        Destroy(gameObject);
    }
}