using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    void Start()
    {
        EventManager.OnPowerUpAcquired.AddListener(OnPowerUpAcquired);
    }

    private void OnPowerUpAcquired(PowerUp powerUp)
    {
        GameManager.Instance.CurrentPlayer.ApplyStatusChange(powerUp.Data.Speed, powerUp.Data.JumpForce, powerUp.Data.Duration, powerUp.Data.Texture);
    }
}
