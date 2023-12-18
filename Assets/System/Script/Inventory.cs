using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player에 Component함, Player의 인벤에 있는 아이템들을 List로 저장하는 스크립트라고 생각하면 쉬울듯

public class Inventory : MonoBehaviour
{
    // 싱글톤이란걸 이용해서 정적으로 거시기 하는건데 자세한건 구글링하고ㅎㅎ, 하여튼 다른 스크립트에서 Inventory (별명같이 변수이름) 형태로 Inventory.cs 참조 가능
    #region Singleton
    public static Inventory instance;
    
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    
    #endregion
    
    public delegate void OnSlotCountChange(int val);                        // delegate(자세한건 구글링ㅎㅎ) 대충 추상변수? 같은거라고 생각하면 됨, 그냥 잠깐 참조하는 휴게소같은 느낌
    public OnSlotCountChange onSlotCountChange;                             // 얘가 진짜 쓰이는 변수
    
    public delegate void OnChangeItem();                                    // 얘도 휴게소
    public OnChangeItem onChangeItem;                                       // 얘가 진짜 쓰이는 변수
    
    public List<TypeofItem> items = new List<TypeofItem>();                 // TypeofItem.cs의 데이터를 items라는 이름의 리스트로 저장하려고 생성한 변수 / 실제로는 Player의 아이템들을 저장해줌
    
    public List<TypeofItem> citems = new List<TypeofItem>();                // 얘도 items랑 같은데, COOKING UI에 저장해줌 / 다시말해서 요리할 아이템의 데이터를 저장함
    
    GameObject useitem;                                                     // InventoryUI에서 아이템을 클릭했을 때 클릭한 아이템을 저장
        
    private int slotCnt;                                                    // 슬롯 개수를 정의하는 int
    
