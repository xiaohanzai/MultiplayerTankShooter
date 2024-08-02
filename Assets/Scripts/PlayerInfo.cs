using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using TMPro;
public class PlayerInfo : NetworkBehaviour
{
    [SerializeField] private TextMeshPro nicknameDisplay;
    public NetworkVariable<FixedString32Bytes> nickname = new NetworkVariable<FixedString32Bytes>("Player Nickname", 
        NetworkVariableReadPermission.Everyone, 
        NetworkVariableWritePermission.Owner);

    public NetworkVariable<int> killCount = new NetworkVariable<int>();
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        nickname.OnValueChanged += OnNicknameChanged;
        if(IsLocalPlayer)
        {
            GameManager.Singleton.SetLocalPlayer(this);
        }
        else
        {
            nicknameDisplay.text = nickname.Value.ToString();
        }

        GameManager.Singleton.OnPlayerJoin(this);
    }
    
    public void SetNickname(string newNickname)
    {
        nickname.Value = newNickname;
    }

    private void OnNicknameChanged(FixedString32Bytes oldValue, FixedString32Bytes newValue)
    {
        if(IsServer)
        {
            //IF THERE ARE BAD WORDS IN THE NICK NAME
                //ban the player
        }

        nicknameDisplay.text = newValue.ConvertToString();

    }
}
