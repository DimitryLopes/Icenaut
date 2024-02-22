using UnityEngine;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField]
    private List<PowerUpData> powerUpDatas;
    
    private PowerUp currentPowerUp;
    public PowerUp CurrentPowerUp => currentPowerUp;

    public static PowerUpManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    void Start()
    {
        EventManager.OnPowerUpAcquired.AddListener(OnPowerUpAcquired);
        EventManager.OnPowerUpExpired.AddListener(OnPowerUpExpired);
    }

    public void CreatePowerUpData(float duration, int id)
    {
        if (id != -1)
        {
            foreach (PowerUpData data in powerUpDatas)
            {
                if (data.ID == id)
                {
                    PowerUpData newData = new PowerUpData();
                    newData.CreateCustom(data, duration);
                    ApplyPowerUp(newData);
                }
            }
        }

    }

    private void OnPowerUpAcquired(PowerUp powerUp)
    {
        currentPowerUp = powerUp;
        ApplyPowerUp(powerUp.Data);
    }

    private void ApplyPowerUp(PowerUpData data)
    {
        ClothingManager.Instance.ChangePlayerClothing(data.Texture, data.Duration);
        GameManager.Instance.CurrentPlayer.ApplyPowerUp(data);
    }

    private void OnPowerUpExpired(PowerUpData data)
    {
        if(data.ID == currentPowerUp?.Data.ID)
        {
            currentPowerUp = null;
        }
    }
}
