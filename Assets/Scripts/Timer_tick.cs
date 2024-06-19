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
    private bool isTimerRunning = false; // Ÿ�̸� ���� ���θ� ��Ÿ���� ����
    private Coroutine timerCoroutine; // Ÿ�̸� �ڷ�ƾ�� ������ ����

    public Button startButton; // ���� ��ư
    public Button stopButton; // ���� ��ư
    public Button resetButton; // �ʱ�ȭ ��ư

    void Start()
    {
        // ��ư�� ���� Ŭ�� �����ʸ� �����մϴ�.
        startButton.onClick.AddListener(StartTimer);
        //stopButton.onClick.AddListener(StopTimer);
        //resetButton.onClick.AddListener(ResetTimer);
    }

    public void StartTimer()
    {
        Debug.Log("Ÿ�̸� ����");

        // Ÿ�̸Ӱ� �̹� ���� ���� ���, �ߺ� ������ �����մϴ�.
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            // Ÿ�̸Ӹ� �����ϱ� ���� ���� �ڷ�ƾ�� �����մϴ�.
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
            }
            // Ÿ�̸� �ڷ�ƾ�� �����ϰ�, Coroutine�� �����մϴ�.
            timerCoroutine = StartCoroutine(Tick());
        }
    }

    public void StopTimer()
    {
        Debug.Log("Ÿ�̸� ����");

        // Ÿ�̸Ӱ� ���� ���� ��쿡�� �����մϴ�.
        if (isTimerRunning)
        {
            isTimerRunning = false;
            // Ÿ�̸� �ڷ�ƾ�� �����մϴ�.
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
            }
        }
    }

    public void ResetTimer()
    {
        Debug.Log("Ÿ�̸� �ʱ�ȭ");

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
        // �ð��� �ؽ�Ʈ�� ǥ���մϴ�.
        time_text.text = minute.ToString("00") + ":" + second.ToString("00");
    }
}