    public int SlotCnt {
        get => slotCnt;
        set {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }
    
    public int _slotnum;
    
    public GameObject fieldItemPrefab;
    
    public bool isCutted = false;
    
    public int cooknum = 0;
    
    public int cooktime = 1;
    
    Vector3 pos;
    
    public GameObject Cook1;
    public GameObject Cook2;
    
    // Start is called before the first frame update
    void Start()
    {
        slotCnt = 36;                                                       // 슬롯 개수 선언
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool AddItem(TypeofItem _item) {                                 // 아이템을 먹을 때 items 리스트에 저장해주는 함수
        if(items.Count < slotCnt) {
            items.Add(_item);                                               // 이 라인이 List에 추가해주는 라인임
            if(onChangeItem != null)
                onChangeItem.Invoke();
            
            return true;
        }
        return false;
    }
    
    public void RemoveItem(int _index) {                                   // 아이템을 사용했을 때 호출되는 함수 / 원래는 아이템을 클릭하면 리스트에서 삭제해야하는데(사용) 우리는 클릭했을때 사용하는게 아니라 COOKING UI로 넘어가니 삭제 X
        // Debug.Log(_index);
        // items.RemoveAt(_index);
        onChangeItem.Invoke();
    }
    
    public void ReAddItem(int _index) {                                     // 인벤에서 클릭한 아이템이 COOKING UI로 넘어가는데 넘어간 아이템을 (COOKING UI에서) 다시 클릭했을 떄 인벤으로 다시 넘겨주는 함수
        // items.RemoveAt(_index);
        onChangeItem.Invoke();
    }
    
    public void AddCitem(TypeofItem _item) {                                // COOKING UI로 아이템을 넘겨주기 위한 함수, 인벤에서 아이템 클릭하면 클릭한 아이템이 citem에 저장되는 함수
        if (citems.Count == 1) {                                            // citem의 리스트 길이를 항상 1로 맞춰주기 위해 원래 데이터가 있었다면 그걸 지워주고 새로 저장
            citems.Add(_item);
            if(onChangeItem != null)
                onChangeItem.Invoke();
        }
        else if (citems.Count == 0) {
            citems.Add(_item);
        }
        else {
            return;
        }
    }
    
    public int AddCCitem(TypeofItem _item) {
        if (citems.Count < 2) {
            citems.Add(_item);
            // if (onChangeItem != null)
            //     onChangeItem.Invoke();
            
                
                return 1;
        }
        else
            return 0;
        
    }
    
    private void OnTriggerEnter(Collider collision) {                       // 바닥에 생성된 아이템을 Inventory의 items 리스트로 저장하는 함수
        if (collision.CompareTag("FieldItem")) {
            FieldItems fieldItems = collision.GetComponent<FieldItems>();
            if (AddItem(fieldItems.GetItem())) {                            // 생성된 아이템을 먹으면 당연히 그 아이템은 field에서 삭제 
                fieldItems.DestroyItem();
            }
        }
    }
    
    public void RemoveAfterCook(int _index) {
        items.RemoveAt(_index);
    }
    
    
    public void Knife(TypeofItem _item) {
        if (_item.itemtag <= 299) {
            
            float _itemtag = _item.itemtag + 100f;
            
            TypeofItem cut = ItemDB.instance.typeofitem.Find(element => element.itemtag == _itemtag);
            
            if (cooktime == 1)
                pos = Cook1.transform.position;
            else if (cooktime == 2)
                pos = Cook2.transform.position;
            else
                pos = new Vector3(0, 3, 0);
            
            GameObject go = Instantiate(fieldItemPrefab, pos, Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(cut);
            
            isCutted = true;
            
            
        }
        
        else
            Debug.Log("더이상 썰면 흔적을 알아볼 수 없을 것 같다...");
    }
    
    public void FryingPan(TypeofItem _item) {
        if (_item.itemtag <= 299) {

            float _itemtag = _item.itemtag + 1000f;
            
            TypeofItem cut = ItemDB.instance.typeofitem.Find(element => element.itemtag == _itemtag);
            
            if (cooktime == 1)
                pos = Cook1.transform.position;
            else if (cooktime == 2)
                pos = Cook2.transform.position;
            else
                pos = new Vector3(0, 3, 0);
            
            GameObject go = Instantiate(fieldItemPrefab, pos, Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(cut);
                    
            isCutted = true;
        }
        else
            Debug.Log("더이상 구우면 타버린다...");
    }
    
    public void Pot(TypeofItem _item) {
        if (_item.itemtag <= 299) {

            float _itemtag = _item.itemtag + 3000f;
            
            TypeofItem cut = ItemDB.instance.typeofitem.Find(element => element.itemtag == _itemtag);
            
            if (cooktime == 1)
                pos = Cook1.transform.position;
            else if (cooktime == 2)
                pos = Cook2.transform.position;
            else
                pos = new Vector3(0, 3, 0);
            
            GameObject go = Instantiate(fieldItemPrefab, pos, Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(cut);
                    
            isCutted = true;
        }
        
        else
            Debug.Log("더이상 끓이면 국물도 안나온다...");
    }
    
    public void Bowl(TypeofItem _item) {
        if (_item.itemtag <= 299) {
        
            float _itemtag = _item.itemtag + 5000f;
            
            TypeofItem cut = ItemDB.instance.typeofitem.Find(element => element.itemtag == _itemtag);
            
            if (cooktime == 1)
                pos = Cook1.transform.position;
            else if (cooktime == 2)
                pos = Cook2.transform.position;
            else
                pos = new Vector3(0, 3, 0);
            
            GameObject go = Instantiate(fieldItemPrefab, pos, Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(cut);
                    
            isCutted = true;
        }
        
        else
            Debug.Log("더이상 섞으면 숨이 다 죽을 것 같다...");
    }
    
    public void Deep(TypeofItem _item) {
        if (_item.itemtag <= 299) {

            float _itemtag = _item.itemtag + 7000f;
            
            TypeofItem cut = ItemDB.instance.typeofitem.Find(element => element.itemtag == _itemtag);
            
            if (cooktime == 1)
                pos = Cook1.transform.position;
            else if (cooktime == 2)
                pos = Cook2.transform.position;
            else
                pos = new Vector3(0, 3, 0);
            
            GameObject go = Instantiate(fieldItemPrefab, pos, Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(cut);
                    
            isCutted = true;
        }
        
        else
            Debug.Log("더이상 튀기면 신발도 맛 없을 것 같다...");
    }
}
