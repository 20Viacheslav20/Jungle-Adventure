using Assets.Models;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveGameScript : MonoBehaviour
{
    //private ItemCollectorScript itemCollectorScript;
    private TimerScript timerScript;
    private PlayerDataWraper playerDataWraper;
    private PlayerControllerScript playerController;

    private string path;
    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/data.json";
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        playerController = player.GetComponent<PlayerControllerScript>();
        timerScript = GameObject.FindGameObjectWithTag("UI").GetComponent<TimerScript>();
        
        //itemCollectorScript = player.GetComponent<ItemCollectorScript>();
        
        playerDataWraper = new()
        {
            playerData = Array.Empty<PlayerData>()
        };
    }

    public void SaveData()
    {
        PlayerData newData = new PlayerData
        {
            Level = SceneManager.GetActiveScene().buildIndex - 2,
            //CountOfGems = itemCollectorScript.CountOfGems,
            //CountOfLive = itemCollectorScript.CountOfLives,
            CountOfDies = playerController.CountOfDies,
            CountOfEnemies = playerController.CountOfEnemies,
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

            playerDataWraper = JsonUtility.FromJson<PlayerDataWraper>(json); // read data from file
            if (playerDataWraper is null)
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
            // data of lvl exists and new score is smaller
            if (actualData.Level == newPlayerData.Level && actualData.ScoreTime > newPlayerData.ScoreTime) 
            {
                playerDatas[i] = newPlayerData;
                return;
            }
        }

        bool levelDataExist = playerDatas.ToList().Any(x => x.Level == newPlayerData.Level);

        if (playerDatas.Length == 0 || !levelDataExist)
        {
            PlayerData[] concatPlayerDataArray = new PlayerData[playerDataWraper.playerData.Length + 1];

            // copy old data to new array with lenght (old + 1)
            Array.Copy(playerDataWraper.playerData, 0, concatPlayerDataArray, 0, playerDataWraper.playerData.Length);

            concatPlayerDataArray[concatPlayerDataArray.Length - 1] = newPlayerData;

            playerDataWraper.playerData = concatPlayerDataArray;
        } 
    }


}
