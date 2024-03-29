using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "Scriptable Objects/Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    [SerializeField]
    private Projectile projectile;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float shootDelay;
    [SerializeField]
    private int ammo;
    [SerializeField]
    private float reloadTime;
    [SerializeField]
    private AudioClip sfx;

    public float ReloadTime => reloadTime;
    public int MaxAmmo => ammo;
    public AudioClip SFX => sfx;
    public float ShootDelay => shootDelay;
    public float Damage => damage;
    public Projectile ProjectilePrefab => projectile;
}
