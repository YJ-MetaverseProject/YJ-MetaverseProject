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
        // Ÿ�̸Ӱ� �̹� ���� ���� ���, �ߺ� ������ �����մϴ�.
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            // Ÿ�̸Ӹ� �����ϱ� ���� ���� �ڷ�ƾ�� �����մϴ�.
            StopAllCoroutines();
            // Ÿ�̸� �ڷ�ƾ�� �����մϴ�.
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
            // �ð��� �ؽ�Ʈ�� ǥ���մϴ�.
            time_text.text = minute.ToString("00") + ":" + second.ToString("00");
        }
    }


    //������ ����� �ڵ��
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
