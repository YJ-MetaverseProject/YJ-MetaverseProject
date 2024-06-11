using TMPro;
using UnityEngine;

public class Game_Start : MonoBehaviour
{
    public GameObject Player;
    public GameObject map_light;
    public Timer_tick timer;
    public bool game_start_bool = false;
    public GameManager gameManager;

    [Header("플레이어 스폰 포인트")]
    public GameObject tutorial_Spawn_point;
    [SerializeField] private GameObject[] Random_Spawn_points;

    void Start()
    {
        TutorialSpawn();//게임 시작시 튜토리얼에 스폰하고
        map_light.SetActive(true); //맵에 불 켜지고
    }

    void Update()
    {
        StartGame();
    }

    public void TutorialSpawn()
    {
        Player.transform.position = tutorial_Spawn_point.transform.position;
    }

    void StartGame()
    {
        if (!game_start_bool && timer.second == 30) //만약 30초 되면 게임 시작
        {
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
        if (timer.minute == 6) //시간이 6분이 되면
        {
            Player.transform.position = tutorial_Spawn_point.transform.position; //다시 튜토리얼룸으로 옮기고
            Warning_text_manager.single.end_game_ment();

        }
        else if (timer.minute < 6 && gameManager.aliveAPCount > 10)//시간이 6분보다 작고 alive카운트가 10보다 커지면
        {
            Player.transform.position = tutorial_Spawn_point.transform.position; //다시 튜토리얼룸으로 옮기고
            Warning_text_manager.single.lose_game_ment();

        }
    }

}
