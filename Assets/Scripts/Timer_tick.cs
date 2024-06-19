using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer_tick : MonoBehaviour
{
    [SerializeField] private TMP_Text time_text;
    [SerializeField] private float tick = 5.0f;
    public int minute;
    public int second;
    private bool isTimerRunning = false; // 타이머 실행 여부를 나타내는 변수
    private Coroutine timerCoroutine; // 타이머 코루틴을 저장할 변수

    public Button startButton; // 시작 버튼
    public Button stopButton; // 정지 버튼
    public Button resetButton; // 초기화 버튼

    void Start()
    {
        // 버튼에 대한 클릭 리스너를 설정합니다.
        startButton.onClick.AddListener(StartTimer);
        //stopButton.onClick.AddListener(StopTimer);
        //resetButton.onClick.AddListener(ResetTimer);
    }

    public void StartTimer()
    {
        Debug.Log("타이머 실행");

        // 타이머가 이미 실행 중인 경우, 중복 실행을 방지합니다.
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            // 타이머를 시작하기 전에 이전 코루틴을 중지합니다.
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
            }
            // 타이머 코루틴을 시작하고, Coroutine을 저장합니다.
            timerCoroutine = StartCoroutine(Tick());
        }
    }

    public void StopTimer()
    {
        Debug.Log("타이머 정지");

        // 타이머가 실행 중인 경우에만 정지합니다.
        if (isTimerRunning)
        {
            isTimerRunning = false;
            // 타이머 코루틴을 중지합니다.
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
            }
        }
    }

    public void ResetTimer()
    {
        Debug.Log("타이머 초기화");

        minute = 0;
        second = 0;
        Game_Start.Instance.game_start_bool = false;
        UpdateUIText();
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
            UpdateUIText();
        }
    }

    void UpdateUIText()
    {
        // 시간을 텍스트로 표시합니다.
        time_text.text = minute.ToString("00") + ":" + second.ToString("00");
    }
}
