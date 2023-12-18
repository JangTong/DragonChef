using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Ingredients, Finisheds};

 [System.Serializable]
public class Items
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;

    public bool Use() {
        return false;
    }
}
