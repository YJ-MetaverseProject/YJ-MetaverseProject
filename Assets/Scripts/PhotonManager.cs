using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public GameObject otherPrefab;

    public DefaultPool defaultPool;

    private void Awake()
    {
        DefaultPool pool = new DefaultPool();
        defaultPool = pool;
        pool.ResourceCache.Clear();
        pool.ResourceCache.Add("Player", playerPrefab);
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Start()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        RoomOptions options = new RoomOptions();
        options.PublishUserId = true;
        options.MaxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom("TEST", options, TypedLobby.Default);
    }

    private GameObject go;
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        // foreach(var player in PhotonNetwork.PlayerListOthers) Create_Player(player);
        photonView.RPC("Create_Player", RpcTarget.All);
        GameManager.Instance.player_Spawn.Player = go;
        GameManager.Instance.player_Spawn.TutorialSpawn();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom : " + newPlayer.NickName);
        // Create_Player(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom : " + otherPlayer.NickName);
        photonView.RPC("Remove_Player", RpcTarget.Others, otherPlayer);
    }

    [PunRPC]
    public void Create_Player()
    {
        Debug.Log("Create Player");
        var prefab = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        go = prefab;
    }

    [PunRPC]
    public void Remove_Player(Player player)
    {
        PhotonNetwork.DestroyPlayerObjects(player);
    }
}
