using Photon.Pun;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngineInternal;

public class Character_Controller : MonoBehaviour
{
    public Animator m_Animator;

    [Range(0, 10f)]
    public float f_MoveSpeed;

    [Range(0, 10f)]
    public float f_RunSpeed;

    [Range(0, 100f)]
    public float f_RotateSpeed;

    public bool isMicOn;

    public GameObject obj_Rotate_Horizontal;
    public GameObject obj_Rotate_Vertical;
    public GameObject obj_Body;
    public GameObject obj_Cam_First, obj_Cam_Quarter;
    public float radius = 0f;
    public LayerMask layer;
    public Collider[] colliders;
    public GameObject OnText;
    public Collider nearObj;

    public Transform cameraTransform;
    public GameObject walkAudio;
    public GameObject runAudio;
    

    private void Start()
    {
        // GameObject otherCameraObj = GameObject.FindGameObjectWithTag("ViewCamera"); // 다른 카메라를 태그로 찾음
        // cameraTransform = otherCameraObj.transform; // 다른 카메라의 Transform을 가져옴

        OnText = GameManager.Instance.text;

        if(GetComponent<PhotonView>().IsMine)
        {
            cameraTransform.gameObject.SetActive(true);
            GameManager.Instance.SetVoice(gameObject);
            GetComponent<AudioSource>().enabled = false;
        }
        else
        {
            cameraTransform.gameObject.SetActive(false);
            GetComponent<Rigidbody>().isKinematic = true;
            ChangeLayersRecursively(transform, "OtherPlayer");
            void ChangeLayersRecursively(Transform trans, string name)
            {
                trans.gameObject.layer = LayerMask.NameToLayer(name);
                foreach (Transform child in trans)
                {
                    ChangeLayersRecursively(child, name);
                }
            }
        }
    }

    private void Update()
    {
        if(!GetComponent<PhotonView>().IsMine) return;

        if(Input.GetButtonDown("Mic")) 
        {
            isMicOn = !isMicOn;
            GameManager.Instance.IsMicOn = isMicOn;
        }

        character_ray_shot();


        Cursor.lockState = CursorLockMode.Locked;

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
                runAudio.SetActive(true);
                walkAudio.SetActive(false);
            }
            else
            {
                m_Animator.SetBool("Run", false);
                runAudio.SetActive(false);
                walkAudio.SetActive(true);
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
                walkAudio.SetActive(true);
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
                walkAudio.SetActive(false);
            }

            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     m_Animator.SetTrigger("Jump");
            // }
        }
    }

    private void OnDrawGizmos()
    {
        // 플레이어의 현재 위치를 기준으로 Gizmos를 그립니다.
        Vector3 desiredPosition = transform.position + new Vector3(0.0f, 1.0f, 0.0f); // 원하는 높이로 오프셋 적용
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(desiredPosition, radius);
    }

    public void character_ray_shot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject otherCameraObj = GameObject.FindGameObjectWithTag("ViewCamera"); // 다른 카메라를 태그로 찾음

            // 다른 카메라를 가지고 있는 게임 오브젝트에서 카메라 컴포넌트를 찾습니다.
            Camera otherCamera = otherCameraObj.GetComponent<Camera>();

            Ray ray = otherCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            // Raycast 거리를 Gizmos 구의 반지름 내로 조정합니다.
            float raycastDistance = radius; // Gizmos 구의 반지름 사용

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                print("raycast hit!");
                Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red, 5f);
                Debug.Log(hit.collider.gameObject.name);

                // 충돌한 객체가 버튼이라면
                if (hit.collider.CompareTag("Button"))
                {
                    // 버튼 클릭 이벤트 실행
                    Button button = hit.collider.GetComponent<Button>();
                    Debug.Log("레이저가 버튼에 닿음");

                    if (button != null)
                    {
                        button.onClick.Invoke();
                        Debug.Log("해당 버튼 코드 실행");
                    }
                }
                // 충돌한 객체가 Door라면
                if (hit.collider.CompareTag("Door"))
                {
                    // Door 코드 실행
                    Door door = hit.collider.GetComponent<Door>();
                    // door 값이 null이 아닐 때
                    if (door != null)
                    {
                        Debug.Log(door.DoorState);
                        // door의 상태가 false면 true로 바꾸면서 문이 열림
                        if (!door.DoorState)
                        {
                            door.Open();
                            Debug.Log("문이 열림");
                        }
                        // door의 상태가 true면 false로 바꾸면서 문이 닫힘
                        else
                        {
                            door.Close();
                            Debug.Log("문이 닫힘");
                        }
                    }
                }
                // 충돌한 객체가 Error 태그를 가지고 있다면
                if (hit.collider.CompareTag("Error"))
                {
                    // 이상현상 값을 감소시킵니다.
                    GameManager.Instance.APFound(hit.collider.GetComponent<AbnomalPhenomenon>());
                    Debug.Log("Error 태그를 가진 오브젝트를 맞춤. 이상현상 -1.");
                }
            }
        }
    }
}
