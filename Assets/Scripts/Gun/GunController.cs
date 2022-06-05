using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform _weaponHold;
    [SerializeField] private Gun _startingGun;
    
    private Gun _equippedGun;

    private void Start()
    {
        if (_startingGun)
        {
            EquipGun(_startingGun);
        }
    }

    public void EquipGun(Gun gunToEquip)
    {
        if (_equippedGun != null)
        {
            Destroy(_equippedGun.gameObject);
        }

        _equippedGun = Instantiate(gunToEquip, _weaponHold.position, _weaponHold.rotation);
        _equippedGun.transform.parent = _weaponHold;
    }

    public void Shoot()
    {
        if (_equippedGun != null)
        {
            _equippedGun.Shoot();
        }
    }
}
