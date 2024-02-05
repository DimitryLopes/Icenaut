using UnityEngine;
using UnityEngine.UI;

public class UIBulletIcon : MonoBehaviour
{
    [SerializeField]
    private Image bulletIcon;

    public bool IsActive => bulletIcon.gameObject.activeSelf;

    public void ConsumeBullet()
    {
        bulletIcon.gameObject.SetActive(false);
    }

    public void RestoreBullet()
    {
        bulletIcon.gameObject.SetActive(true);
    }
}
