using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] private float _startingHealth;
    
    protected float Health { get; private set; }
    protected bool Died { get; private set; }

    protected virtual void Start()
    {
        Health = _startingHealth;
    }

    public void TakeHit(float damage, RaycastHit hit)
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
        
        Destroy(gameObject);
    }
}
