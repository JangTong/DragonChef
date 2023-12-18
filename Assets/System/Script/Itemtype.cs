using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemtype : MonoBehaviour
{
    // 원산지
    public enum Origins { Stage1, Stage2, Stage3, Stage4, Stage5, ETC1, ETC2, ETC3 };
    
    // 스프라이트 세분화 범주 1
    public enum Category1 { Meat, Vege, Seaf, ETC1, ETC2, ETC3 };
    
    public enum Category2 { Meat, Vege, Seaf, ETC1, ETC2, ETC3 };
    
    public enum Category3 { Meat, Vege, Seaf, ETC1, ETC2, ETC3 };
    
    // 아이템 등급
    public enum Rank { Common, Rare, Special, Magical, Legendary, ETC };
    
    // 재료 썬 횟수
    public enum Cuts { Once, Twice, Third, Fourth};
    
    // 요리 접미사
    public enum Finished { Ingredient, Stirfry, Soup, Salad, Deepfry };
    
    
    
    public Origins origin;                  // 원산지 설정
    public Rank rank;                       // 아이템 등급 설정
    
    public Cuts ctime;                      // 재료 썬 횟수 설정 (cut time)
    
    public Category1 Big;                   // 스프라이트 세분화 범주 1 설정
    public Category2 Mid;
    public Category3 Small;
    
    public Finished finishedtype;           // 요리 접미사 설정
    
    public string uname;                    // 이름 (unique name)
    public string utag;                     // 밸류 (unique tag) (고유 태그...?)
    public float fullness;                  // 포만감
    public float alot;                      // 능률 (더 많은 재료 얻을 확률 UP)
    public float higher;                    // 사기 (높은등급 재료 얻을 확률 UP)
    public float freshness;                 // 신선도
    
}
