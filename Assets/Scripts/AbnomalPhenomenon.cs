using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbnomalPhenomenon : MonoBehaviour
{
    // APObjs 배열 선언 (0: Non-AP 객체, 1: AP 객체)
    public GameObject[] APObjs; // 0:Non-AP, 1:AP

    // AP 상태를 나타내는 bool 변수
    private bool isAP;

    // AP 상태를 설정하는 메서드
    public void APSet(bool swh)
    {
        // isAP 변수에 전달된 값을 설정
        isAP = swh;
        // isAP 값에 따라 APObjs 배열의 객체 활성화 상태를 변경
        switch (isAP)
        {
            case true:
                // isAP가 true일 때 (AP 상태일 때)
                // Non-AP 객체 비활성화
                APObjs[0].SetActive(false);
                // AP 객체 활성화
                APObjs[1].SetActive(true);
                break;
            case false:
                // isAP가 false일 때 (Non-AP 상태일 때)
                // Non-AP 객체 활성화
                APObjs[0].SetActive(true);
                // AP 객체 비활성화
                APObjs[1].SetActive(false);
                break;
        }
    }

    // AP 상태를 읽어오는 메서드
    public bool APReader()
    {
        // isAP가 true일 때 (AP 상태일 때)
        if (isAP)
        {
            // GameManager의 인스턴스에서 APFound 메서드 호출, 현재 객체(this)를 인자로 전달
            GameManager.Instance.APFound(this);
            // AP 상태를 변경 (false로 설정)
            APSet(false);
            // isAP 변수를 false로 설정
            isAP = false;
        }
        // 콘솔에 "AAA" 메시지 출력
        Debug.Log("AAA");
        // 현재 isAP 값을 반환
        return isAP;
    }
}
