using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    public static GameManager Instance { get => gameManager; }

    public const int TIME_TICK = 5;
    public const int AP_LIMIT = 10;
    public const int AP_DELAY_TICK = 1;
    // public const int AP_AMOUNT = 10; // 이거 왜 넣었지?

    public AbnomalPhenomenon[] apObjs;
    private Dictionary<AbnomalPhenomenon, bool> APList = new();

    public static bool isGameStart;
    public static bool isGamePause;

    private int aliveAPCount; // 탈락카운트 값
    private int apDelayTick;

    public GameObject text;
    public Game_Start game_Start;

    private void Awake()
    {
        if(gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
            return;
        }
        gameManager = this;
        foreach(var ap in apObjs) APList.Add(ap, false);
        game_Start = GetComponent<Game_Start>();
    }

    public void Start() => GameSet();

    private IEnumerator TimeTick() //틱탕 오브젝트 바뀌는기능
    {
        while(isGameStart)
        {
            yield return new WaitForSeconds(TIME_TICK);
            // yield return new WaitUntil(() => !isGamePause);
            TickUpdate();
        }
    }

    public void GameSet()
    {
        isGameStart = true;
        StartCoroutine(TimeTick());
        while(APList.Count > 0 && AP_LIMIT/2 > aliveAPCount)
        {
            var temp = UnityEngine.Random.Range(0, apObjs.Length);
            if(!APSet(temp)) continue;
        }
    }

    private void TickUpdate()
    {
        if(APList.Count > 0 && apDelayTick++ < AP_DELAY_TICK && aliveAPCount < AP_LIMIT)
        {
            apDelayTick = 0;
            while(!APSet(UnityEngine.Random.Range(0, apObjs.Length)));
        }
    }

    // false : 활성화 실패, true : 활성화 성공
    private bool APSet(int idx) // 이상현상이 
    {
        if(APList.Count > 0 || APList[apObjs[idx]]) return false;
        APList[apObjs[idx]] = true;
        apObjs[idx].APSet(true);
        aliveAPCount++;
        return true;
    }

    public void APFound(AbnomalPhenomenon ap)
    {
        aliveAPCount--;
        APList[ap] = false;
    }
}
