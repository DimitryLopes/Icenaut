using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private Transform playerSpawnPoint;
    [SerializeField]
    private AudioClip checkPointSFX;
    [SerializeField]
    private int id;

    public Transform PlayerSpawnPoint => playerSpawnPoint;

    public int ID => id;
    private bool isInRange;
    private bool IsActive => LevelManager.Instance.CurrentCheckpointID == id;
    public void ActivateCheckpoint()
    {
        LevelManager.Instance.ChangePlayerSpawnPoint(this);
        AudioManager.Instance.PlaySFX(transform.position, checkPointSFX);
        UIManager.Instance.ShowUITooltip("Activated");
    }

    private void Update()
    {
        if (isInRange)
        {
            if (!IsActive)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ActivateCheckpoint();
                    SaveManager.Instance.SaveGame();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (IsActive)
            {
                UIManager.Instance.ShowUITooltip("Activated");
            }
            else
            {
                isInRange = true;
                UIManager.Instance.ShowUITooltip("Press E to save the checkpoint");
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            UIManager.Instance.HideUITooltip();

        }
    }
}