using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreBoard;
    [SerializeField] private TMP_InputField nickNameInput;
    [SerializeField] private Transform[] spawnPosition;

    private List<PlayerInfo> connectedPlayers = new List<PlayerInfo>();
    public Dictionary<ulong, int> scores = new Dictionary<ulong, int>();
    public static GameManager Singleton { get; private set; }

    private void Awake()
    {
        Singleton = this;
        
    }

    private void Update()
    {

    }

    public void OnPlayerJoin(PlayerInfo playerObject)
    {
        RepositionPlayer(playerObject.NetworkObject);
        connectedPlayers.Add(playerObject);
        playerObject.killCount.OnValueChanged += OnKillCountUpdate;

        if (NetworkManager.IsServer)
        {
            playerObject.killCount.Value = 0;
        }

        OnKillCountUpdate(0, 0);

    }

    private void OnKillCountUpdate(int previousNumber, int currentNumber)
    {
        string tempText = "";

        foreach (PlayerInfo p in connectedPlayers)
        {
            tempText += p.nickname.Value.ToString() + ": " + p.killCount.Value + "\n";
        }

        scoreBoard.text = tempText;
    }

    private void RepositionPlayer(NetworkObject playerObject)
    {
        
        Transform spawnPoint = spawnPosition[Random.Range(0, spawnPosition.Length)];
        playerObject.transform.position = spawnPoint.position;
        playerObject.transform.rotation = spawnPoint.rotation;
    }

    public void RespawnPlayer(PlayerHealth health)
    {
        health.health.Value = 3;
        RepositionPlayer(health.NetworkObject);
    }


    public void PlayerGotKill(ulong playerId)
    {
        NetworkManager.ConnectedClients[playerId].PlayerObject.GetComponent<PlayerInfo>().killCount.Value++;
    }

    public void SetLocalPlayer(PlayerInfo playerObject)
    {
        playerObject.SetNickname(nickNameInput.text);
        nickNameInput.gameObject.SetActive(false);
    }


}
