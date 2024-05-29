using UnityEngine;

public class Game_Start : MonoBehaviour
{
    public GameObject Player;
    public GameObject map_light;
    public Timer_tick timer;
    public GameObject warning_text;
    private bool game_start_bool = false;

    [Header("플레이어 스폰 포인트 값")]
    [SerializeField] private GameObject tutorial_Spawn_point;
    [SerializeField] private GameObject[] Random_Spawn_points;

    void Start()
    {
        Player.transform.position = tutorial_Spawn_point.transform.position;
        Debug.Log("플레이어 위치 튜토리얼 룸으로 이동");
        map_light.SetActive(true);
        warning_text.SetActive(false);
    }

    void Update()
    {
        if (timer.second == 20) // 20초일때 경고문구 띄우기
        {
            warning_text.SetActive(true);

        }

        StartGame();
    }

    void StartGame()
    {
        if (!game_start_bool && timer.second == 30)
        {
            game_start_bool = true;
            map_light.SetActive(false);
            warning_text.SetActive(false);

            Player_Random_Spawn();
        }
    }
          

    public void Player_Random_Spawn()
    {
        int randomIndex = Random.Range(0, Random_Spawn_points.Length);
        Player.transform.position = Random_Spawn_points[randomIndex].transform.position;
        Debug.Log("플레이어 위치 " + randomIndex + " 로 이동");
    }
}
