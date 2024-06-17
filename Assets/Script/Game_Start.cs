using System.Collections;
using TMPro;
using UnityEngine;

public class Game_Start : MonoBehaviour
{
    // 싱글톤 인스턴스
    private static Game_Start instance;
    public static Game_Start Instance { get { return instance; } }

    public GameObject Player;
    public GameObject map_light;
    public Timer_tick timer;
    public bool game_start_bool = false;
    public GameManager gameManager;
    private Character_Controller character_Controller;
    public TMP_Text result_text;
    private bool game_end_check;

    public GameObject tutorial_Spawn_point;
    [SerializeField] private GameObject[] Random_Spawn_points;

    private bool endGameExecuted = false; // 게임 종료 처리가 한 번만 실행되도록 제어하는 변수

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        TutorialSpawn();
        map_light.SetActive(true);
        game_end_check = false;
    }

    void Update()
    {
        StartGame();

        // 게임 종료 조건 추가
        if (!game_end_check) // game_end_check가 false인 경우에만 실행
        {
            if (timer.minute < 6 && gameManager.aliveAPCount >= 10)
            {
                End_Game();
            }
            else if (timer.minute == 6)
            {
                End_Game();
            }
        }
    }

    public void TutorialSpawn()
    {
        Player.transform.position = tutorial_Spawn_point.transform.position;
    }

    void StartGame()
    {
        if (!game_start_bool && timer.second == 30)
        {
            timer.ResetTimer();// 시간 리셋해주고
            game_start_bool = true;
            map_light.SetActive(false);
            Player_Random_Spawn();
        }
    }

    public void Player_Random_Spawn()
    {
        int randomIndex = Random.Range(0, Random_Spawn_points.Length);
        Player.transform.position = Random_Spawn_points[randomIndex].transform.position;
    }

    public void End_Game()
    {
        if (!endGameExecuted)
        {
            endGameExecuted = true;
            character_Controller = Player.GetComponent<Character_Controller>(); // Player의 Character_Controller 컴포넌트를 가져옴

            // 디버깅을 위한 로그 추가
            Debug.Log($"End_Game() called - timer.minute: {timer.minute}, gameManager.aliveAPCount: {gameManager.aliveAPCount}");

            if (timer.minute == 6)
            {
                timer.StopTimer();
                Player.transform.position = tutorial_Spawn_point.transform.position;
                Warning_text_manager.Instance.end_game_ment();
                result_text.text = "클리어를 축하드립니다!\n" + "체크한 이상현상 수 : " + gameManager.abcheck_success_count.ToString() + "\n" + "실패한 이상현상 체크 수 : " + character_Controller.abcheck_fail_count.ToString();
                Debug.Log("Game cleared!");
            }
            else if (timer.minute < 6 && gameManager.aliveAPCount >= 10)
            {
                timer.StopTimer();
                Player.transform.position = tutorial_Spawn_point.transform.position;
                Warning_text_manager.Instance.lose_game_ment();
                result_text.text = "탈락입니다\n" + "탈락한 시간 : " + timer.minute.ToString() + "분" + timer.second.ToString() + "초" + "\n" + "체크한 이상현상 수 : " + gameManager.abcheck_success_count.ToString() + "\n" + "실패한 이상현상 체크 수 : " + character_Controller.abcheck_fail_count.ToString();
                Debug.Log("Game lost!");
            }

            // 게임 종료 처리가 완료되었음을 표시
            game_end_check = true;
        }
    }
}
