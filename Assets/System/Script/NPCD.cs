using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCD : MonoBehaviour
{
    public float fullness, morale, courage;

    public bool isFull;

    void Start()
    {
        ResetStatus();
    }

    void Update()
    {
        CheckStatusIsOver();
        getPoint();
    }


    public void ResetStatus()
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
        if (morale > 100f) morale = 100f;
        if (courage > 100f) courage = 100f;

        if (fullness < 0) fullness = 0;
        if (morale < 0) morale = 0;
        if (courage < 0) courage = 0;
    }

    void getPoint()
    {
        if (Inventory.instance.isGive == true && Inventory.instance.cooknum == 6)
        {
            fullness += Inventory.instance.pitems[0].fullness;
            morale += Inventory.instance.pitems[0].power;
            courage += Inventory.instance.pitems[0].efficiency;
            Inventory.instance.isGive = false;
        }
    }

}
