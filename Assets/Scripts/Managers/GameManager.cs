using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Player playerPrefab;
    [SerializeField]
    private StateMachine stateMachine;

    private Player currentPlayer;
    private CinemachineVirtualCamera mainCamera;
    private LevelManager levelManager;

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
    }

    public void OnMainMenuLoaded()
    {
        stateMachine.ChangeState(GameStates.MainMenu);
    }

    public void OnMainGameLoaded()
    {
        levelManager.GetLevelInfo();
        stateMachine.ChangeState(GameStates.OnGoing);

        currentPlayer = Instantiate(playerPrefab);
        AnimationManager.Instance.PlayerAnimationController = currentPlayer.GetComponent<AnimationController>();
        currentPlayer.transform.position = levelManager.CurrentLevelPlayerSpawnPoint.position;
        currentPlayer.EnableActing();

        mainCamera = levelManager.LevelCamera;
        mainCamera.Follow = currentPlayer.transform;
        mainCamera.LookAt = currentPlayer.transform;
    }

    public void LoadMainMenu()
    {
        stateMachine.ChangeState(GameStates.LoadingMenu);
    }

    public void LoadLevel1()
    {
        stateMachine.ChangeState(GameStates.LoadingGame);
    }
}
