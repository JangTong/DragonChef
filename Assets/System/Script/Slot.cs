using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// InventoryUI의 슬롯들을 만들어주는 스크립트, Canvas의 최하위 자식들인 Slot에 Component함 

public class Slot : MonoBehaviour
{
    public int slotnum;                                 // 슬롯들의 숫자들을 지정하는 변수인데 이거 지금 작동안함 이거때문에 개고생함 Tlqkf
    public TypeofItem item;                             // TypeofItem.cs에서 지정한 아이템의 상태들(정보들)을 저장하는 변수
    public Image itemIcon;                              // 추가된 아이템의 sprite를 저장하는 Image형태의 변수
                
    public void Start() {
        
    }

    public void UpdateSlotUI() {                        // 슬롯 UI를 다시 그려주는 함수
        itemIcon.sprite = item.uitemimage;              // 아이템의 uitemimage에 저장된 sprite를 itemIcon의 sprite에 저장
        itemIcon.gameObject.SetActive(true);            // 저장한 itemIcon을 켜줌 - 그러면 보이겠죠?
    }
    
    public void RemoveSlot() {                          // 아이템이 할당되지 않은 슬롯을 꺼줌
        item = null;
        itemIcon.gameObject.SetActive(false);
    }
    
    public void ClickSlot() {                           // 슬롯을 클릭하면(아이템을 사용하면) 클릭한 슬롯에 저장된 아이템 데이터를 Inventory.cs의 AddCitem()함수를 이용해 citem에 저장
        if (item.itemtag != 0) {                        // 예외처리를 위한 if문
            bool isUse = item.Use();
            if (isUse) {
                int doornot;
                
                // Inventory.instance.AddCCitem(item);
                
                doornot = Inventory.instance.AddCCitem(item);
                
                // Inventory.instance.RemoveItem(slotnum); // 클릭한 슬롯의 item 데이터를 지워주는 라인인데 필요 없을듯?
                
                if (doornot == 1) {
                    // Inventory.instance.RemoveItem(slotnum);
                    itemIcon.gameObject.SetActive(false);   // 클릭한 슬롯의 itemIcon을 꺼주는 라인, 플레이해보면 아이템이 COOKING UI로 넘어간거처럼 보이죠
                    Inventory.instance._slotnum = slotnum;
                }
                else {
                    return;
                }
            }
        }
        else
            return;
    }
    
    public void ClickCSlot() {                          // COOKING UI의 슬롯을 클릭했을 때, citem의 값을 지워주는 함수
        if (item.utag != null) {                        // 예외처리
            bool isUse = item.Use();
            if (isUse) {
                itemIcon.gameObject.SetActive(false);
                if (Inventory.instance.citems[0].utag != "") // 애도 예외처리긴 한데 아직 이상함 
                    Inventory.instance.citems.RemoveAt(0);
                else
                    return;
            }
        }
        else
            return;
    }

}
