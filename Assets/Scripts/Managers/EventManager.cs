using UnityEngine.Events;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //states
    public static UnityEvent<GameState> OnStateActivated = new UnityEvent<GameState>();
    public static UnityEvent<GameState> OnStateDeactivated = new UnityEvent<GameState>();
    public static UnityEvent OnLoadingGameFinished = new UnityEvent();
    public static UnityEvent OnLoadingMainMenuFinished = new UnityEvent();
    
    //weapons
    public static UnityEvent<BaseGun> OnWeaponReloadStart = new UnityEvent<BaseGun>();
    public static UnityEvent<BaseGun> OnWeaponReloadFinish = new UnityEvent<BaseGun>();
    public static UnityEvent<BaseGun> OnWeaponShoot = new UnityEvent<BaseGun>();
    public static UnityEvent<BaseGun> OnWeaponChanged = new UnityEvent<BaseGun>();

    //enemies
    public static UnityEvent<EnemyBase> OnEnemyDeath = new UnityEvent<EnemyBase>();

    //Player
    public static UnityEvent<Player> OnPlayerDeath = new UnityEvent<Player>();
    public static UnityEvent<Player> OnPlayerHit = new UnityEvent<Player>();
}
