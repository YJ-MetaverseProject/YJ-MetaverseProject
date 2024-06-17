﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Warning_text_manager : MonoBehaviour
{
    // 싱글톤 인스턴스
    private static Warning_text_manager _instance;
    public static Warning_text_manager Instance { get { return _instance; } }

    // 나머지 필드들은 여기에 있습니다
    public Timer_tick timer;
    public GameObject warning_text;
    public Game_Start game_Start;
    public GameManager GameManager;
    public TMP_Text apCountText;
    public Image warningImage;
    public float warningDisplayDuration = 2.0f;
    public int warningDisplayCount = 3;

    private CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f;
    private bool hasShownGameStartText = false;
    private bool hasShownWarningImage = false;

    private void Awake()
    {
        // 인스턴스가 설정되지 않았다면 현재 객체를 할당합니다
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            // 이미 다른 인스턴스가 존재한다면 자신을 파괴합니다
            Destroy(gameObject);
        }

        // 다른 객체에서 싱글톤을 참조할 때 파괴되지 않도록 설정합니다
        DontDestroyOnLoad(gameObject);

        // CanvasGroup을 초기화합니다
        canvasGroup = warning_text.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = warning_text.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;
        warning_text.SetActive(false);

        // AP Count 텍스트 업데이트를 위한 코루틴 시작
        StartCoroutine(UpdateAPCountText());
    }

    private void Update()
    {
        game_start_text();
    }

    public void game_start_text()
    {
        if (timer.second == 20 && !game_Start.game_start_bool && !hasShownGameStartText) //20초가 지나고 게임이 시작하지 않았을경우
        {
            warning_text.GetComponent<TMP_Text>().text = "게임이 곧 시작됩니다.\n랜덤한 위치에 스폰됩니다";
            StartCoroutine(ShowWarningText());
            hasShownGameStartText = true;
        }

        if (GameManager.aliveAPCount >= 7 && !hasShownWarningImage)
        {
            StartCoroutine(ShowWarningImageRepeatedly());
            hasShownWarningImage = true;
        }
    }

    public void warning_ment()
    {
        if (GameManager.aliveAPCount == 7)
        {
            warning_text.GetComponent<TMP_Text>().text = "경고! 이상현상 수치 위험!";
            StartCoroutine(ShowWarningText());
        }
    }

    public void end_game_ment()
    {
        warning_text.GetComponent<TMP_Text>().text = "게임이 종료되었습니다";
        StartCoroutine(ShowWarningText());
    }

    public void lose_game_ment()
    {
        warning_text.GetComponent<TMP_Text>().text = "게임에서 탈락하셨습니다";
        StartCoroutine(ShowWarningText());
    }

    public void restart_ment()
    {
        warning_text.GetComponent<TMP_Text>().text = "게임을 다시 시작합니다";
        StartCoroutine(ShowWarningText());
    }

    private IEnumerator ShowWarningText()
    {
        warning_text.SetActive(true);
        yield return StartCoroutine(FadeInText());
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(FadeOutText());
        warning_text.SetActive(false);
    }

    private IEnumerator FadeInText()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }

    private IEnumerator UpdateAPCountText()
    {
        while (true)
        {
            apCountText.text = $"현재 이상현상 수: {GameManager.aliveAPCount}";
            yield return new WaitForSeconds(0.5f); // 0.5초마다 업데이트
        }
    }

    private IEnumerator ShowWarningImageRepeatedly()
    {
        for (int i = 0; i < warningDisplayCount; i++)
        {
            warningImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(warningDisplayDuration);
            warningImage.gameObject.SetActive(false);
            if (i < warningDisplayCount - 1) // 마지막 사진 이후에는 기다리지 않음
                yield return new WaitForSeconds(3.0f); // 3초 간격으로 사진을 띄움
        }
    }
}
