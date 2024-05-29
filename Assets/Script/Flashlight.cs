using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{    
    [SerializeField] private GameObject flashlight;
    public bool on;
    public bool off;
    public bool checkflash = true;

    private void Start()
    {
        // flashlight = GameObject.FindWithTag("FlashLight");
        off = true;
        flashlight.SetActive(false);
    }

    private void Update()
    {
        if(off && Input.GetButtonDown("FlashLight") && checkflash)
        {
            Debug.Log("on");
            flashlight.SetActive(true);
            off = false;
            on = true;
            checkflash = false;
            StartCoroutine(WaitFlash());
        }
        else if( on && Input.GetButtonDown("FlashLight") && checkflash)
        {
            Debug.Log("off");
            flashlight.SetActive(false);
            on = false;
            off = true;
            checkflash = false;
            StartCoroutine(WaitFlash());

        }
    }
    IEnumerator WaitFlash()
    {
        yield return new WaitForSeconds(1.0f);
        checkflash = true;
    }
}
