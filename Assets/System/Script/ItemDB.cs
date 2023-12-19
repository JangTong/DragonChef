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
    
    private void Start() {
        // for문을 이용해 아이템 여러개 만들기, pos[]는 위치 지정, Instantiate로 fieldItemPrefab clone하는 함수
        for (int i = 1; i<11; i++) {
            GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(typeofitem[1]);
        }
    }
}
