using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    public static StageController instance = null; // �̱��� ����

    public float dayCount = 0;

    public int stageNum = 1;
    public int cookCount = 0; // �丮 Ƚ��

    public bool isPrepared; // Ȱ��ȭ �� ���� ��¥�� ����
    public bool isPreparedStage; // Ȱ��ȭ �� ���� ���������� �̵�

    public Transform itemSpawnPoint;

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
    void ResetStageStatus()
    {
        isPreparedStage = false;
        isPrepared = false;

        itemSpawnPoint = gameObject.Find("ItemBucket").GetComponent<Transform>;

        npcs.Clear();  // NPC�迭 �ʱ�ȭ

        NPC[] foundNPCs = GameObject.FindObjectsOfType<NPC>();
        npcs.AddRange(foundNPCs); 
    }
}
