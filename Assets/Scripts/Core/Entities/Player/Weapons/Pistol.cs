using UnityEngine;

public class Pistol : BaseGun
{
    protected override void OnShoot(Vector3 direction)
    {
        Projectile projectile = GetAvailableProjectile();
        projectile.transform.position = transform.position;
        projectile.Shoot(Stats.Damage, direction);
    }
}
