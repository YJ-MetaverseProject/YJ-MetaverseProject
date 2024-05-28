using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer_tick : MonoBehaviour
{
    [SerializeField] private TMP_Text time_text;
    [SerializeField] private float tick = 5.0f;
    [SerializeField] private int minute;
    [SerializeField] private int second;
    [SerializeField] private bool isTimerRunning = false;

    public void StartTimer()
    {
        // 타이머가 이미 실행 중인 경우, 중복 실행을 방지합니다.
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            // 타이머를 시작하기 전에 이전 코루틴을 중지합니다.
            StopAllCoroutines();
            // 타이머 코루틴을 시작합니다.
            StartCoroutine(Tick());
        }
    }

    IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(tick);
            second += 5;
            if (second >= 60)
            {
                minute += 1;
                second = 0;
            }
            // 시간을 텍스트로 표시합니다.
            time_text.text = minute.ToString("00") + ":" + second.ToString("00");
        }
    }


    //이전에 사용한 코드들
    //private void Awake()
    //{
    //    StartCoroutine(Tick());
    //}
    //IEnumerator Tick()
    //{

    //    while (true)
    //    {
    //        yield return new WaitForSeconds(tick);
    //        second += 5;
    //        if (second >= 60)
    //        {
    //            minute += 1;
    //            second = 0;
    //        }
    //        time_text.text = minute.ToString("00") + ":" + second.ToString("00");
    //    }
    //}
}
