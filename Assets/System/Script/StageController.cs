using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    public static StageController instance = null; // 싱글톤 선언

    public float dayCount = 1;

    public int stageNum = 1;
    public int cookCount = 0; // 요리 횟수

    public bool isPreparedStage; // 활성화 시 다음 스테이지로 이동

    public Transform itemSpawnPoint;

    public GameObject fieldItemPrefab;

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
        GenerateStageItemList();
        SpawnItems();
    }

    void Update()
    {
        GoToNextTime();
        GoToNextStage();
    }

    void SpawnItems()
    {
        // 변수 선언
        int itemCount = 4;
        float avrMorale = 0;
        float avrCourage = 0;


        for(int i =0;i<npcs.Count;i++) // 평균값 계산
        {
            avrMorale += npcs[i].morale;
            avrCourage += npcs[i].courage;
        }
        avrMorale /= npcs.Count;
        avrCourage /= npcs.Count;


        if (dayCount == 1) itemCount = 6;  // itemCount계산
        else
        {
            for (int i = 0; i < npcs.Count; i++)
            {
                if (npcs[i].fullness < 20) itemCount -= 3;
                else if(npcs[i].fullness < 50) itemCount -= 2;
                else if (npcs[i].fullness < 80) itemCount -= 1;
            }
            itemCount += (int)(avrMorale / 20);
            itemCount += (int)(avrCourage / 20);
        }
        if(itemCount < 0)itemCount = 0; // 0보다 작으면 0으로 변환
        Debug.Log($"ItemCount: {itemCount}");


        for (int i = 0; i < itemCount; i++) // 아이템 생성
        {
            int randomIndex = Random.Range(0, stage1Items.Count);

            float randomX = Random.Range(-0.5f, 0.5f);
            float randomZ = Random.Range(-0.5f, 0.5f);
            Vector3 spawnPosition = itemSpawnPoint.position + new Vector3(randomX, 0.3f, randomZ); // 아이템 스폰포인트 지정

            // 아이템 스폰
            GameObject spawnedItem = Instantiate(fieldItemPrefab, spawnPosition, Quaternion.identity);
            spawnedItem.GetComponent<FieldItems>().SetItem(stage1Items[randomIndex]);
            switch (stageNum)
            {
                case 1: // 스테이지 1
                    spawnedItem.GetComponent<FieldItems>().SetItem(stage1Items[randomIndex]);
                    break;
                case 2: // 스테이지 2
                    spawnedItem.GetComponent<FieldItems>().SetItem(stage2Items[randomIndex]);
                    break;
                case 3: // 스테이지 3
                    spawnedItem.GetComponent<FieldItems>().SetItem(stage3Items[randomIndex]);
                    break;
                case 4: // 스테이지 4
                    spawnedItem.GetComponent<FieldItems>().SetItem(stage4Items[randomIndex]);
                    break;
                case 5: // 스테이지 5
                    spawnedItem.GetComponent<FieldItems>().SetItem(stage5Items[randomIndex]);
                    break;
            }
        }
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
    void GoToNextTime() // 시간 전환 함수
    {
        if(cookCount > 6) // 요리 횟수 6번 초과 시 시간전환
        {
            dayCount += 0.5f;
            if (dayCount % 1 != 0) SpawnItems(); // 밤이 되면 아이템 생성
            cookCount = 0;
        }
    }
    void ResetStageStatus() // 스테이지 리셋 함수
    {
        isPreparedStage = false;

        itemSpawnPoint = GameObject.Find("ItemBucket").GetComponent<Transform>(); // item스폰위치 초기화

        npcs.Clear();  // NPC배열 초기화

        NPC[] foundNPCs = GameObject.FindObjectsOfType<NPC>();
        npcs.AddRange(foundNPCs); 
    }
}
