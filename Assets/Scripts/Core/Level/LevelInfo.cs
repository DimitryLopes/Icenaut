using UnityEngine;
using Cinemachine;
public class LevelInfo : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera camera;
    [SerializeField]
    private Transform playerSpawnPoint;
    [SerializeField]
    private Transform bulletContainer;

    public Transform PlayerSpawnPoint => playerSpawnPoint;
    public Transform BulletContainer => bulletContainer;
    public CinemachineVirtualCamera LevelCamera => camera;

    public void ChangePlayerSpawnPoint(Transform spawnPoint)
    {
        playerSpawnPoint = spawnPoint;
    }
}
