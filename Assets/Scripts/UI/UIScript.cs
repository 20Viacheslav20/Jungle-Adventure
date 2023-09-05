using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] private Text clLevel1;
    [SerializeField] private Text cgLevel1;
    [SerializeField] private Text level1;
    [SerializeField] private Text scorel1;
    
    [SerializeField] private Text clLevel2;
    [SerializeField] private Text cgLevel2;
    [SerializeField] private Text level2;
    [SerializeField] private Text scorel2;

    [SerializeField] private Text clLevel3;
    [SerializeField] private Text cgLevel3;
    [SerializeField] private Text level3;
    [SerializeField] private Text scorel3;

    private string path;

    private PlayerDataWraper playerDataWraper;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/data.json";
        CreateWrapper();
        LoadData();
        if (playerDataWraper != null &&  playerDataWraper.playerData.Count() != 0)
        {
            for (int i = 0; i < playerDataWraper.playerData.Length; i++)
            {
                PlayerData playerData = playerDataWraper.playerData[i];
                switch (playerData.Level)
                {
                    case 1:
                        clLevel1.text = playerData.CountOfLive.ToString();
                        cgLevel1.text = playerData.CountOfGems.ToString();
                        level1.text = playerData.Level.ToString();
                        scorel1.text = playerData.ScoreTime.ToString();
                        break;
                    case 2:
                        clLevel2.text = playerData.CountOfLive.ToString();
                        cgLevel2.text = playerData.CountOfGems.ToString();
                        level2.text = playerData.Level.ToString();
                        scorel2.text = playerData.ScoreTime.ToString();
                        break;
                    case 3:
                        clLevel3.text = playerData.CountOfLive.ToString();
                        cgLevel3.text = playerData.CountOfGems.ToString();
                        level3.text = playerData.Level.ToString();
                        scorel3.text = playerData.ScoreTime.ToString();
                        break;
                    default: break;
                }
            }
        }
    }


    private void LoadData()
    {
        try
        {
            using StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();

            playerDataWraper = JsonUtility.FromJson<PlayerDataWraper>(json);
        }
        catch
        {
            CreateWrapper();
        }
    }

    public void DeleteData()
    {
        CreateWrapper();
        string jsonData = JsonUtility.ToJson(playerDataWraper, true);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(jsonData);
        DeleteUIText();
    }

    public void CreateWrapper()
    {
        playerDataWraper = new()
        {
            playerData = Array.Empty<PlayerData>()
        };
    }

    private void DeleteUIText()
    {
        clLevel1.text = "";
        cgLevel1.text = "";
        level1.text = "";
        scorel1.text = "";

        clLevel2.text = "";
        cgLevel2.text = "";
        level2.text = "";
        scorel2.text = "";

        clLevel3.text = "";
        cgLevel3.text = "";
        level3.text = "";
        scorel3.text = "";
    }


}
