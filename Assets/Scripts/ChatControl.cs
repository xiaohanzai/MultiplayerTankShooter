using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class ChatControl : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI headerMessage;
    [SerializeField] private TMP_InputField inputField;

    public void SendToChat()
    {
        if(IsServer)
        {
            ShowServerMessageRPC(inputField.text, OwnerClientId.ToString());
        }
        else
        {
            RequestMessageSentRpc(inputField.text);
        }
        
    }

    [Rpc(SendTo.Server)]
    public void RequestMessageSentRpc(string msg, RpcParams param = default)
    {
        if(msg.Contains("badword"))
        {
            msg = "I am a loser";
        }

        ShowServerMessageRPC(msg, param.Receive.SenderClientId.ToString());
    }

    [Rpc(SendTo.Everyone)]
    public void ShowServerMessageRPC(string msg, string index)
    {
        headerMessage.text += "\n" + index + ": " + msg;
    }
}
