using UnityEngine;
using Cinemachine;
public class LevelInfo : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera camera;
    [SerializeField]
    private Transform playerSpawnPoint;

    public Transform PlayerSpawnPoint => playerSpawnPoint;
    public CinemachineVirtualCamera LevelCamera => camera;
}
