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


    // Start is called before the first frame update
    /*private void Start()
    {
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
    }*/
    // Update is called once per frame
    private void LateUpdate()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            float pos_x = Input.GetAxis("Horizontal");
            float pos_z = Input.GetAxis("Vertical");

            //�޸��� ON&OFF
            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_Animator.SetBool("Run", true);
            }
            else
            {
                m_Animator.SetBool("Run", false);
            }

            //�ȱ� ON&OFF �� ĳ���� �̵�
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                //Debug.Log(new Vector2(pos_x, pos_z));
                if (pos_x > 0)
                {
                    if (pos_z > 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 45f, 0f);
                        //transform.Rotate(new Vector3(0f, 45f, 0f));
                    }
                    else if (pos_z < 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 135f, 0f);
                        //transform.Rotate(new Vector3(0f, 135f, 0f));
                    }
                    else
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
                        //transform.Rotate(new Vector3(0f, 90f, 0f));
                    }
                }
                else if (pos_x < 0)
                {
                    if (pos_z > 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, -45f, 0f);
                        //transform.Rotate(new Vector3(0f, -45f, 0f));
                    }
                    else if (pos_z < 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, -135f, 0f);
                        //transform.Rotate(new Vector3(0f, -135f, 0f));
                    }
                    else
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 270f, 0f);
                        //transform.Rotate(new Vector3(0f, 270f, 0f));
                    }
                }
                else
                {
                    if (pos_z > 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                        //transform.Rotate(new Vector3(0f, 0f, 0f));
                    }
                    else if (pos_z < 0)
                    {
                        obj_Body.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                        //transform.Rotate(new Vector3(0f, 45f, 0f));
                    }
                }

                m_Animator.SetBool("Walk", true);
                if (m_Animator.GetBool("Run"))
                {
                    transform.Translate(new Vector3(pos_x, 0, pos_z) * Time.deltaTime * f_MoveSpeed * f_RunSpeed);
                }
                else
                {
                    //transform.position += new Vector3(pos_x, 0, pos_z) * Time.deltaTime * f_MoveSpeed;
                    transform.Translate(new Vector3(pos_x, 0, pos_z) * Time.deltaTime * f_MoveSpeed);
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

            if (Input.GetMouseButton(1))
            {
                float rot_x = Input.GetAxis("Mouse Y");
                float rot_y = Input.GetAxis("Mouse X");
                //obj_Rotate_Horizontal.transform.eulerAngles += new Vector3(0, rot_y, 0) * f_RotateSpeed;
                transform.eulerAngles += new Vector3(0, rot_y, 0) * f_RotateSpeed;
            }
            colliders = Physics.OverlapSphere(transform.position, radius, layer);

        // 범위 안에 오브젝트가 들어오면 상호작용 문구를 출력
            if (colliders.Length > 0)
        {
            // 상호작용할 수 있는 오브젝트가 범위 안에 있을 때 UI를 활성화
            OnText.SetActive(true);
        }
        else
        {
            // 범위 안에 오브젝트가 없을 때 UI를 비활성화
            OnText.SetActive(false);
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