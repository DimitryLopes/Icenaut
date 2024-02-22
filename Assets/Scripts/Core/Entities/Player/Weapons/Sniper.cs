using UnityEngine;

public class Sniper : BaseGun
{
    [SerializeField]
    private VFXBurstComponent burstVFX;
    protected override void OnShoot(Vector3 direction)
    {
        Projectile projectile = GetAvailableProjectile();
        projectile.transform.position = transform.position;
        projectile.Shoot(Stats.Damage, direction);
        burstVFX.Burst();
    }
}
