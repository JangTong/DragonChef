using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 필요 없어보이는건 다 주석처리함 + 카메라가 x축 기준 +30도로 돌아가 있기에 플레이어도 x축 30도 돌려놓음 <- 플레이어 2D 이미지 왜곡 없음 ++ 아마 다른 아이템들도 다  카메라 각도 만큼 돌려놔야 할듯...?
// 필요 없는건 나중에 다 지울 예정 


public class Player : MonoBehaviour
{
    public float speed;                             // Player 걷는 속도 조절하는 변수
    
    public GameObject[] cookings;                   // 뭐더라 애도 필요 없을걸
    public bool[] hasCookings;                      // 뭐더라 얘도 필요 없을걸
    
    float hAxis;                                    // 현재 플레이어의 horizontal 값 받는 변수
    float vAxis;                                    // 현재 플레이어의 vertical 값 받는 변수
    
    bool wDown;
    bool iDown;
    bool sDown1;                                    // 현재는 쓸모 없음
    bool sDown2;                                    // 현재는 쓸모 없음
    bool sDown3;                                    // 현재는 쓸모 없음
    bool sDown4;                                    // 현재는 쓸모 없음
    bool sDown5;                                    // 현재는 쓸모 없음
    bool sDown6;                                    // 현재는 쓸모 없음
    bool qDown;                                     // 현재는 쓸모 없음
    
    bool cDown;                                     // C 입력 받는 bool
    
    bool isSwap;                                    // 현재는 쓸모 없음
    bool isBorder;                                  // StoptoWall() 에서 초록색 Ray를 쏘는데 거기에 다른 사물이 닿았을 때 true가 되는 bool
    
    bool UII;                                       // 인벤 열려있는지 받는 bool

    public Vector3 moveVec;                         // 플레이어 좌표 Vector3 형태로 저장 +
    public Vector3 dropVec;
    public Vector3 pos1;
    
    Rigidbody rigid;
    Animator anim;                                  // 애니메이션 현재는 쓸모없음
    
    GameObject nearObject;                          // collider 안에 들어갔을 때, 그 collider의 object를 받는 변수
    GameObject equipCooking;                        // 현재는 쓸모없음
    
    GameObject dropItem;                            // 이게 뭐더라
    
    public GameObject CookUI;                       // COOKING UI를 담는 gameobject
    public GameObject InvUI;                        // 인벤 UI를 담는 gameobject
    
    int equipCookingIndex = 0;                      // 현재는 쓸모 없음
    
    public bool isClick = false;
    
    public GameObject leg;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        
        CookUI.SetActive(false);                    // 시작하자마자 COOKING UI 꺼놓음
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();                                 // Player에게 필요한 모든 Input을 받는 함수
        
        Move();                                     // Player를 움직을 수 있게 하는 함수           <--- 플레이어를 2D로 바꾸면 얘 아마 수정해야 할걸? + 움직일 때 Rotation을 고정하던지 / 뒷모습 앞모습을 추가하던지 /
        
        // Turn();                                     // Player를 움직일 수 있게 하는 함수
        
        Interaction();                              // 떨어져있는 아이템을 줍는 함수 얘도 지금은 필요 없어보이는 함수임
        
        // Swap();                                     // 1, 2, 3, 4, 5를 눌렀을 때 각각 획득한 도구(아이템)를 드는 모션 1번은 맨손 그러나 현재는 필요 없음
        
        // DropItem();                                 // Q를 눌렀을 때 도구를 떨어뜨리는 함수인데 현재는 필요 없음
        
        // dropVec = this.transform.position;          // 옌 진짜 모르겠음
        
        FixedUpdate();                              // Player의 물리 문제를 고치는 함수
        
        ActInfo();                                  // 인벤 UI가 열려있는지 닫혀있는지를 받는 함수
        
        if (hAxis > 0f || hAxis < 0f)
        {
            transform.Translate(new Vector3(hAxis* speed * Time.deltaTime, 0f));

        }
        if (vAxis > 0f || vAxis < 0f)
        {
            transform.Translate(new Vector3(vAxis * speed * Time.deltaTime, 0f));
        }

