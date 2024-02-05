using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class UIGunReload : ActivatedInGame
{
    [SerializeField]
    private UIBulletIcon bulletIconPrefab;
    [SerializeField]
    private Transform iconContainer;
    [SerializeField]
    private Image fillImage;


    [SerializeField, Header("Animation")]
    private float animationDuration;
    [SerializeField]
    private float fadeInTarget;
    [SerializeField]
    private float fadeOutTarget;

    private List<UIBulletIcon> bulletIcons = new List<UIBulletIcon>();
    private BaseGun currentGun;

    private void Awake()
    {
        EventManager.OnWeaponReloadStart.AddListener(DoReloadAnimation);
        EventManager.OnWeaponReloadFinish.AddListener(FinishReloadAnimation);
        EventManager.OnWeaponChanged.AddListener(OnWeaponChanged);
        EventManager.OnWeaponShoot.AddListener(OnGunShoot);
    }

    public void OnWeaponChanged(BaseGun gun)
    {
        currentGun = gun;

        for(int i = 0; i < bulletIcons.Count; i++)
        {
            bulletIcons[i].gameObject.SetActive(false);
            bulletIcons[i].ConsumeBullet();
        }

        for (int i = 0; i < gun.Stats.MaxAmmo; i++)
        {
            if (bulletIcons.Count > i)
            {
                bulletIcons[i].gameObject.SetActive(true);
                bulletIcons[i].RestoreBullet();
            }
            else
            {
                GetAvailableIcon();
            }
        }

        if(gun.ReloadTimer == 0)
        {
            FinishReloadAnimation(gun);
        }
    }

    void Update()
    {
        if (currentGun.ReloadTimer != 0)
        {
            fillImage.fillAmount = currentGun.ReloadTimer / currentGun.Stats.ReloadTime;
        }
    }

    private void DoReloadAnimation(BaseGun gun)
    {
        if (gun == currentGun)
        {
            fillImage.fillAmount = 0;

            OnBeforeAnimation();
            fillImage.DOFade(fadeInTarget, animationDuration);
        }
    }

    private void FinishReloadAnimation(BaseGun gun)
    {
        if(gun == currentGun)
        {
            fillImage.fillAmount = 1;

            foreach(UIBulletIcon icon in bulletIcons)
            {
                icon.RestoreBullet();
            }

            OnBeforeAnimation();
            fillImage.DOFade(fadeOutTarget, animationDuration);
        }
    }

    private void OnBeforeAnimation()
    {
        if (DOTween.IsTweening(fillImage))
        {
            DOTween.Kill(fillImage);
        }
    }

    private void OnGunShoot(BaseGun gun)
    {
        if (gun == currentGun)
        {
            bulletIcons[gun.CurrentAmmo - 1].ConsumeBullet();
        }
    }

    private UIBulletIcon GetAvailableIcon()
    {
        foreach(UIBulletIcon icon in bulletIcons)
        {
            if (!icon.IsActive)
            {
                return icon;
            }
        }

        UIBulletIcon newIcon = Instantiate(bulletIconPrefab, iconContainer);
        bulletIcons.Add(newIcon);
        return newIcon;
    }

    protected override void OnGameStarted()
    {
        gameObject.SetActive(true);
    }
}
