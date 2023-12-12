using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    private LevelInfo currentLevelInfo;

    public static LevelManager Instance;
    public Transform CurrentLevelPlayerSpawnPoint => currentLevelInfo.PlayerSpawnPoint;
    public CinemachineVirtualCamera LevelCamera => currentLevelInfo.LevelCamera;

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
    }
}
