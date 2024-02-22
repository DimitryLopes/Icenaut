using UnityEngine;
using Cinemachine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Player playerPrefab;
    [SerializeField]
    private StateMachine stateMachine;
    [SerializeField]
    private float playerRespawnTimer;

    private Player currentPlayer;
    private CinemachineVirtualCamera mainCamera;
    private LevelManager levelManager;

    public Player CurrentPlayer => currentPlayer;
    public CinemachineVirtualCamera MainCamera => mainCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    void Start()
    {
        stateMachine.ChangeState(GameStates.MainMenu);
        levelManager = LevelManager.Instance;
        EventManager.OnPlayerDeath.AddListener(StartPlayerRespawn);
    }

    public void OnMainMenuLoaded()
    {
        stateMachine.ChangeState(GameStates.MainMenu);
    }

    public void OnMainGameLoaded()
    {
        levelManager.GetLevelInfo();
        currentPlayer = Instantiate(playerPrefab);

        HandleSave();

        AnimationManager.Instance.PlayerAnimationController = currentPlayer.GetComponent<AnimationController>();
        currentPlayer.transform.position = levelManager.CurrentLevelPlayerSpawnPoint.position;
        currentPlayer.EnableActing();

        mainCamera = levelManager.LevelCamera;
        mainCamera.Follow = currentPlayer.transform;
        mainCamera.LookAt = currentPlayer.transform;

        stateMachine.ChangeState(GameStates.OnGoing);
    }

    private void HandleSave()
    {
        SavedData savedData = SaveManager.Instance.LoadSave();
        if (savedData != null)
        {
            if (savedData.checkPointId != 0)
            {
                levelManager.ChangePlayerSpawnPointByCheckpointID(savedData.checkPointId);
            }
            PowerUpManager.Instance.CreatePowerUpData(savedData.remainingBuffDuration, savedData.activeBuffId);
            ItemManager.Instance.ChangeItemAmount(ItemType.Coin, savedData.coins);
            ItemManager.Instance.ChangeItemAmount(ItemType.HealthPack, savedData.healthPacks);
            currentPlayer.SpawnPlayer(savedData.playerHealth);
        }
    }

    public void LoadMainMenu()
    {
        stateMachine.ChangeState(GameStates.LoadingMenu);
    }

    public void LoadLevel1()
    {
        stateMachine.ChangeState(GameStates.LoadingGame);
    }

    private void StartPlayerRespawn(Player player)
    {
        stateMachine.ChangeState(GameStates.Finished);
        StartCoroutine(PlayerRespawn());
    }

    private IEnumerator PlayerRespawn()
    {
        float timer = 0;
        while(timer < playerRespawnTimer)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        stateMachine.ChangeState(GameStates.OnGoing);
        currentPlayer.Respawn();
        UIManager.Instance.ShowPlayerUI();
        currentPlayer.transform.position = levelManager.CurrentLevelPlayerSpawnPoint.position;
    }
}
