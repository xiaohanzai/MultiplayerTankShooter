using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField messageCenter;
    [SerializeField] private GameObject buttonParent;
    public void Btn_StartHost()
    {
        NetworkManager.Singleton.StartHost();
        buttonParent.SetActive(false);
    }

    public void Btn_StartClient()
    {
        NetworkManager.Singleton.StartClient();
        buttonParent.SetActive(false);
    }

    public void Btn_StartServer()
    {
        NetworkManager.Singleton.StartServer();
        messageCenter.gameObject.SetActive(true);
        buttonParent.SetActive(false);
    }
}
