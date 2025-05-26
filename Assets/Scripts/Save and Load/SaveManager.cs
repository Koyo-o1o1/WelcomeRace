using System.Collections.Generic;
using System.Xml.Serialization;
using NUnit.Framework;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private GameData gameData;

    private List<ISaveManager> saveManagers;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }


    private void Start()
    {
        saveManagers = FindAllSaveManager();

        LoadData();


    }

    public void NewGame()
    {
        gameData = new GameData();
    }


    public void LoadData()
    {
        //gamedata=data from data handler


        if(this.gameData == null)
        {
            Debug.Log("NO save data");
            NewGame();
        }


        foreach (ISaveManager savemanager in saveManagers)
        {
            savemanager.LoadData(gameData);
        }

        Debug.Log("Loaded currency" + gameData.currency);
    }

    public void SaveGame()
    {
        //data handler save gamedata
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }

        Debug.Log("Saved currency"+gameData.currency);
    }



    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManager()
    {
        var saveManagers = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        return saveManagers.OfType<ISaveManager>().ToList();
    }
}
