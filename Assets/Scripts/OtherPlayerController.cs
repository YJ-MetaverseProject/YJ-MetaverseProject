using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class OtherPlayerController : MonoBehaviour, IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            Vector3 pos = transform.localPosition;
            stream.Serialize(ref pos);
        }
        else
        {
            Vector3 pos = Vector3.zero;
            stream.Serialize(ref pos);  // pos gets filled-in. must be used somewhere
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
