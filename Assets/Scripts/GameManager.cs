using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    public static GameManager Instance { get => gameManager; }

    public const int TIME_TICK = 5;
    public const int AP_LIMIT = 10;
    public const int AP_DELAY_TICK = 6;
    // public const int AP_AMOUNT = 10; // 이거 왜 넣었지?

    public AbnomalPhenomenon[] apObjs;
    private Dictionary<AbnomalPhenomenon, bool> APList = new();

    public static bool isGameStart;
    public static bool isGamePause;

    private int aliveAPCount;
    private int apDelayTick;

    private void Awake()
    {
        if(gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
            return;
        }
        gameManager = this;
        foreach(var ap in apObjs) APList.Add(ap, false);
    }

    public void Start() => GameSet();

    private IEnumerator TimeTick()
    {
        while(isGameStart)
        {
            yield return new WaitForSeconds(TIME_TICK);
            yield return new WaitUntil(() => !isGamePause);
            TickUpdate();
            Debug.Log(aliveAPCount);
        }
    }

    public void GameSet()
    {
        StartCoroutine(TimeTick());
        isGameStart = true;
        while(AP_LIMIT/2 > aliveAPCount)
        {
            var temp = UnityEngine.Random.Range(0, apObjs.Length);
            if(!APSet(temp)) continue;
        }
    }

    private void TickUpdate()
    {
        if(apDelayTick++ < AP_DELAY_TICK)
        {
            apDelayTick = 0;
            while(!APSet(UnityEngine.Random.Range(0, apObjs.Length)));
        }
    }

    // false : 활성화 실패, true : 활성화 성공
    private bool APSet(int idx)
    {
        if(APList[apObjs[idx]]) return false;
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
