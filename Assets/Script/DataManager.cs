using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

[System.Serializable]
public class SaveData
{
    // 플레이어
    public Vector3 playerPos;
    public float health;

    // 적
    public List<int> alived = new List<int>();

    public int aliveChecked;

    // 인벤토리(총알, 권총알, 구급 상자)
    public int counts;
    public int nowB;
    public int nowPB;

    public int leftB;
    public int leftPB;
}

public class DataManager : MonoBehaviour
{
    StartSceneManager manageStart;
    Player player;
    HPManager hpManager;
    SniperManager sniperManager;
    PistolManager pistolManager;
    HealPackManager healPack;
    InventoryCreator creators;

    SaveData saveData = new SaveData();

    private string savePath;

    public GameObject enemys;
    public List<int> bools;

    private void Awake()
    {
        manageStart = FindObjectOfType<StartSceneManager>();
        player = FindObjectOfType<Player>();
        sniperManager = FindObjectOfType<SniperManager>();
        pistolManager = FindObjectOfType<PistolManager>();
        hpManager = FindObjectOfType<HPManager>();
        creators = FindObjectOfType<InventoryCreator>();
        // 경로
        savePath = $"{Application.dataPath}/Save/Save.json";

        if(manageStart != null)
        {
            if (manageStart.isLoaded)
            {
                LoadStart();
            }
        }
        Debug.Log(savePath);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            SaveStart();
        }
    }

    public void SaveStart()
    {
        if (saveData.alived == null)
        {
            for (int i = 0; i < enemys.transform.childCount; i++)
            {
                //Debug.Log(enemys.transform.GetChild(i).GetComponent<TestNav>().saveSet);
                GameManager.instance.aliveCount = enemys.transform.GetChild(i).GetComponent<TestNav>().saveSet;

                saveData.alived.Add(enemys.transform.GetChild(i).GetComponent<TestNav>().saveSet);
            }
        }
        else
        {
            saveData.alived = new List<int>();

            for (int i = 0; i < enemys.transform.childCount; i++)
            {
                //Debug.Log(enemys.transform.GetChild(i).GetComponent<TestNav>().saveSet);
                GameManager.instance.aliveCount = enemys.transform.GetChild(i).GetComponent<TestNav>().saveSet;

                saveData.alived.Add(enemys.transform.GetChild(i).GetComponent<TestNav>().saveSet);
            }
        }

        GameManager.instance.pos = player.transform.position;
        GameManager.instance.hp = hpManager.currentHP;
        GameManager.instance.nB = sniperManager.bulletCount;
        GameManager.instance.bPB = pistolManager.currentPistolBulletCount;
        GameManager.instance.healKits = creators.healPackCount;
        GameManager.instance.leftNB = creators.bulletCount;
        GameManager.instance.leftPB = creators.pistolCount;

        saveData.playerPos = GameManager.instance.pos;
        saveData.health = GameManager.instance.hp;
        saveData.nowB = GameManager.instance.nB;
        saveData.nowPB = GameManager.instance.bPB;
        saveData.counts = GameManager.instance.healKits;
        saveData.leftB = GameManager.instance.leftNB;
        saveData.leftPB = GameManager.instance.leftPB;

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);

    }

    public void LoadStart()
    {
        SaveData saveData = new SaveData();

        if(!File.Exists(savePath))
        {
            //SaveStart();
            //LoadStart();
            Debug.Log("NONE");
        }
        else
        {
            string loadJson = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            for (int i = 0; i < enemys.transform.childCount; i++)
            {
                enemys.transform.GetChild(i).GetComponent<TestNav>().saveSet = saveData.alived[i];
                Debug.Log($"{i} = {enemys.transform.GetChild(i).GetComponent<TestNav>().saveSet} = {saveData.alived[i]}");
            }

            Vector3 pos = new Vector3(saveData.playerPos.x, saveData.playerPos.y, saveData.playerPos.z);
            player.transform.position = pos;

            hpManager.currentHP = saveData.health;
            sniperManager.bulletCount = saveData.nowB;
            pistolManager.currentPistolBulletCount = saveData.nowPB;
            creators.healPackCount = saveData.counts;
            creators.bulletCount = saveData.leftB;
            creators.pistolCount = saveData.leftPB;
        }
    }
}
