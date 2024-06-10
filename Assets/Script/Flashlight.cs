using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Flashlight : MonoBehaviour
{    
    [SerializeField] private GameObject flashlight;
    public bool isOn;
    public bool checkflash = true;

    private void Start()
    {
        // flashlight = GameObject.FindWithTag("FlashLight");
        flashlight.SetActive(false);
        StartCoroutine(WaitFlash());
    }

    private void Update()
    {
        if(!GetComponent<PhotonView>().IsMine) return;
        if(checkflash && Input.GetButtonDown("FlashLight")) GetComponent<PhotonView>().RPC("OnOff", RpcTarget.All, new object[]{!isOn, GetComponent<PhotonView>().ViewID});
        // if(off && Input.GetButtonDown("FlashLight") && checkflash)
        // {
        //     Debug.Log("on");
        //     flashlight.SetActive(true);
        //     off = false;
        //     on = true;
        //     checkflash = false;
        //     StartCoroutine(WaitFlash());
        // }
        // else if( on && Input.GetButtonDown("FlashLight") && checkflash)
        // {
        //     Debug.Log("off");
        //     flashlight.SetActive(false);
        //     on = false;
        //     off = true;
        //     checkflash = false;
        //     StartCoroutine(WaitFlash());

        // }
    }

    [PunRPC]
    public void OnOff(object[] objects)
    {
        if((int)objects[1] != GetComponent<PhotonView>().ViewID) return;
        checkflash = false;
        isOn = (bool)objects[0];
        flashlight.SetActive((bool)objects[0]);
    }

    IEnumerator WaitFlash()
    {
        while (true)
        {     
            yield return new WaitForSeconds(1.0f);
            checkflash = true;
            yield return new WaitUntil(() => !checkflash);
        }
    }
}
