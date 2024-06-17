using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbnomalPhenomenon : MonoBehaviour
{
    // APObjs �迭 ���� (0: Non-AP ��ü, 1: AP ��ü)
    public GameObject[] APObjs; // 0:Non-AP, 1:AP

    // AP ���¸� ��Ÿ���� bool ����
    private bool isAP;

    // AP ���¸� �����ϴ� �޼���
    public void APSet(bool swh)
    {
        // isAP ������ ���޵� ���� ����
        isAP = swh;
        // isAP ���� ���� APObjs �迭�� ��ü Ȱ��ȭ ���¸� ����
        switch (isAP)
        {
            case true:
                // isAP�� true�� �� (AP ������ ��)
                // Non-AP ��ü ��Ȱ��ȭ
                APObjs[0].SetActive(false);
                // AP ��ü Ȱ��ȭ
                APObjs[1].SetActive(true);
                break;
            case false:
                // isAP�� false�� �� (Non-AP ������ ��)
                // Non-AP ��ü Ȱ��ȭ
                APObjs[0].SetActive(true);
                // AP ��ü ��Ȱ��ȭ
                APObjs[1].SetActive(false);
                break;
        }
    }

    // AP ���¸� �о���� �޼���
    public bool APReader()
    {
        // isAP�� true�� �� (AP ������ ��)
        if (isAP)
        {
            // GameManager�� �ν��Ͻ����� APFound �޼��� ȣ��, ���� ��ü(this)�� ���ڷ� ����
            GameManager.Instance.APFound(this);
            // AP ���¸� ���� (false�� ����)
            APSet(false);
            // isAP ������ false�� ����
            isAP = false;
        }
        // �ֿܼ� "AAA" �޽��� ���
        Debug.Log("AAA");
        // ���� isAP ���� ��ȯ
        return isAP;
    }
}
