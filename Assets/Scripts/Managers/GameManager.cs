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

    public Player CurrentPlayer => currentPlayer;

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

        currentPlayer = Instantiate(playerPrefab);
        AnimationManager.Instance.PlayerAnimationController = currentPlayer.GetComponent<AnimationController>();
        currentPlayer.transform.position = levelManager.CurrentLevelPlayerSpawnPoint.position;
        currentPlayer.EnableActing();

        mainCamera = levelManager.LevelCamera;
        mainCamera.Follow = currentPlayer.transform;
        mainCamera.LookAt = currentPlayer.transform;

        stateMachine.ChangeState(GameStates.OnGoing);
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