        anim.SetFloat("MoveX", hAxis);
        anim.SetFloat("MoveY", vAxis);

    }
    
    void GetInput() {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        iDown = Input.GetButton("Interaction");
        sDown1 = Input.GetButton("Swap1");
        sDown2 = Input.GetButton("Swap2");
        sDown3 = Input.GetButton("Swap3");
        sDown4 = Input.GetButton("Swap4");
        sDown5 = Input.GetButton("Swap5");
        sDown6 = Input.GetButton("Swap6");
        qDown = Input.GetKeyDown(KeyCode.G);
        
        cDown = Input.GetButton("CookingT");
    
    }
    
    void Move() {                                   // 얘는 뭐 Player 움직이게 하는 함수
    
        float currentRotationY = transform.rotation.eulerAngles.y;
        
        if (currentRotationY == 0) {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
            transform.position += moveVec * speed * Time.deltaTime;
        }
        
        else if (currentRotationY == 90) {
            moveVec = new Vector3(vAxis, 0, -hAxis).normalized;
            transform.position += moveVec * speed * Time.deltaTime;
        }
            
        else if (currentRotationY == 180) {
            moveVec = new Vector3(-hAxis, 0, -vAxis).normalized;
            transform.position += moveVec * speed * Time.deltaTime;
        }
            
        else if (currentRotationY == 270) {
            moveVec = new Vector3(-vAxis, 0, hAxis).normalized;
            transform.position += moveVec * speed * Time.deltaTime;
        }
        
        
        
        
        
        // anim.SetBool("isRun", moveVec != Vector3.zero);
        // anim.SetBool("isWalk", wDown);
            
    }
    
    void Turn() {                                   // 플레이어가 움직일 때 가고있는 방향을 바라보게 돌려주는 함수인데 2D면 좌우로 움직일 때 Player가 ( 너무 얇아서 )안보임 그래서 Update()에서 주석 처리 해놓음
        transform.LookAt(transform.position + moveVec);

    }
    
    void Swap() {                                   // 획득한 아이템별로 1, 2, 3, 4, 5 누르면 스왑모션이랑 그런거 있었는데 필요없죠
        hasCookings[0] = true;
        
        if(sDown2 && (!hasCookings[1] || equipCookingIndex == 1))
            return;
        if(sDown3 && (!hasCookings[2] || equipCookingIndex == 2))
            return;
        if(sDown4 && (!hasCookings[3] || equipCookingIndex == 3))
            return;
        if(sDown5 && (!hasCookings[4] || equipCookingIndex == 4))
            return;
        if(sDown6 && (!hasCookings[6] || equipCookingIndex == 6))
            return;
            
    
        int cookingIndex = -1;
        if(sDown1) cookingIndex = 0;
        if(sDown2) cookingIndex = 1;
        if(sDown3) cookingIndex = 2;
        if(sDown4) cookingIndex = 3;
        if(sDown5) cookingIndex = 4;
        if(sDown6) cookingIndex = 5;
        
        if(sDown1 || sDown2 || sDown3 || sDown4 || sDown5 || sDown6){
            if(equipCooking != null)
                equipCooking.SetActive(false);
                
            equipCookingIndex = cookingIndex;
            equipCooking = cookings[cookingIndex];
            equipCooking.SetActive(true);
            
            // anim.SetTrigger("doSwap");
            
            isSwap = true;
            
            Invoke("SwapOut", 0.4f);
            
        }
    }
    
    void DropItem() {                               // 얘도 현재 필요 없음 떨어진 도구 줍는 함수인데
        if (qDown) {
            if (equipCookingIndex == 1) {
                dropVec = this.transform.position;
                dropVec.y += 3;
                dropItem.transform.position = dropVec;

                Instantiate(dropItem, dropItem.transform.position, dropItem.transform.rotation);
                
                dropItem.SetActive(true);
                
                equipCookingIndex = 1;
                equipCooking = cookings[1];
                
                equipCooking.SetActive(false);
                
                // anim.SetTrigger("doSwap");
                
                isSwap = true;
                
                Invoke("SwapOut", 0.4f);

                
                
            }
            
            else if (equipCookingIndex == 2) {
                dropVec = this.transform.position;
                dropVec.y += 3;
                dropItem.transform.position = dropVec;

                Instantiate(dropItem, dropItem.transform.position, dropItem.transform.rotation);
                
                dropItem.SetActive(true);
                
                equipCookingIndex -= 2;
                equipCooking.SetActive(false);
                
                // anim.SetTrigger("doSwap");
                
                isSwap = true;
                
                Invoke("SwapOut", 0.4f);
                
            }
        }
    }
    
    void SwapOut() {                                // 얘도 딱히 필요 없죠
        isSwap = false;
    }
    
    void Interaction() {                            // 얘는 먹은 아이템 식별해주면서 아이템 데이터를 hasCooking에 저장해주는 함수인데 필요 없죠?
        if(nearObject != null) {
            if(iDown) {
                if(nearObject.tag == "Cooking") {
                    dropItem = nearObject;
                    Item item = nearObject.GetComponent<Item>();
                    int cookingIndex = item.value;
                    hasCookings[cookingIndex] = true;
                    
                    nearObject.SetActive(false);
                }
            }
        }
    }
    
    void StoptoWall() {                             // 길이 5만큼의 Ray를 쏘는데 거기에 닿으면 isBorder가 true가 되는 함수임
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 5,LayerMask.GetMask("Tools"));
    }
    
    void FreezeRotation() {                         // 뭐 잘못 밟았을 때 지맘대로 공중에서 공중제비를 조져버리는데 그거 막는 함수임 근데 필요 없어보임...
        // rigid.angularVelocity = Vector3.zero;
    }
    
    void FixedUpdate() {                            //  StoptoWall(), FreezeRotation() 함수를 이 함수로 묶어서 한번에 Update()에 넣어줌 - 그냥 묶는 함수임
        FreezeRotation();
        // StoptoWall();
    }
    
    void ActInfo() {                                // 인벤창이 열려있으면 UII가 true, 닫혀있으면 UII false <- 나중에 COOKING UI만 덩그러니 열려있지 않게 해주는 함수
        if (InvUI.activeSelf == true)
            UII = true;
        else if (InvUI.activeSelf == false)
            UII = false;
    }
    
    public void isCookingClick() {
        isClick = true;
        Debug.Log("d");
    }
    
    void OnTriggerStay(Collider other) {                    // 요리 도구 앞에 섰을 때 인벤 열고 C누르면 COOKING UI 열림 위에서 받은 bool UII의 상테에 의해 작동됨
        if (UII == true) {                                  // 인벤이 열려있을 때
            if(cDown == true) {                             // C를 누르고
                if(other.tag == "Knife") {                  // 서있는 collider의 object tag가 Knife면
                    CookUI.SetActive(true);                 // 일단 COOKING UI를 열고, 밑에 행동을 하는겨, 이하 같으므로 생략
                    Inventory.instance.cooknum = 1;
                }
                
                else if(other.tag == "FryingPan") {
                    CookUI.SetActive(true);
                    Inventory.instance.cooknum = 2;
                }
                
                
                else if(other.tag == "Pot") {
                    CookUI.SetActive(true);
                    Inventory.instance.cooknum = 3;
                    
                }
                
                else if(other.tag == "Bowl") {
                    CookUI.SetActive(true);
                    Inventory.instance.cooknum = 4;
                    
                }
                
                else if(other.tag == "Fryer") {
                    CookUI.SetActive(true);
                    Inventory.instance.cooknum = 5;
                    
                }
                
                else if(other.tag == "Archer") {
                    CookUI.SetActive(true);
                    Inventory.instance.cooknum = 6;
                    
                }
                
                else if(other.tag == "Priest") {
                    CookUI.SetActive(true);
                    Inventory.instance.cooknum = 7;
                    
                }
                
                else if(other.tag == "Warrior") {
                    CookUI.SetActive(true);
                    Inventory.instance.cooknum = 8;
                    
                }
                
                else if(other.tag == "Yongsa") {
                    CookUI.SetActive(true);
                    Inventory.instance.cooknum = 9;
                    
                }
                
            }
        }
        else                                                // 인벤이 꺼지면 같이 거짐
            CookUI.SetActive(false);
            
        if(other.tag == "leg" && leg.activeSelf == true) {
            StageController.instance.isPreparedStage = true;
        }
    }
    
    void OnTriggerExit(Collider other) {                    // 얘는 이제 현재는 Player가 인벤이 열려있어도 움직일 수 있으니 적을라캤는데 그냥 인벤 열려있을 때에는 못움직이게 만들고싶음 ㅎㅎ
        
    }
        
    void NumberAccessDeny() {
        
    }
    
}
