using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public static ItemDB instance;


    private void Awake() { // Singleton
        instance = this;
    }
    
    public List<TypeofItem> typeofitem = new List<TypeofItem>(); // Item database
    
    public GameObject fieldItemPrefab;
    public Vector3[] pos;
}
