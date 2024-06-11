using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbnomalPhenomenon : MonoBehaviour
{
    public GameObject[] APObjs; // 0:Non-AP, 1:AP
    private bool isAP;
    public bool IsAP { get { return isAP; } }

    public void APSet(bool swh)
    {
        isAP = swh;
        switch(isAP)
        {
            case true:
                APObjs[0].SetActive(false);
                APObjs[1].SetActive(true);
            break;
            case false:
                APObjs[0].SetActive(true);
                APObjs[1].SetActive(false);
            break;
        }
    }

    public bool APReader()
    { 
        if(isAP)
        {
            GameManager.Instance.APFound(this);
            APSet(false);
            isAP = false;
            Debug.Log("¤·¤·");
        }
        Debug.Log("AAA");
        return isAP;
    }
}
