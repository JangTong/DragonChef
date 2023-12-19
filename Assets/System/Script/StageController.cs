using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    public static StageController instance = null; // �̱��� ����

    public float dayCount = 1;

    public int stageNum = 1;
    public int cookCount = 0; // �丮 Ƚ��

    public bool isPreparedStage; // Ȱ��ȭ �� ���� ���������� �̵�

    public Transform itemSpawnPoint;

    public GameObject fieldItemPrefab;

    public List<TypeofItem> stage1Items = new List<TypeofItem>(); // ���������� ������ ����Ʈ
    public List<TypeofItem> stage2Items = new List<TypeofItem>();
    public List<TypeofItem> stage3Items = new List<TypeofItem>();
    public List<TypeofItem> stage4Items = new List<TypeofItem>();
    public List<TypeofItem> stage5Items = new List<TypeofItem>();
        
    public List<NPC> npcs = new List<NPC>(); // NPC����Ʈ


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
        // ���� ����
        int itemCount = 4;
        float avrMorale = 0;
        float avrCourage = 0;


        for(int i =0;i<npcs.Count;i++) // ��հ� ���
        {
            avrMorale += npcs[i].morale;
            avrCourage += npcs[i].courage;
        }
        avrMorale /= npcs.Count;
        avrCourage /= npcs.Count;


        if (dayCount == 1) itemCount = 6;  // itemCount���
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
        if(itemCount < 0)itemCount = 0; // 0���� ������ 0���� ��ȯ
        Debug.Log($"ItemCount: {itemCount}");


        for (int i = 0; i < itemCount; i++) // ������ ����
        {
            int randomIndex = Random.Range(0, stage1Items.Count);

            float randomX = Random.Range(-0.5f, 0.5f);
            float randomZ = Random.Range(-0.5f, 0.5f);
            Vector3 spawnPosition = itemSpawnPoint.position + new Vector3(randomX, 0.3f, randomZ); // ������ ��������Ʈ ����

            // ������ ����
            GameObject spawnedItem = Instantiate(fieldItemPrefab, spawnPosition, Quaternion.identity);
            spawnedItem.GetComponent<FieldItems>().SetItem(stage1Items[randomIndex]);
            switch (stageNum)
            {
                case 1: // �������� 1
                    spawnedItem.GetComponent<FieldItems>().SetItem(stage1Items[randomIndex]);
                    break;
                case 2: // �������� 2
                    spawnedItem.GetComponent<FieldItems>().SetItem(stage2Items[randomIndex]);
                    break;
                case 3: // �������� 3
                    spawnedItem.GetComponent<FieldItems>().SetItem(stage3Items[randomIndex]);
                    break;
                case 4: // �������� 4
                    spawnedItem.GetComponent<FieldItems>().SetItem(stage4Items[randomIndex]);
                    break;
                case 5: // �������� 5
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
    void GoToNextStage() // �������� �̵��Լ�
    {
        if (isPreparedStage)
        {
            switch (stageNum)
            {
                case 1: // �������� 1���� 2�� �̵�
                    SceneManager.LoadScene("Stage2_Ocean");
                    ResetStageStatus();
                    stageNum++;
                    break;
                case 2: // �������� 2���� 3�� �̵�
                    SceneManager.LoadScene("Stage3_MushroomForest");
                    ResetStageStatus();
                    stageNum++; 
                    break;
                case 3: // �������� 3���� 4�� �̵�
                    SceneManager.LoadScene("Stage4_Snowy land");
                    ResetStageStatus();
                    stageNum++; 
                    break;
                case 4: // �������� 4���� 5�� �̵�
                    SceneManager.LoadScene("Stage5_Volcano");
                    ResetStageStatus();
                    stageNum++; 
                    break;
                case 5: // �������� 5���� �������� �̵�
                    SceneManager.LoadScene("Ending");
                    ResetStageStatus();
                    break;
            }
        }
    }
    void GoToNextTime() // �ð� ��ȯ �Լ�
    {
        if(cookCount > 6) // �丮 Ƚ�� 6�� �ʰ� �� �ð���ȯ
        {
            dayCount += 0.5f;
            if (dayCount % 1 != 0) SpawnItems(); // ���� �Ǹ� ������ ����
            cookCount = 0;
        }
    }
    void ResetStageStatus() // �������� ���� �Լ�
    {
        isPreparedStage = false;

        itemSpawnPoint = GameObject.Find("ItemBucket").GetComponent<Transform>(); // item������ġ �ʱ�ȭ

        npcs.Clear();  // NPC�迭 �ʱ�ȭ

        NPC[] foundNPCs = GameObject.FindObjectsOfType<NPC>();
        npcs.AddRange(foundNPCs); 
    }
}
