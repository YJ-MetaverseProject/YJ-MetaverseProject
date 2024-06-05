using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float sesitivity = 500f;
    public float rotationX;
    public float rotationY;
<<<<<<< HEAD
    public Transform playerBody; // �÷��̾��� Transform ���� �߰�
    public Transform flTf;

=======
>>>>>>> parent of 03e8f63 (SIya)
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

<<<<<<< HEAD
        if(rotationX > 70f)
=======
        if(rotationX > 30f)
>>>>>>> parent of 03e8f63 (SIya)
        {
            rotationX = 70f;
        }
<<<<<<< HEAD
        else if(rotationX < -70f)
=======
        else if(rotationX < -30f)
>>>>>>> parent of 03e8f63 (SIya)
        {
            rotationX = -70f;
        }

<<<<<<< HEAD
        playerBody.eulerAngles = new Vector3(0, rotationY, 0); // �÷��̾��� ȸ���� ī�޶��� ȸ���� ��ġ��Ŵ
        transform.eulerAngles = new Vector3(-rotationX, playerBody.eulerAngles.y, 0);
        flTf.eulerAngles = new Vector3(-rotationX, playerBody.eulerAngles.y, 0); // �÷��̾��� ȸ���� ī�޶��� ȸ���� ��ġ��Ŵ
    }
}
=======
        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0);
    }       
}
>>>>>>> parent of 03e8f63 (SIya)
