using Assets.Models;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BestScoresScript : MonoBehaviour
{
    [SerializeField] private GameObject myObject;
    [SerializeField] private GameObject parentObject;

    private string path => Application.dataPath + "/data.json";

    private PlayerDataWraper playerDataWraper;
   

    // Start is called before the first frame update
    void Start()
    {
        CreateWrapper();
        LoadData();
        ChangeView();
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

    private void ChangeView()
    {
        foreach (PlayerData playerData in playerDataWraper.playerData.OrderBy(x => x.Level))
        {
            GameObject temp = Instantiate(myObject, parentObject.transform);

            Transform lvlTransform = temp.transform.Find("LvlText");
            Text textComponent = lvlTransform.GetComponent<Text>();
            textComponent.text = playerData.Level.ToString();

            Transform scoreTimeTransform = temp.transform.Find("ScoreText");
            textComponent = scoreTimeTransform.GetComponent<Text>();
            textComponent.text = playerData.ScoreTime.ToString();

            Transform countOfDiesTransform = temp.transform.Find("CountOfDiesText");
            textComponent = countOfDiesTransform.GetComponent<Text>();
            textComponent.text = playerData.CountOfDies.ToString();

            Transform countOfEnemiesTransform = temp.transform.Find("CountOfEnemiesText");
            textComponent = countOfEnemiesTransform.GetComponent<Text>();
            textComponent.text = playerData.CountOfDies.ToString();
        }
    }

    public void DeleteData()
    {
        CreateWrapper();
        string jsonData = JsonUtility.ToJson(playerDataWraper, true);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(jsonData);
        Clean();
    }

    private void Clean()
    {
        if (parentObject.transform.childCount > 1)
        {
            for (int i = 1; i < parentObject.transform.childCount; i++)
            {
                Destroy(parentObject.transform.GetChild(i).gameObject);
            }
        }
    }

    public void CreateWrapper()
    {
        playerDataWraper = new()
        {
            playerData = Array.Empty<PlayerData>()
        };
    }
}
