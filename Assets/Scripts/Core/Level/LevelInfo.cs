using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
public class LevelInfo : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera camera;
    [SerializeField]
    private Transform playerSpawnPoint;
    [SerializeField]
    private Transform bulletContainer;
    [SerializeField]
    private Transform particlesContainer;
    [SerializeField]
    private List<ItemSpawner> itemSpawners;
    [SerializeField]
    private List<Checkpoint> checkpoints;

    public Transform PlayerSpawnPoint => playerSpawnPoint;
    public Transform BulletContainer => bulletContainer;
    public Transform ParticlesContainer => particlesContainer;
    public CinemachineVirtualCamera LevelCamera => camera;

    public void ChangePlayerSpawnPoint(Transform spawnPoint)
    {
        playerSpawnPoint = spawnPoint;
    }

    public void ChangePlayerSpawnPointByCheckpointID(int checkpointID)
    {
        foreach(Checkpoint checkpoint in checkpoints)
        {
            if(checkpoint.ID == checkpointID)
            {
                ChangePlayerSpawnPoint(checkpoint.PlayerSpawnPoint);
            }
        }
    }

    public void Load()
    {
        foreach(ItemSpawner spawner in itemSpawners)
        {
            spawner.Load();
        }
    }
}
