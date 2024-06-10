using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Spawn : MonoBehaviour
{
    public GameObject Player; //�÷��̾�
    public int Random_range; //������ �� �޾ƿ� Ȯ��

    [Header("�÷��̾� ���� ����Ʈ ��")]
    [SerializeField] private GameObject tutorial_Spawn_point; //Ʃ�丮�� ���� ����Ʈ
    [SerializeField] private GameObject Random_Spawn_point1; //���� ���� ����Ʈ1
    [SerializeField] private GameObject Random_Spawn_point2; //���� ���� ����Ʈ2
    [SerializeField] private GameObject Random_Spawn_point3; //���� ���� ����Ʈ3
    [SerializeField] private GameObject Random_Spawn_point4; //���� ���� ����Ʈ4
    [SerializeField] private GameObject Random_Spawn_point5; //���� ���� ����Ʈ5
    [SerializeField] private GameObject Random_Spawn_point6; //���� ���� ����Ʈ6


    void Start()
    {
        //���� ���۽� �÷��̾� ��ġ ���� Ʃ�丮�� ���� ����Ʈ ������ ����
        Player.transform.position = tutorial_Spawn_point.transform.position;
        Debug.Log("�÷��̾� ��ġ Ʃ�丮������� �̵�");
    }

    public void TutorialSpawn()
    {
        Player.transform.position = tutorial_Spawn_point.transform.position;
    }

    //�÷��̾� ���� ���۽� ������ ��ġ�� ����
    //�÷��̾ ���� ���� ��ư ������ ������ ��ġ�� ����
    public void Player_Random_Spawn()
    {
        Random_range = Random.Range(1, 7); //1~6������ ������ ���� ����
        switch (Random_range)
        {
            case 1:
                Player.transform.position = Random_Spawn_point1.transform.position;
                Debug.Log("������ ���� " + Random_range + " ����" + " �÷��̾� ��ġ " + Random_range + " �� �̵�");
                break;
            case 2:
                Player.transform.position = Random_Spawn_point2.transform.position;
                Debug.Log("������ ���� " + Random_range + " ����" + " �÷��̾� ��ġ " + Random_range + " �� �̵�");
                break;
            case 3:
                Player.transform.position = Random_Spawn_point3.transform.position;
                Debug.Log("������ ���� " + Random_range + " ����" + " �÷��̾� ��ġ " + Random_range + " �� �̵�");
                break;
            case 4:
                Player.transform.position = Random_Spawn_point4.transform.position;
                Debug.Log("������ ���� " + Random_range + " ����" + " �÷��̾� ��ġ " + Random_range + " �� �̵�");
                break;
            case 5:
                Player.transform.position = Random_Spawn_point5.transform.position;
                Debug.Log("������ ���� " + Random_range + " ����" + " �÷��̾� ��ġ " + Random_range + " �� �̵�");
                break;
            case 6:
                Player.transform.position = Random_Spawn_point6.transform.position;
                Debug.Log("������ ���� " + Random_range + " ����" + " �÷��̾� ��ġ " + Random_range + " �� �̵�");
                break;


        }

    }
}
