using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveGameScript;

public class SaveGameScript : MonoBehaviour
{
    private ItemCollectorScript itemCollectorScript;
    private TimerScript timerScript;
    private PlayerDataWraper playerDataWraper;

    private string path;
    // Start is called before the first frame update
    void Start()
    {
        GameObject plyer = GameObject.FindGameObjectWithTag("Player");
        itemCollectorScript = plyer.GetComponent<ItemCollectorScript>();
        timerScript = GameObject.FindGameObjectWithTag("UI").GetComponent<TimerScript>();
        path = Application.dataPath + "/data.json";
        playerDataWraper = new()
        {
            playerData = Array.Empty<PlayerData>()
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveData()
    {
        PlayerData newData = new PlayerData
        {
            Level = SceneManager.GetActiveScene().buildIndex,
            CountOfGems = itemCollectorScript.CountOfGems,
            CountOfLive = itemCollectorScript.CountOfLives,
            ScoreTime = Mathf.FloorToInt(timerScript.CurrentTime)
        };
        LoadData();

        AddData(ref playerDataWraper.playerData, newData);

        string jsonData = JsonUtility.ToJson(playerDataWraper, true);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(jsonData);
    }

    public void LoadData()
    {
        try
        {
            using StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();

            playerDataWraper = JsonUtility.FromJson<PlayerDataWraper>(json);
            if (playerDataWraper == null)
            {
                playerDataWraper = new()
                {
                    playerData = Array.Empty<PlayerData>()
                };
            }
        } catch 
        {
            return;
        }
    }

    public void AddData(ref PlayerData[] playerDatas, PlayerData newPlayerData)
    {
        for (int i = 0; i < playerDatas.Length; i++)
        {
            PlayerData actualData = playerDatas[i];
            if (actualData.Level == newPlayerData.Level && actualData.ScoreTime > newPlayerData.ScoreTime) 
            {
                playerDatas[i] = newPlayerData;
                return;
            }
        }

        bool levelDataExist = false;
        for (int i = 0; i < playerDatas.Length; i++)
        {
            if (playerDatas[i].Level == newPlayerData.Level)
            {
                levelDataExist = true;
                break;
            }
        }

        if (playerDatas.Length == 0 || !levelDataExist)
        {
            PlayerData[] newPlayerDataArray = { newPlayerData };

            PlayerData[] concatPlayerDataArray = new PlayerData[playerDataWraper.playerData.Length + 1];

            Array.Copy(playerDataWraper.playerData, 0, concatPlayerDataArray, 0, playerDataWraper.playerData.Length);
            Array.Copy(newPlayerDataArray, 0, concatPlayerDataArray, playerDataWraper.playerData.Length, newPlayerDataArray.Length);
            playerDataWraper.playerData = concatPlayerDataArray;
        } 
    }


}
