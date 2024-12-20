using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float sesitivity = 500f;
    public float rotationX;
    public float rotationY;
    public Transform playerBody;
    public Transform flTf;



    // Update is called once per frame
    void Update()
    {
        float mouseMoveX = Input.GetAxis("Mouse X");
        float mouseMoveY = Input.GetAxis("Mouse Y");
        rotationY += mouseMoveX * sesitivity * Time.deltaTime;
        rotationX += mouseMoveY * sesitivity * Time.deltaTime;

        if(rotationX > 70f)
        {
            rotationX = 70f;
        }
        else if(rotationX < -70f)
        {
            rotationX = -70f;
        }

        playerBody.eulerAngles = new Vector3(0, rotationY, 0); 
        transform.eulerAngles = new Vector3(-rotationX, playerBody.eulerAngles.y, 0);
        flTf.eulerAngles = new Vector3(-rotationX, playerBody.eulerAngles.y, 0);
    }
}