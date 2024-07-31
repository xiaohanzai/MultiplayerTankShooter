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

    public Dictionary<ulong, int> scores = new Dictionary<ulong, int>();
    public static GameManager Singleton { get; private set; }

    private void Awake()
    {
        Singleton = this;
    }

    private void Update()
    {
        string tempText = "";
        foreach(ulong id in scores.Keys)
        {
            tempText += id + ": " + scores[id] + "\n";
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

    public void OnPlayerJoin(NetworkObject playerObject)
    {
        RepositionPlayer(playerObject);
        scores.Add(playerObject.NetworkObjectId, 0);
    }

    public void PlayerGotKill(ulong playerId)
    {
        scores[playerId]++;
    }

    public void SetLocalPlayer(PlayerInfo playerObject)
    {
        playerObject.SetNickname(nickNameInput.text);
        nickNameInput.gameObject.SetActive(false);
    }


}
