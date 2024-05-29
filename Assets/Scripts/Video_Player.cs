using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Video_Player : MonoBehaviour
{
    public VideoPlayer video_Player;

    public void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Clicked_Play()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GetComponent<PhotonView>().RPC("PlayVideo", RpcTarget.All);
        }
    }

    public void Clicked_Pause()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GetComponent<PhotonView>().RPC("PauseVideo", RpcTarget.All);
        }
    }

    [PunRPC]
    public void PlayVideo()
    {
        if (!video_Player.isPlaying)
        {
            video_Player.Play();
        }
    }

    [PunRPC]
    public void PauseVideo()
    {
        if (!video_Player.isPaused)
        {
            video_Player.Pause();
        }
    }
}