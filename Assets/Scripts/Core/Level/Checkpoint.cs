using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private Transform playerSpawnPoint;

    public Transform PlayerSpawnPoint => playerSpawnPoint;

    private bool isInRange;
    private bool IsActive => LevelManager.Instance.CurrentCheckpoint == this;
    public void ActivateCheckpoint()
    {
        LevelManager.Instance.ChangePlayerSpawnPoint(this);
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