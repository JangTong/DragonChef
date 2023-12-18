using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public TypeofItem item;
    public SpriteRenderer image;
    public List<ItemEffect> effects;
    
    // 필드에 아이템을 생성하는 함수
    public void SetItem(TypeofItem _item) {
        item.uname = _item.uname;               // 전달받은 Item으로 현재 클래스의 Item 초기화
        item.utag = _item.utag;
        item.itemtag = _item.itemtag;
        item.fullness = _item.fullness;
        item.power = _item.power;
        item.efficiency = _item.efficiency;
        item.freshness = _item.freshness;
        item.uitemimage = _item.uitemimage;
        
        item.origin = _item.origin;
        item.rank = _item.rank;
        // item.ctime = _item.ctime;
        item.bigc = _item.bigc;
        // item.finishedtype = _item.finishedtype;
        
        image.sprite = _item.uitemimage;         // 아이템에 맞게 스프라이트 변경
    }
    
    public bool Use() {
        bool isUsed = false;
        foreach (ItemEffect effect in effects) {
            isUsed = effect.ExecuteRole();
        }
        
        return isUsed;
    }
    
    public TypeofItem GetItem() {
        return item;
    }
    
    public void DestroyItem() {
        Destroy(gameObject);
    }
}
