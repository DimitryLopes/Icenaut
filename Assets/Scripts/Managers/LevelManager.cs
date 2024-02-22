using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    private LevelInfo currentLevelInfo;

    public static LevelManager Instance;
    public Transform CurrentLevelPlayerSpawnPoint => currentLevelInfo.PlayerSpawnPoint;
    public LevelInfo CurrentLevelInfo => currentLevelInfo;
    public CinemachineVirtualCamera LevelCamera => currentLevelInfo.LevelCamera;

    public int CurrentCheckpointID { get; set; } = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public void GetLevelInfo()
    {
        currentLevelInfo = FindObjectOfType<LevelInfo>();
        currentLevelInfo.Load();
    }

    public void ChangePlayerSpawnPointByCheckpointID(int id)
    {
        currentLevelInfo.ChangePlayerSpawnPointByCheckpointID(id);
    }

    public void ChangePlayerSpawnPoint(Checkpoint checkpoint)
    {
        currentLevelInfo.ChangePlayerSpawnPoint(checkpoint.PlayerSpawnPoint);
        CurrentCheckpointID = checkpoint.ID;
    }
}
