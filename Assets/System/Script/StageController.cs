using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    public static StageController instance = null; // 싱글톤 선언

    public float dayCount = 0;

    public int stageNum = 1;
    public int cookCount = 0; // 요리 횟수

    public bool isPrepared; // 활성화 시 다음 날짜로 변경
    public bool isPreparedStage; // 활성화 시 다음 스테이지로 이동

    public Transform itemSpawnPoint;

    public List<TypeofItem> stage1Items = new List<TypeofItem>(); // 스테이지별 아이템 리스트
    public List<TypeofItem> stage2Items = new List<TypeofItem>();
    public List<TypeofItem> stage3Items = new List<TypeofItem>();
    public List<TypeofItem> stage4Items = new List<TypeofItem>();
    public List<TypeofItem> stage5Items = new List<TypeofItem>();
        
    public List<NPC> npcs = new List<NPC>(); // NPC리스트


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance!=this)
            {
                Destroy(gameObject);
            }
        }
        ResetStageStatus();
    }

    void Start()
    {
        ResetStageStatus();
    }

    void Update()
    {
        GenerateStageItemList();
        GoToNextStage();
    }


    void GenerateStageItemList()
    {
        if (stage1Items.Count == 0 || stage2Items.Count == 0 || stage3Items.Count == 0 || stage4Items.Count == 0 || stage5Items.Count == 0)
        {
            for (int i = 1; i < ItemDB.instance.typeofitem.Count; i++)
            {
                if ((ItemDB.instance.typeofitem[i].origin == Origins.Stage0 || ItemDB.instance.typeofitem[i].origin == Origins.Stage1) && ItemDB.instance.typeofitem[i].itemtag < 100) stage1Items.Add(ItemDB.instance.typeofitem[i]);
                if ((ItemDB.instance.typeofitem[i].origin == Origins.Stage0 || ItemDB.instance.typeofitem[i].origin == Origins.Stage2) && ItemDB.instance.typeofitem[i].itemtag < 100) stage2Items.Add(ItemDB.instance.typeofitem[i]);
                if ((ItemDB.instance.typeofitem[i].origin == Origins.Stage0 || ItemDB.instance.typeofitem[i].origin == Origins.Stage3) && ItemDB.instance.typeofitem[i].itemtag < 100) stage3Items.Add(ItemDB.instance.typeofitem[i]);
                if ((ItemDB.instance.typeofitem[i].origin == Origins.Stage0 || ItemDB.instance.typeofitem[i].origin == Origins.Stage4) && ItemDB.instance.typeofitem[i].itemtag < 100) stage4Items.Add(ItemDB.instance.typeofitem[i]);
                if ((ItemDB.instance.typeofitem[i].origin == Origins.Stage0 || ItemDB.instance.typeofitem[i].origin == Origins.Stage5) && ItemDB.instance.typeofitem[i].itemtag < 100) stage5Items.Add(ItemDB.instance.typeofitem[i]);
            }
            Debug.Log("Stage item list Generated");
        }
    }
    void GoToNextStage() // 스테이지 이동함수
    {
        if (isPreparedStage)
        {
            switch (stageNum)
            {
                case 1: // 스테이지 1에서 2로 이동
                    SceneManager.LoadScene("Stage2_Ocean");
                    ResetStageStatus();
                    stageNum++;
                    break;
                case 2: // 스테이지 2에서 3로 이동
                    SceneManager.LoadScene("Stage3_MushroomForest");
                    ResetStageStatus();
                    stageNum++; 
                    break;
                case 3: // 스테이지 3에서 4로 이동
                    SceneManager.LoadScene("Stage4_Snowy land");
                    ResetStageStatus();
                    stageNum++; 
                    break;
                case 4: // 스테이지 4에서 5로 이동
                    SceneManager.LoadScene("Stage5_Volcano");
                    ResetStageStatus();
                    stageNum++; 
                    break;
                case 5: // 스테이지 5에서 엔딩으로 이동
                    SceneManager.LoadScene("Ending");
                    ResetStageStatus();
                    break;
            }
        }
    }
    void ResetStageStatus()
    {
        isPreparedStage = false;
        isPrepared = false;

        itemSpawnPoint = gameObject.Find("ItemBucket").GetComponent<Transform>;

        npcs.Clear();  // NPC배열 초기화

        NPC[] foundNPCs = GameObject.FindObjectsOfType<NPC>();
        npcs.AddRange(foundNPCs); 
    }
}
