using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Warning_text_manager : MonoBehaviour
{
    public static Warning_text_manager single;
    public Timer_tick timer;
    public GameObject warning_text;
    public Game_Start game_Start;
    public GameManager GameManager;
    public TMP_Text apCountText; // 추가된 텍스트 요소
    public Image warningImage; // 추가된 이미지 요소
    public float warningDisplayDuration = 2.0f; // 경고 이미지 표시 기간
    public int warningDisplayCount = 3; // 경고 이미지 표시 횟수

    private CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f; // 페이드 인/아웃 시간
    private bool hasShownGameStartText = false;
    private bool hasShownWarningImage = false; // 이미지가 띄워졌는지 여부를 나타내는 플래그

    private void Start()
    {
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
            hasShownWarningImage = true; // 이미지가 띄워진 후에는 다시 띄우지 않도록 플래그 설정
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