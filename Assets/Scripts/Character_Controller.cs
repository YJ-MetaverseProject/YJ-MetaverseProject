using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Controller : MonoBehaviour
{
    public Animator m_Animator;

    [Range(0, 10f)]
    public float f_MoveSpeed;

    [Range(0, 10f)]
    public float f_RunSpeed;

    [Range(0, 100f)]
    public float f_RotateSpeed;

    public GameObject obj_Rotate_Horizontal;
    public GameObject obj_Rotate_Vertical;
    public GameObject obj_Body;
    public float radius = 0f;
    public LayerMask layer;
    public Collider[] colliders;
    public GameObject OnText;
    public Collider nearObj;
    public float sensitivity = 300f;
    public float rotationX;
    public float rotationY;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 입력을 이용한 캐릭터 회전
        float mouseMoveX = Input.GetAxis("Mouse X");
        float mouseMoveY = Input.GetAxis("Mouse Y");
        rotationY += mouseMoveX * sensitivity * Time.deltaTime;
        rotationX += mouseMoveY * sensitivity * Time.deltaTime;

        if (rotationX > 35f)
        {
            rotationX = 35f;
        }
        if (rotationX < -30f)
        {
            rotationX = -30f;
        }

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0);

        Vector3 centerPosition = Camera.main.transform.position + Camera.main.transform.forward * radius;
        colliders = Physics.OverlapSphere(centerPosition, radius, layer);
        Debug.Log("Number of colliders detected: " + colliders.Length);

        // 초기화
        nearObj = null;

        if (colliders.Length > 0)
        {
            float shortestDistance = float.MaxValue; // 최대값으로 초기화
            foreach (Collider col in colliders)
            {
                float distance = Vector3.Distance(centerPosition, col.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearObj = col;
                }
            }
            OnText.SetActive(true);
        }
        else
        {
            nearObj = null;
            OnText.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            float pos_x = Input.GetAxis("Horizontal");
            float pos_z = Input.GetAxis("Vertical");

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                // Debug.Log(new Vector2(pos_x, pos_z));
                if (pos_x > 0)
                {
                    if (pos_z > 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 45f, 0f);
                    }
                    else if (pos_z < 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 135f, 0f);
                    }
                    else
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
                    }
                }
                else if (pos_x < 0)
                {
                    if (pos_z > 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, -45f, 0f);
                    }
                    else if (pos_z < 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, -135f, 0f);
                    }
                    else
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 270f, 0f);
                    }
                }
                else
                {
                    if (pos_z > 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                    }
                    else if (pos_z < 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                    }
                }

                m_Animator.SetBool("Walk", true);
                // Rigidbody를 사용하여 이동
                Vector3 movement = new Vector3(pos_x, 0, pos_z) * f_MoveSpeed * Time.deltaTime;
                rb.MovePosition(rb.position + transform.TransformDirection(movement));
            }
            else
            {
                m_Animator.SetBool("Walk", false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 desiredPosition = new Vector3(0.0f, 1.0f, 0.0f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(desiredPosition, radius);
    }
}