using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    public SaveData data;
    public GameManagerScript GMScript;
    public string file = "result.txt";

    //Reference stats to be saved
    public void CreatePlayerData()
    {
        data = new SaveData(GMScript.timePassed, GMScript.collectedCollectibles, GMScript.currentScore);
    }

    //Save data to File
    public void Save()
    {
        string json = JsonUtility.ToJson(data);
        WriteToFile(file, json);
    }
    //Write data to json file
    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    //Set file destination
    private string GetFilePath (string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
