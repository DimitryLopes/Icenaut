using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    protected PowerUpData data;
    [SerializeField]
    private VFXBurstComponent particles;

    public PowerUpData Data => data;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            particles.transform.SetParent(LevelManager.Instance.CurrentLevelInfo.ParticlesContainer);
            particles.Burst();
            EventManager.OnPowerUpAcquired.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
