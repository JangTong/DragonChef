using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float fullness, morale, courage; // NPC 스테이터스

    public bool isFull; // 포만감이 가득찼는지 확인

    void Start()
    {
        ResetStatus();
    }

    void Update()
    {
        CheckStatusIsOver();
    }


    void ResetStatus()
    {
        fullness = morale = courage = 0;
        isFull = false;
    }
    void CheckStatusIsOver()
    {
        if (fullness > 100f)
        {
            isFull = true;
            fullness = 100f;
        }
        if (morale > 100f)
        {
            isFull = true;
            morale = 100f;
        }
        if (courage > 100f)
        {
            courage = 100f;
        }
    }
}
