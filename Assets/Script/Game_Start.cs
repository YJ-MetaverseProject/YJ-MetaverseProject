using UnityEngine;

public class Game_Start : MonoBehaviour
{
    public GameObject Player;
    public GameObject map_light;
    public Timer_tick timer;
    public GameObject warning_text;
    private bool game_start_bool = false;

    [Header("플레이어 스폰 포인트")]
    [SerializeField] private GameObject tutorial_Spawn_point;
    [SerializeField] private GameObject[] Random_Spawn_points;

    void Start()
    {
        // Player.transform.position = tutorial_Spawn_point.transform.position;
        map_light.SetActive(true);
        warning_text.SetActive(false);
    }

    void Update()
    {
        waring_text();

        StartGame();
    }

    public void TutorialSpawn()
    {
        Player.transform.position = tutorial_Spawn_point.transform.position;
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
          
    public void waring_text()
    {
        if (timer.second == 20 && !game_start_bool) //20초가 지나고 게임이 시작하지 않았을경우
        {
            warning_text.SetActive(true);

        }
    }
    public void Player_Random_Spawn()
    {
        int randomIndex = Random.Range(0, Random_Spawn_points.Length);
        Player.transform.position = Random_Spawn_points[randomIndex].transform.position;
        Debug.Log("�÷��̾� ��ġ " + randomIndex + " �� �̵�");
    }
}
