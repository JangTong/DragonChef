using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// canvas에 component함 인벤 ui를 띄우는 스크립트 + 먹은 아이템들을 Slot.cs에서의 Slot[] 형태로 (임시)저장하고, Inventory에 저장해주는 스크립트


public class InventoryUI : MonoBehaviour
{

    Inventory inven;                                                // Inventory.cs를 inven이라는 이름으로 불러옴
        
    public GameObject inventoryPanel;                               // 나중에 인벤 켜는 버튼을 눌렀을 때 인벤을 켜주기 위한 GameObject
    bool activeInventory = false;                                   // 인벤의 상태를 정의하는 bool
    
    public GameObject io;                                           // 인벤여는 버튼
    public GameObject ic;                                           // 인벤 닫는 버튼
    bool oc = false;                                                // 열고닫는 버튼의 상태를 정의하는 bool
    
    public Slot[] slots;                                            // Slot[]형태로 저장하기 위한 변수
    public Transform slotHolder;                                    // slotHolder라고 Slot objcet의 바로 위 부모로 지정하여 Slot object의 개수를 받아올거임
    
    public Slot[] cslots;
    public Transform cslotHolder;
    
    public GameObject _CSlot;
    public int _slotnum;
    
    private void Start()
    {
        inven = Inventory.instance;                                 // 대충 편의를 위한 라인
        slots = slotHolder.GetComponentsInChildren<Slot>();         // slots에 slotHolder의 자식들을 저장
        cslots = cslotHolder.GetComponentsInChildren<Slot>();       // 얘도 마찬가지안데 cslotholder는 CSlot(COOKING UI의 슬롯)5개 가져옴
        inven.onSlotCountChange += SlotChange;                      // 아이템이 할당된 슬롯들은 button을 true, 할당되지 않으면 button을 false로 지정하는 라인인데 작동안함;;;
        inven.onChangeItem += RedrawSlotUI;                         // 다시 그려주는 라인
        
        io.SetActive(!oc);                                          // 인벤여는버튼 true
        ic.SetActive(oc);                                           // 인벤닫는버튼 false
        
        inventoryPanel.SetActive(activeInventory);                  // 인벤 false
        
        // _slotnum = _CSlot.GetComponent<Slot>().slotnum;             // 위에 36번 라인이 작동하지 않아 현재는 레전드 쓸모없음
        
        
    }
    
    private void SlotChange(int val) {                              // 위에서 설명했지만 아이템이 할당된 슬롯은 button을 true, 할당되지 않으면 button을 false로 지정하는데 왜 작동 안함? 개빡침 얘하나때문에 함수가 개많이 늘어남 씻팔
        for (int i = 0; i<36; i++) {
            // Debug.Log(i);
            if (i < inven.SlotCnt) {
                slots[i].GetComponent<Button>().interactable = true;
                slots[i].slotnum = i;
            }
            else {
                slots[i].GetComponent<Button>().interactable = false;
            }
            Debug.Log($"Slot {i} interactable: {slots[i].GetComponent<Button>().interactable}");
        }
    }
    
    private void Update()
    {
        // _slotnum = _CSlot.GetComponent<Slot>().slotnum;
    }
    
    public void InvOpen() {                                         // 인벤을 열기위해 만든 함수 인벤여는버튼에 On Click()으로 지정하여 bool형태 변수들을 자기 상태의 반대 상태로 바꿔주는데 이정도는 코드 읽으면 이해 할듯
        activeInventory = !activeInventory;
        oc = !oc;
        
        inventoryPanel.SetActive(activeInventory);
        RedrawSlotUI();
        
        io.SetActive(!oc);
        ic.SetActive(oc);
    }
    
    public void AddSlot() {                                         // 슬롯 추가 함수인데 필요없을듯
        inven.SlotCnt++;
    }
    
