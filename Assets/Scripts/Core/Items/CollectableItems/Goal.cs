using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            UIManager.Instance.ShowVictoryScreen();
            GameManager.Instance.CurrentPlayer.DisableActing();
        }
    }
}
