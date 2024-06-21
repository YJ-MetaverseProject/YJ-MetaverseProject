using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private static PhotonManager photonManager;
    public static PhotonManager Instance { get => photonManager; }

    public GameObject playerPrefab;

    public DefaultPool defaultPool;

    public GameObject[] managerObjs;

    private void Awake()
    {
        if (photonManager != null && photonManager != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        photonManager = this;
        DefaultPool pool = new DefaultPool();
        defaultPool = pool;
        pool.ResourceCache.Clear();
        pool.ResourceCache.Add("Player", playerPrefab);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = false;
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
        Create_Player();
        GameManager.Instance.game_Start.Player = go;
        GameManager.Instance.game_Start.TutorialSpawn();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom : " + newPlayer.NickName);
        // Create_Player(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom : " + otherPlayer.NickName);
        Remove_Player(otherPlayer);
    }

    public void Create_Player()
    {
        Debug.Log("Create Player");
        var prefab = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        prefab.layer = LayerMask.NameToLayer("OtherPlayer");
        go = prefab;
    }

    public void Remove_Player(Player player)
    {
        PhotonNetwork.DestroyPlayerObjects(player);
    }

    public void ReTry()
    { 
        foreach(var go in managerObjs) SceneManager.MoveGameObjectToScene(go, SceneManager.GetActiveScene());
        SceneManager.CreateScene("Temp");
        var ao = SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(0));
        ao.completed += (x) => {
            var loadao = SceneManager.LoadSceneAsync("main");
            loadao.completed += (x) => {
                Time.timeScale = 1;
                Create_Player();
                GameManager.Instance.game_Start.Player = go;
                GameManager.Instance.game_Start.TutorialSpawn();
            };
        };
    }
}
