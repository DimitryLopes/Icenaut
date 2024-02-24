using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    [SerializeField]
    private GunType gunType;
    [SerializeField]
    private WeaponStats stats;

    private bool onCooldown;
    private int currentAmmo;
    private float reloadTimer;

    protected List<Projectile> projectiles = new List<Projectile>();

    public float ReloadTimer => reloadTimer;
    public GunType GunType => gunType;
    public WeaponStats Stats => stats;
    public int CurrentAmmo => currentAmmo;
    private bool CanShoot => onCooldown == false && currentAmmo > 0;

    public void Start()
    {
        currentAmmo = stats.MaxAmmo;
    }

    public void Shoot(Vector3 direction)
    {
        if (CanShoot)
        {
            onCooldown = true;
            StartCoroutine(ShootCooldown());
            AudioManager.Instance.PlaySFX(transform.position, stats.SFX);
            currentAmmo--;
            if (currentAmmo <= 0)
            {
                StartReload();
            }

            OnShoot(direction);
        }
    }

    protected virtual void OnShoot(Vector3 direction) { }

    private IEnumerator ShootCooldown()
    {
        float timer = 0;
        EventManager.OnWeaponShoot.Invoke(this);
        while(timer < stats.ShootDelay)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        onCooldown = false;
    }

    public void StartReload()
    {
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        EventManager.OnWeaponReloadStart.Invoke(this);
        reloadTimer = 0;
        while (reloadTimer < stats.ReloadTime)
        {
            reloadTimer += Time.deltaTime;
            yield return null;
        }
        EventManager.OnWeaponReloadFinish.Invoke(this);
        LoadWeapon();
    }

    public void LoadWeapon()
    {
        currentAmmo = stats.MaxAmmo;
        reloadTimer = 0;
    }

    protected Projectile GetAvailableProjectile()
    {
        foreach (Projectile projectile in projectiles)
        {
            if (!projectile.Active)
            {
                return projectile;
            }
        }

        Projectile newProjectile = Instantiate(Stats.ProjectilePrefab, LevelManager.Instance.CurrentLevelInfo.BulletContainer);
        projectiles.Add(newProjectile);
        return newProjectile;
    }

}

public enum GunType
{
    Sniper = 0,
    Pistol
}
