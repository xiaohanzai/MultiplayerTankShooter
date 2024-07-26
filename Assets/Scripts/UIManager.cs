using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField messageCenter;
    public void Btn_StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void Btn_StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void Btn_StartServer()
    {
        NetworkManager.Singleton.StartServer();
        messageCenter.gameObject.SetActive(true);
    }
}