    public void RedrawSlotUI() {                                    // 슬롯 다시 그려주는 함수인데 뭐 그냥 그런 함수임
        for (int i = 0; i < slots.Length; i++) {
            slots[i].RemoveSlot();
            slots[i].slotnum = i;
        }
        for (int i = 0; i < inven.items.Count; i++) {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
        
    }
    
    public void ReRedrawSlotUI() {                                    // 슬롯 다시 그려주는 함수인데 뭐 그냥 그런 함수임
        for (int i = 0; i < 2; i++) {
            cslots[i].RemoveSlot();
        }
        for (int i = 0; i < inven.citems.Count; i++) {
            // if (inven.citems[i] != null)
            //    cslots[i].item = inven.citems[i];
            // else
            //     cslots[i].itemicon = null;
            // cslots[i].UpdateSlotUI();
        }
        
    }
    
    public void AddCookingUI() {                         // 인벤에서 아이템 클릭하면 COOKING UI로 이동하는 함수임 그냥 / citem에 저장된 아이템데이터를 cslot에 저장하고, COOKING UI에서 cslot에 저장된 itemIcon을 true로 만드는 함수
        if (cslots[0].item.itemtag <= 0 && cslots[1].item.itemtag <= 0) {
            cslots[0].item = inven.citems[0];
            cslots[0].itemIcon.sprite = cslots[0].item.uitemimage;
            cslots[0].itemIcon.gameObject.SetActive(true);
            cslots[0].slotnum = inven._slotnum;
        }
        
        else if (cslots[0].item.itemtag > 0 && cslots[1].item.itemtag <= 0) {
            cslots[1].item = inven.citems[1];
            cslots[1].itemIcon.sprite = cslots[1].item.uitemimage;
            cslots[1].itemIcon.gameObject.SetActive(true);
            cslots[1].slotnum = inven._slotnum;
        }
        else if (cslots[0].item.itemtag <= 0 && cslots[1].item.itemtag > 0) {
            cslots[0].item = inven.citems[0];
            cslots[0].itemIcon.sprite = cslots[0].item.uitemimage;
            cslots[0].itemIcon.gameObject.SetActive(true);
            cslots[0].slotnum = inven._slotnum;
        }
        else if (cslots[0].item.itemtag > 0 && cslots[1].item.itemtag > 0) {
            return;
        }
        else
            return;
    }
    
    public void RemoveCookingUI() {                                 // COOKING UI의 아이템을 클릭하면 인벤으로 돌아가는 함수임
        if (cslots[0].item.itemtag != 0)
            cslots[0].item = null;
            
        else if (cslots[1].item.itemtag != 0)
            cslots[1].item = null;
            
        else
            return;
    }
    
    public void RemoveCookingFUI() {                                 // COOKING UI의 아이템을 클릭하면 인벤으로 돌아가는 함수임
        if (cslots[0].item.itemtag != 0) {
            // inven.citems.Add(cslots[0].item);
            // int index = Array.IndexOf(slots, slotnum);
            slots[cslots[0].slotnum].itemIcon.gameObject.SetActive(true);
            
            
            cslots[0].itemIcon.gameObject.SetActive(false);
            cslots[0].item = ItemDB.instance.typeofitem[0];
        }
            
        else
            return;
    }
    
    public void RemoveCookingSUI() {                                 // COOKING UI의 아이템을 클릭하면 인벤으로 돌아가는 함수임
        if (cslots[1].item.itemtag != 0) {
            // inven.items.Add(cslots[1].item);
            slots[cslots[1].slotnum].itemIcon.gameObject.SetActive(true);
            
            cslots[1].itemIcon.gameObject.SetActive(false);
            cslots[1].item = ItemDB.instance.typeofitem[0];
        }
        else
            return;
    }
    
    public void triggerKnife() {
        if (Inventory.instance.cooknum == 1) {
            List<TypeofItem> _citem = new List<TypeofItem>();
            
            _citem.Add(Inventory.instance.citems[0]);
            _citem.Add(Inventory.instance.citems[1]);
                           
            Inventory.instance.cooktime = 1;
            Inventory.instance.Knife(_citem[0]);
            
            Inventory.instance.cooktime = 2;
            Inventory.instance.Knife(_citem[1]);
                        
            Inventory.instance.citems.RemoveAt(0);
            Inventory.instance.RemoveAfterCook(cslots[0].slotnum);
            
            Inventory.instance.citems.RemoveAt(0);
            Inventory.instance.RemoveAfterCook(cslots[1].slotnum - 1);
            
            RemoveCookingFUI();
            RemoveCookingSUI();
            
            InvOpen();
        }
    }
    
    public void triggerFryingPan() {
        if (Inventory.instance.cooknum == 2) {
            // if ( Inventory.instance.citems[0] != null) {
                List<TypeofItem> _citem = new List<TypeofItem>();
                
                // Debug.Log(Inventory.instance.citems[0].itemtag);
                                
                _citem.Add(Inventory.instance.citems[0]);
                
                // Debug.Log(_citem[0].itemtag);
                
                _citem.Add(Inventory.instance.citems[1]);
                
                // Debug.Log(_citem[1].itemtag);
               
                Inventory.instance.cooktime = 1;
                Inventory.instance.FryingPan(_citem[0]);
                
                Inventory.instance.cooktime = 2;
                Inventory.instance.FryingPan(_citem[1]);
                
                // Debug.Log(_citem[0].itemtag);
                // Debug.Log(_citem[1].itemtag);
                
                Inventory.instance.citems.RemoveAt(0);
                Inventory.instance.citems.RemoveAt(0);
                
                Inventory.instance.RemoveAfterCook(cslots[0].slotnum);
                Inventory.instance.RemoveAfterCook(cslots[1].slotnum - 1);
                
                RemoveCookingFUI();
                RemoveCookingSUI();
                
                InvOpen();
            // }
            
        }
    }
    
    public void triggerPot() {
        if (Inventory.instance.cooknum == 3) {
            // if ( Inventory.instance.citems[0] != null) {
                List<TypeofItem> _citem = new List<TypeofItem>();
                
                // Debug.Log(Inventory.instance.citems[0].itemtag);
                                
                _citem.Add(Inventory.instance.citems[0]);
                
                // Debug.Log(_citem[0].itemtag);
                
                _citem.Add(Inventory.instance.citems[1]);
                
                // Debug.Log(_citem[1].itemtag);
               
                Inventory.instance.cooktime = 1;
                Inventory.instance.Pot(_citem[0]);
                
                Inventory.instance.cooktime = 2;
                Inventory.instance.Pot(_citem[1]);
                
                // Debug.Log(_citem[0].itemtag);
                // Debug.Log(_citem[1].itemtag);
                
                Inventory.instance.citems.RemoveAt(0);
                Inventory.instance.citems.RemoveAt(0);
                
                Inventory.instance.RemoveAfterCook(cslots[0].slotnum);
                Inventory.instance.RemoveAfterCook(cslots[1].slotnum - 1);
                
                RemoveCookingFUI();
                RemoveCookingSUI();
                
                InvOpen();
            // }
            
        }
    }
    
    public void triggerBowl() {
        if (Inventory.instance.cooknum == 4) {
            // if ( Inventory.instance.citems[0] != null) {
                List<TypeofItem> _citem = new List<TypeofItem>();
                
                // Debug.Log(Inventory.instance.citems[0].itemtag);
                                
                _citem.Add(Inventory.instance.citems[0]);
                
                // Debug.Log(_citem[0].itemtag);
                
                _citem.Add(Inventory.instance.citems[1]);
                
                // Debug.Log(_citem[1].itemtag);
               
                Inventory.instance.cooktime = 1;
                Inventory.instance.Bowl(_citem[0]);
                
                Inventory.instance.cooktime = 2;
                Inventory.instance.Bowl(_citem[1]);
                
                // Debug.Log(_citem[0].itemtag);
                // Debug.Log(_citem[1].itemtag);
                
                Inventory.instance.citems.RemoveAt(0);
                Inventory.instance.citems.RemoveAt(0);
                
                Inventory.instance.RemoveAfterCook(cslots[0].slotnum);
                Inventory.instance.RemoveAfterCook(cslots[0].slotnum - 1);
                
                RemoveCookingFUI();
                RemoveCookingSUI();
                
                InvOpen();
            // }
            
        }
    }
    
    public void triggerDeep() {
        if (Inventory.instance.cooknum == 5) {
            // if ( Inventory.instance.citems[0] != null) {
                List<TypeofItem> _citem = new List<TypeofItem>();
                
                // Debug.Log(Inventory.instance.citems[0].itemtag);
                                
                _citem.Add(Inventory.instance.citems[0]);
                
                // Debug.Log(_citem[0].itemtag);
                
                _citem.Add(Inventory.instance.citems[1]);
                
                // Debug.Log(_citem[1].itemtag);
               
                Inventory.instance.cooktime = 1;
                Inventory.instance.Deep(_citem[0]);
                
                Inventory.instance.cooktime = 2;
                Inventory.instance.Deep(_citem[1]);
                
                // Debug.Log(_citem[0].itemtag);
                // Debug.Log(_citem[1].itemtag);
                
                Inventory.instance.citems.RemoveAt(0);
                Inventory.instance.citems.RemoveAt(0);
                
                Inventory.instance.RemoveAfterCook(cslots[0].slotnum);
                Inventory.instance.RemoveAfterCook(cslots[1].slotnum - 1);
                
                RemoveCookingFUI();
                RemoveCookingSUI();
                
                InvOpen();
            // }
            
        }
    }
    
    public void triggerGive() {
        if (Inventory.instance.cooknum >= 6 && Inventory.instance.cooknum <= 9) {
            List<TypeofItem> _citem = new List<TypeofItem>();
                                        
            _citem.Add(Inventory.instance.citems[0]);
            _citem.Add(Inventory.instance.citems[1]);
                       
            Inventory.instance.cooktime = 1;
            Inventory.instance.Give(_citem[0]);
            
            Inventory.instance.cooktime = 2;
            Inventory.instance.Give(_citem[1]);
            
            Inventory.instance.citems.RemoveAt(0);
            Inventory.instance.citems.RemoveAt(0);
            
            Inventory.instance.RemoveAfterCook(cslots[0].slotnum);
            Inventory.instance.RemoveAfterCook(cslots[1].slotnum - 1);
            
            RemoveCookingFUI();
            RemoveCookingSUI();
            
            InvOpen();
            
        }
    }
    
    public void triggerLegend() {
        if (Inventory.instance.cooknum == 99) {
            Debug.Log("1");
            List<TypeofItem> _citem = new List<TypeofItem>();
            
            // Debug.Log(Inventory.instance.citems[0].itemtag);
                            
            _citem.Add(Inventory.instance.citems[0]);
            
            // Debug.Log(_citem[0].itemtag);
            
            _citem.Add(Inventory.instance.citems[1]);
            
            // Debug.Log(_citem[1].itemtag);
           
            Inventory.instance.LegendaryCook(_citem[0], _citem[1]);
            
            // Debug.Log(_citem[0].itemtag);
            // Debug.Log(_citem[1].itemtag);
            
            Inventory.instance.citems.RemoveAt(0);
            Inventory.instance.citems.RemoveAt(0);
            
            Inventory.instance.RemoveAfterCook(cslots[0].slotnum);
            Inventory.instance.RemoveAfterCook(cslots[1].slotnum - 1);
            
            RemoveCookingFUI();
            RemoveCookingSUI();
            
            InvOpen();
        }
    }

    public void OnPre()
    {
        StageController.instance.GoToNextTime();
    }

    public void NextStage()
    {
        Debug.Log("On NextStage Func, StageNum:" + StageController.instance.stageNum);
        if (StageController.instance.stageNum == 5) StageController.instance.SpawnLegends();
        else StageController.instance.OnPrepareStage();
    }
}
