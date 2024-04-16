using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [SerializeField] private float time;
    [SerializeField] private float curTime;

    int minute;
    int second;

    private void Awake()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        curTime = time;
        while (curTime > 0)
        {
            curTime += Time.deltaTime;
            minute = (int)curTime / 60;
            second = (int)curTime % 60;
            text.text = minute.ToString("00") + ":" + second.ToString("00");
            yield return null;

            if (curTime >= 360)
            {
                Debug.Log("�ð� ����");
                curTime = 0;
                yield break;
            }
        }
    }
}
