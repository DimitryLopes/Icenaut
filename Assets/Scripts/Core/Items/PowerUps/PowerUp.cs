using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    protected PowerUpData data;

    public PowerUpData Data => data;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            EventManager.OnPowerUpAcquired.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
