using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Spawn : MonoBehaviour
{
    public GameObject Player; //플레이어
    public int Random_range; //랜덤한 값 받아와 확인

    [Header("플레이어 스폰 포인트 값")]
    [SerializeField] private GameObject tutorial_Spawn_point; //튜토리얼 스폰 포인트
    [SerializeField] private GameObject Random_Spawn_point1; //랜덤 스폰 포인트1
    [SerializeField] private GameObject Random_Spawn_point2; //랜덤 스폰 포인트2
    [SerializeField] private GameObject Random_Spawn_point3; //랜덤 스폰 포인트3
    [SerializeField] private GameObject Random_Spawn_point4; //랜덤 스폰 포인트4
    [SerializeField] private GameObject Random_Spawn_point5; //랜덤 스폰 포인트5
    [SerializeField] private GameObject Random_Spawn_point6; //랜덤 스폰 포인트6


    void Start()
    {
        //게임 시작시 플레이어 위치 값을 튜토리얼 스폰 포인트 값으로 변경
        Player.transform.position = tutorial_Spawn_point.transform.position;
        Debug.Log("플레이어 위치 튜토리얼룸으로 이동");
    }


    //플레이어 게임 시작시 랜덤한 위치에 스폰
    //플레이어가 게임 시작 버튼 누를시 랜덤한 위치에 스폰
    public void Player_Random_Spawn()
    {
        Random_range = Random.Range(1, 7); //1~6까지의 랜덤한 숫자 생성
        switch (Random_range)
        {
            case 1:
                Player.transform.position = Random_Spawn_point1.transform.position;
                Debug.Log("랜덤한 숫자 " + Random_range + " 생성" + " 플레이어 위치 " + Random_range + " 로 이동");
                break;
            case 2:
                Player.transform.position = Random_Spawn_point2.transform.position;
                Debug.Log("랜덤한 숫자 " + Random_range + " 생성" + " 플레이어 위치 " + Random_range + " 로 이동");
                break;
            case 3:
                Player.transform.position = Random_Spawn_point3.transform.position;
                Debug.Log("랜덤한 숫자 " + Random_range + " 생성" + " 플레이어 위치 " + Random_range + " 로 이동");
                break;
            case 4:
                Player.transform.position = Random_Spawn_point4.transform.position;
                Debug.Log("랜덤한 숫자 " + Random_range + " 생성" + " 플레이어 위치 " + Random_range + " 로 이동");
                break;
            case 5:
                Player.transform.position = Random_Spawn_point5.transform.position;
                Debug.Log("랜덤한 숫자 " + Random_range + " 생성" + " 플레이어 위치 " + Random_range + " 로 이동");
                break;
            case 6:
                Player.transform.position = Random_Spawn_point6.transform.position;
                Debug.Log("랜덤한 숫자 " + Random_range + " 생성" + " 플레이어 위치 " + Random_range + " 로 이동");
                break;


        }

    }
}
