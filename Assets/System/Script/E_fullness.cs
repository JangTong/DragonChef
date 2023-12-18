using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(menuName = "ItemEft/Consumable/Fullness")]
public class E_fullness : ItemEffect
{
    public int fullnessPoint = 0;
    public override bool ExecuteRole() {
        Debug.Log("FullnessAdd : " + fullnessPoint);
        return true;
    }
}
