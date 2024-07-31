using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerHealth : NetworkBehaviour
{
    public NetworkVariable<int> health = new NetworkVariable<int>(3);

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        health.Value = 3;
        health.OnValueChanged += HealthChanged;
    }

    public void GetDamaged(ulong whoDamaged)
    {
        health.Value--;
        if(health.Value <= 0)
        {
            PlayerDiedRpc(whoDamaged);
            GameManager.Singleton.RespawnPlayer(this);
        }
    }

    [Rpc(SendTo.Everyone)]
    public void PlayerDiedRpc(ulong whoKilled)
    {
        GameManager.Singleton.PlayerGotKill(whoKilled);
    }

    public void HealthChanged(int oldValue, int newValue)
    {
        Debug.Log("Player" + OwnerClientId + " health was " + oldValue + " now is " + newValue);
    }
}
