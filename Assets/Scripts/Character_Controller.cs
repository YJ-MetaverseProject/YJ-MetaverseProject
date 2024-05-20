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
    public GameObject obj_Cam_First, obj_Cam_Quarter;
    public float radius = 0f;
    public LayerMask layer;
    public Collider[] colliders;
    public GameObject OnText;
    public Collider nearObj;

    private Transform cameraTransform;

    private void Start()
    {
        GameObject otherCameraObj = GameObject.FindGameObjectWithTag("ViewCamera"); // 다른 카메라를 태그로 찾음
        cameraTransform = otherCameraObj.transform; // 다른 카메라의 Transform을 가져옴

        if (GetComponent<PhotonView>().IsMine)
        {
            obj_Cam_First.SetActive(false);
            obj_Cam_Quarter.SetActive(true);
            this.gameObject.name += "(LocalPlayer)";
        }
        else
        {
            obj_Cam_First.SetActive(false);
            obj_Cam_Quarter.SetActive(false);
            this.gameObject.name += "(OtherPlayer)";
        }
    }

    private void Update()
    {
        Vector3 centerPosition = cameraTransform.position + cameraTransform.forward * radius; // 카메라 방향으로 수정
        colliders = Physics.OverlapSphere(centerPosition, radius, layer);

        if (colliders.Length > 0)
        {
            float short_distance = Vector3.Distance(centerPosition, colliders[0].transform.position);
            foreach (Collider col in colliders)
            {
                float short_distance2 = Vector3.Distance(centerPosition, col.transform.position);
                if (short_distance > short_distance2)
                {
                    short_distance = short_distance2;
                    nearObj = col;
                }
            }
            OnText.SetActive(true);
        }
        else
        {
            OnText.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            float pos_x = Input.GetAxis("Horizontal");
            float pos_z = Input.GetAxis("Vertical");

            // Run ON&OFF
            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_Animator.SetBool("Run", true);
            }
            else
            {
                m_Animator.SetBool("Run", false);
            }

            // Camera 방향으로 이동
            Vector3 moveDirection = cameraTransform.forward * pos_z + cameraTransform.right * pos_x;
            moveDirection.y = 0; // 수직 방향은 제거
            moveDirection.Normalize();

            if (moveDirection != Vector3.zero)
            {
                // 애니메이션 방향 설정
                float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                obj_Body.transform.rotation = Quaternion.Euler(0, angle, 0);

                m_Animator.SetBool("Walk", true);
                if (m_Animator.GetBool("Run"))
                {
                    transform.Translate(moveDirection * Time.deltaTime * f_MoveSpeed * f_RunSpeed, Space.World);
                }
                else
                {
                    transform.Translate(moveDirection * Time.deltaTime * f_MoveSpeed, Space.World);
                }
            }
            else
            {
                m_Animator.SetBool("Walk", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Animator.SetTrigger("Jump");
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
