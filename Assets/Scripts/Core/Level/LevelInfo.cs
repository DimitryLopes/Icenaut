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
    private List<ItemSpawner> itemSpawners;

    public Transform PlayerSpawnPoint => playerSpawnPoint;
    public Transform BulletContainer => bulletContainer;
    public CinemachineVirtualCamera LevelCamera => camera;

    public void ChangePlayerSpawnPoint(Transform spawnPoint)
    {
        playerSpawnPoint = spawnPoint;
    }

    public void Load()
    {
        foreach(ItemSpawner spawner in itemSpawners)
        {
            spawner.Load();
        }
    }
}
