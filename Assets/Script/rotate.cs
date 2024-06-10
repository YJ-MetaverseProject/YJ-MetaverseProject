using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class rotate : MonoBehaviour
{
    void Start()
    {
        
    }

    private float speed = 3f;

    void Update()
    {
        if(!GetComponent<PhotonView>().IsMine) return;
        transform.Rotate(0f, Input.GetAxis("Mouse X") * speed, 0f, Space.World);
    }
}
