using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float sesitivity = 500f;
    public float rotationX;
    public float rotationY;
    public Transform playerBody; // 플레이어의 Transform 참조 추가

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float mouseMoveX = Input.GetAxis("Mouse X");
        float mouseMoveY = Input.GetAxis("Mouse Y");
        rotationY += mouseMoveX * sesitivity * Time.deltaTime;
        rotationX += mouseMoveY * sesitivity * Time.deltaTime;

        if (rotationX > 30f)
        {
            rotationX = 30f;
        }
        else if (rotationX < -30f)
        {
            rotationX = -30f;
        }

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0);
        playerBody.rotation = Quaternion.Euler(0, rotationY, 0); // 플레이어의 회전을 카메라의 회전과 일치시킴
    }
}