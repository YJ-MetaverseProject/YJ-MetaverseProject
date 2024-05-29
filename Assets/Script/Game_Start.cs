using UnityEngine;

public class Game_Start : MonoBehaviour
{
    public GameObject Player;
    public GameObject map_light;
    public Timer_tick timer;
    public GameObject warning_text;
    private bool game_start_bool = false;

    [Header("�÷��̾� ���� ����Ʈ ��")]
    [SerializeField] private GameObject tutorial_Spawn_point;
    [SerializeField] private GameObject[] Random_Spawn_points;

    void Start()
    {
        // Player.transform.position = tutorial_Spawn_point.transform.position;
        Debug.Log("�÷��̾� ��ġ Ʃ�丮�� ������ �̵�");
        map_light.SetActive(true);
        warning_text.SetActive(false);
    }

    void Update()
    {
        if (timer.second == 20) // 20���϶� ������� ����
        {
            warning_text.SetActive(true);

        }

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
          

    public void Player_Random_Spawn()
    {
        int randomIndex = Random.Range(0, Random_Spawn_points.Length);
        Player.transform.position = Random_Spawn_points[randomIndex].transform.position;
        Debug.Log("�÷��̾� ��ġ " + randomIndex + " �� �̵�");
    }
}
