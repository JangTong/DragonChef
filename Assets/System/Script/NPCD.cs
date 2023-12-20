using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCD : MonoBehaviour
{
    public float fullness, morale, courage; // NPC Ω∫≈◊¿Ã≈ÕΩ∫
    
    public float _fullness, _morale, _courage;

    public bool _isFull; // ∆˜∏∏∞®¿Ã ∞°µÊ√°¥¬¡ˆ »Æ¿Œ

    void Start()
    {
        ResetStatus();
    }

    void Update()
    {
        CheckStatusIsOver();
        getPoint();
    }


    void ResetStatus()
    {
        fullness = morale = courage = 0;
        _isFull = false;
    }
    void CheckStatusIsOver()
    {
        if (fullness > 100f)
        {
           Inventory.instance.isFull = true;
            _isFull = true;
            fullness = 100f;
            ResetStatus();
        }
        if (morale > 100f)
        {
            _isFull = true;
            morale = 100f;
        }
        if (courage > 100f)
        {
            courage = 100f;
        }
    }
    
    void getPoint() {
        if (Inventory.instance.isGive == true && Inventory.instance.cooknum == 9) {
            _fullness  += Inventory.instance.pitems[0].fullness;
            _morale += Inventory.instance.pitems[0].power;
            _courage += Inventory.instance.pitems[0].efficiency;
            Inventory.instance.isGive = false;
        }
    }
    
}
