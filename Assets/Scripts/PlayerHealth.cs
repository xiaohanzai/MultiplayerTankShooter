using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class PlayerHealth : NetworkBehaviour
{
    [SerializeField] private TextMeshPro _healthText;
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
            //PlayerDiedRpc(whoDamaged);
            GameManager.Singleton.PlayerGotKill(whoDamaged);
            GameManager.Singleton.RespawnPlayer(this);
        }
    }


    public void HealthChanged(int oldValue, int newValue)
    {
        string healthTxt = "";
        for(int i = 0; i < newValue; i++)
        {
            healthTxt += "I";
        }

        _healthText.text = healthTxt;

        Debug.Log("Player" + OwnerClientId + " health was " + oldValue + " now is " + newValue);
    }
}
