using UnityEngine;

using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";


    public FileDataHandler(string _dataDirPath, string _dataFileName)
    {
        dataDirPath = _dataDirPath;
        dataFileName = _dataFileName;
    }


    public void Save(GameData _data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {

        }

        catch (Exception e)
        {
            Debug.LogError("error"+fullPath+"\n"+e);
        }
    }

}
