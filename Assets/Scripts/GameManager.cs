using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.Cockpit;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    public static GameManager Instance { get => gameManager; }

    public const int TIME_TICK = 20;
    public const int AP_LIMIT = 10;
    public const int AP_DELAY_TICK = 1;
    // public const int AP_AMOUNT = 10; // 이거 왜 넣었지?

    public AbnomalPhenomenon[] apObjs;
    private Dictionary<AbnomalPhenomenon, bool> APList = new();

    public static bool isGameStart;
    public static bool isGamePause;

    public int aliveAPCount; // 탈락카운트 값
    private int apDelayTick;
    public int abcheck_success_count;

    public GameObject text;
    public Game_Start game_Start;

    private bool isMicOn;
    public bool IsMicOn { set { isMicOn = value;  GetComponent<Recorder>().TransmitEnabled = isMicOn; } }

    public void SetVoice(GameObject gameObject)
    {
        PunVoiceClient.Instance.SpeakerPrefab = gameObject;
    }

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
        GetComponent<Recorder>().TransmitEnabled = false;
        abcheck_success_count = 0; //한번 초기화
    }

    public void Start() => GameSet();

    private IEnumerator TimeTick() //틱탕 오브젝트 바뀌는기능
    {
        yield return new WaitUntil(() => GetComponent<Game_Start>().game_start_bool );
        //while(APList.Count >= AP_LIMIT && AP_LIMIT/2 > aliveAPCount)
        //{
        //    var tempObj = UnityEngine.Random.Range(0, apObjs.Length);
        //    if(!APSet(tempObj)) continue;
        //}
        while(isGameStart)
        {
            yield return new WaitUntil(() => !isGamePause);
            yield return new WaitForSeconds(TIME_TICK);
            TickUpdate();
        }
    }

    public void GameSet()
    {
        isGameStart = true;
        StartCoroutine(TimeTick());
    }

    private void TickUpdate()
    {
        if(APList.Count >= AP_LIMIT && apDelayTick++ < AP_DELAY_TICK && aliveAPCount < AP_LIMIT)
        {
            apDelayTick = 0;
            while(!APSet(UnityEngine.Random.Range(0, apObjs.Length)));
        }
    }

    // false : 활성화 실패, true : 활성화 성공
    private bool APSet(int idx) // 이상현상이 
    {
        if(APList.Count <= 0 || APList[apObjs[idx]]) return false;
        APList[apObjs[idx]] = true;
        apObjs[idx].APSet(true);
        aliveAPCount++;
        Debug.Log("AP On");
        return true;
    }

    public void APFound(AbnomalPhenomenon ap)
    {
        aliveAPCount--;
        abcheck_success_count++;
        APList[ap] = false;
    }
    

}
