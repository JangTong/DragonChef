using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.Text;


// 이이템의 정보들을 저장할 수 있도록 변수들 정의한 스크립트, Inventory, InventoryUI, Slot, ItemEffect, ItemDB, Fullness 등에 사용될걸?


// 원산지
public enum Origins { Stage0, Stage1, Stage2, Stage3, Stage4, Stage5, ETC1, ETC2 };

// 스프라이트 세분화 범주 1
public enum Category1 { Meat, Vege, Seaf, Bug, ETC1, ETC2 };

// 아이템 등급
public enum Rank { Common, Rare, Special, Magical, Legendary, ETC };

// 재료 썬 횟수
public enum Cuts { NO, Once, Twice};

// 요리 접미사
public enum Finished { Ingredient, Stirfry, Soup, Salad, Deepfry };

 [System.Serializable]
public class TypeofItem
{
    public Origins origin;                                                  // 원산지 설정
    public Rank rank;                                                       // 아이템 등급 설정
    // public Cuts ctime;                                                      // 재료 썬 횟수 설정 (cut time)
    
    public Category1 bigc;                                                  // 스프라이트 세분화 범주 1 설정
    
    // public Finished finishedtype;                                           // 요리 접미사 설정
    
    public string uname;
    public string utag;
    
    // ex) fish의 itemtag = 09, 얘를 한번 썰면 109, 두번썰면 209 이런 형태로 칼은 +100,
    // 후라이팬은 +1000 (itemtag = 1XXX)
    // 냄비는 + 3000 (itemtag = 3XXX)
    // 튀김기는 +5000 (itemtag = 5XXX)
    // 샐러드는 +7000 (itemtag = 7XXX)
    
    public float itemtag;                                                   // 아이템고유태그(코드)
    
    public float fullness;                                                  // 포만감
    public float power;                                                     // 능률 (더 많은 재료 얻을 확률 UP)
    public float efficiency;                                                // 사기 (높은등급 재료 얻을 확률 UP)
    public float freshness;                                                 // 신선도
    public Sprite uitemimage;                                               // 스프라이트 지정

    public bool Use() {
        bool isUsed = false;
        isUsed = true;
        
        return isUsed;
    }
}
