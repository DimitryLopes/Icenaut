using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    [SerializeField]
    private BaseGun pistolPrefab;
    [SerializeField]
    private BaseGun sniperPrefab;
    [SerializeField]
    private float yPosOffset;

    private void Awake()
    {
        EventManager.OnStateActivated.AddListener(OnGameStarted);
    }

    private void OnGameStarted(GameState state)
    {
        if(state.EnumID == GameStates.OnGoing)
        {
            Player player = GameManager.Instance.CurrentPlayer;
            Vector3 playerPos = player.transform.position;
            BaseGun pistol = Instantiate(pistolPrefab, player.transform);
            BaseGun sniper = Instantiate(sniperPrefab, player.transform);
            pistol.transform.position = new Vector3(playerPos.x, playerPos.y + yPosOffset, playerPos.z);
            sniper.transform.position = new Vector3(playerPos.x, playerPos.y + yPosOffset, playerPos.z);
            pistol.LoadWeapon();
            sniper.LoadWeapon();
            player.SetWeapons(pistol, sniper);
        }
    }


}
