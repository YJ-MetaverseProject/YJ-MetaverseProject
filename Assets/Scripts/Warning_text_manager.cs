using Photon.Realtime;
using System.Collections;
using TMPro;
using UnityEngine;

public class Warning_text_manager : MonoBehaviour
{
    public static Warning_text_manager single;
    public Timer_tick timer;
    public GameObject warning_text;
    public Game_Start game_Start;
    public GameManager GameManager;

    private CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f; // 페이드 인/아웃 시간
    private bool hasShownGameStartText = false;

    private void Start()
    {
        canvasGroup = warning_text.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = warning_text.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;
        warning_text.SetActive(false);
    }

    private void Update()
    {
        game_start_text();
    }

    public void game_start_text()
    {
        if (timer.second == 20 && !game_Start.game_start_bool && !hasShownGameStartText) //20초가 지나고 게임이 시작하지 않았을경우
        {
            warning_text.GetComponent<TMP_Text>().text = "게임이 곧 시작됩니다.\n랜덤한 위치에 스폰됩니다.";
            StartCoroutine(ShowWarningText());
            hasShownGameStartText = true;
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

    public void end_game_ment() //게임 탈락 및 클리어
    {
        warning_text.GetComponent<TMP_Text>().text = "게임이 종료되었습니다";
        StartCoroutine(ShowWarningText());
    }

    public void lose_game_ment()
    {
        warning_text.GetComponent<TMP_Text>().text = "게임에서 탈락하셨습니다.";
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
}
